using AutoMapper;
using KnihovnaHer.Data.Models;
using KnihovnaHer.Dto;

namespace KnihovnaHer.Api
{
    public class AutomapperConfigurationProfile : Profile
    {
        public AutomapperConfigurationProfile()
        {
            CreateMap<Uzivatel, UzivatelDto>()
            .ForMember(dest => dest.UzivatelId, opt => opt.MapFrom(src => src.Id))  
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)) 
            .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin));  

            CreateMap<UzivatelCreateDto, Uzivatel>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)) 
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin)); 


            CreateMap<UzivatelEditDto, Uzivatel>()
                   .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin));



            CreateMap<Zanr, string>()
          .ConstructUsing((zanr, resolutionContext) => zanr.Nazev);

            CreateMap<Zanr, ZanrDto>();
            CreateMap<ZanrDto,Zanr>();


            CreateMap<Hra,HraDto>();
            CreateMap<HraDto, Hra>();

            CreateMap<HraCreateEditDto, Hra>()
             .ForMember(dest => dest.Zanry, opt => opt.Ignore());

            




            CreateMap<StatusHryEditDto, StatusHry>();
            CreateMap<StatusHryCreateDto, StatusHry>();
            CreateMap<StatusHry, StatusHryViewDto>();
      

            CreateMap<Vydavatel, VydavatelDto>();
            CreateMap<VydavatelDto, Vydavatel>();










        }



    }
}
