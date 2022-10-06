using AutoMapper;
using ClothesStore.Entities;
using ClothesStore.Models;

namespace ClothesStore
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<AddTrousersDto, Trousers>();
            CreateMap<Trousers, TrousersDto>();

            CreateMap<AddShoesDto, Shoe>();
            CreateMap<Shoe, ShoesDto>();

            CreateMap<AddPulloverAndSweatshirtDto, PulloverAndSweatshirt>();
            CreateMap<PulloverAndSweatshirt, PulloverAndSweatshirtDto>();

            CreateMap<RegisterUserDto, User>()
                .ForMember(a => a.Address, c => c.MapFrom(s => new Address {Country = s.Country, City = s.City, Street = s.Street, PostalCode = s.PostalCode, StreetNumber = s.StreetNumber}));

        }
    }
}
