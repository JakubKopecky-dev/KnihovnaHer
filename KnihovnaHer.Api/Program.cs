using System.Diagnostics;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using KnihovnaHer.Api;
using KnihovnaHer.Api.Interfaces;
using KnihovnaHer.Api.Managers;
using KnihovnaHer.Api.Settings;
using KnihovnaHer.Data.Interfaces;
using KnihovnaHer.Data.Models;
using KnihovnaHer.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// Pøipojení k databázi
var connectionString = builder.Configuration.GetConnectionString("LocalKnihovnaHerConnection");
builder.Services.AddDbContext<KnihovnaHerDbContext>(options =>
    options.UseSqlServer(connectionString)
        .UseLazyLoadingProxies()
        .ConfigureWarnings(x => x.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning)));



// JWT autentizace
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("Missing Jwt:Key"))),
        RoleClaimType = ClaimTypes.Role
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"[AUTH ERROR] {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("[AUTH SUCCESS] Token is valid");
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Console.WriteLine($"[AUTH CHALLENGE] {context.Error} - {context.ErrorDescription}");
            return Task.CompletedTask;
        }
    };
});


// Registrace JWT nastavení
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();




// Registrace Identity bez cookie autentizace
builder.Services.AddIdentityCore<Uzivatel>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<KnihovnaHerDbContext>();






// Registrace kontrolerù
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));



// Swagger s JWT podporou
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("knihovnaher", new OpenApiInfo
    {
        Version = "v1",
        Title = "Knihovna her",
        Description = "Webové API pro knihovnu her",
        Contact = new OpenApiContact
        {
            Name = "Jakub Kopecký"
        }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Zadejte JWT token:",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Repositáøe
builder.Services.AddScoped<IHraRepository, HraRepository>();
builder.Services.AddScoped<IStatusHryRepository, StatusHryRepository>();
builder.Services.AddScoped<IVydavatelRepository, VydavatelRepository>();
builder.Services.AddScoped<IZanrRepository, ZanrRepository>();

// Identity pomocníci
builder.Services.AddScoped<UserManager<Uzivatel>>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();

// Manažeøi
builder.Services.AddScoped<IUzivatelManager, UzivatelManager>();
builder.Services.AddScoped<IHraManager, HraManager>();
builder.Services.AddScoped<IZanrManager, ZanrManager>();
builder.Services.AddScoped<IVydavatelManager, VydavatelManager>();
builder.Services.AddScoped<IStatusHryManager, StatusHryManager>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(AutomapperConfigurationProfile));

var app = builder.Build();

app.Use(async (context, next) =>
{
    Console.WriteLine($"[Middleware Log] Request: {context.Request.Method} {context.Request.Path}");
    await next();
});

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("knihovnaher/swagger.json", "Knihovna Her - v1");
    });
}

// Middleware pro JWT autentizaci a autorizaci
app.UseAuthentication();
app.UseAuthorization();

// Mapování kontrolerù
app.MapControllers();

// Vytvoøení rolí
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await CreateAllRoles(roleManager);
}


app.Run();








async Task CreateAllRoles(RoleManager<IdentityRole> roleManager)
{
    var constants = typeof(UserRoles)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .Where(fieldInfo => fieldInfo.IsLiteral && !fieldInfo.IsInitOnly && fieldInfo.FieldType == typeof(string))
        .ToArray();

    foreach (var role in constants.Select(f => f.GetRawConstantValue()).OfType<string>())
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}
