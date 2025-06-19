using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaHer.Maui.Services
{
    public partial class AuthorizationHandler(ITokenStorageService tokenStorageService) : DelegatingHandler
    {
        private readonly ITokenStorageService tokenStorageService = tokenStorageService;




        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await tokenStorageService.GetTokenAsync();
         


            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Debug.WriteLine("[AuthHandler] Přidávám token: " + token);
            }
            else
            {
                Debug.WriteLine("[AuthHandler] Token není dostupný.");
            }

            Debug.WriteLine($"[AuthHandler] FINAL Authorization Header: {request.Headers.Authorization}");
            Debug.WriteLine("==== FINAL REQUEST HEADERS ====");
            foreach (var header in request.Headers)
            {
                Debug.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }
            Debug.WriteLine("==== END HEADERS ====");



            var response = await base.SendAsync(request, cancellationToken);


            Debug.WriteLine($"[AuthHandler] Response status code: {response.StatusCode}");
            if (response.Content != null)
            {
                var content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"[AuthHandler] Response content: {content}");
            }
            return response;




        }


    }
}
