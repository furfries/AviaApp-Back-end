using AutoMapper;
using AviaApp.Models.ViewModels;
using Data.Entities;

namespace AviaApp.Mapper.Profiles;

public class CabinClassProfile : Profile
{
    public CabinClassProfile()
    {
        CreateMap<CabinClass, CabinClassViewModel>();
        CreateMap<CabinClassViewModel, CabinClass>();
    }
}