using AutoMapper;
using Talabat.APIS.DTOs;
using Talabate.Core.Entites;

namespace Talabat.APIS.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d=>d.ProductType,O=>O.MapFrom(S=>S.ProductType.Name))
                .ForMember(d=>d.ProductBrand,O=>O.MapFrom(S=>S.ProductBrand.Name))
                .ForMember(d=>d.PictureUrl,O=>O.MapFrom<ProductPictureUrlResolver>());

        }
    }
}
