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
            CreateMap<ProductDto, Domain.Models.Product>()
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
               
            CreateMap< Domain.Models.Product, ProductDto>();
             

            CreateMap<Domain.Models.User, UserDto>();
        }
    }

}
