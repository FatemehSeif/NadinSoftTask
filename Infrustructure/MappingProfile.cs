using Application.Contexts;
using Application.DTOs.AccountDtos;
using Application.DTOs.ProductDtos;
using AutoMapper;

namespace Infrustructure
{
    public class MappingProfile : Profile
    {
        private readonly IDataBaseContext context; 
        public MappingProfile()
        {
            CreateMap<Domain.Models.Product, ProductDto>()
                .ForMember(dto => dto.CreatedBy, opt => opt
                .MapFrom(src => src.CreatedBy)).ReverseMap();

            CreateMap<Domain.Models.User, UserDto>();
        }
    }

}
