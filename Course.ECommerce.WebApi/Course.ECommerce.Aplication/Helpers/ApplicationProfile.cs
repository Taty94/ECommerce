using AutoMapper;
using Course.ECommerce.Aplication.Dtos;
using Course.ECommerce.Domain.Entities;

namespace Course.ECommerce.Aplication.Helpers
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            //CreateMap<Origen,Destino>
            CreateMap<ProductDto, Product>();
            CreateMap<CreateProductDto, Product>();
            
            //CreateMap<Product, ProductDto>().ForMember(p=>p.ProductBrand,x=>x.MapFrom(org=>org.ProductBrand.Description))
            //                                .ForMember(p=>p.ProductType, x=>x.MapFrom(org=>org.ProductType.Description));

            CreateMap<ProductTypeDto, ProductType>();
            CreateMap<ProductType, ProductTypeDto>();
            CreateMap<CreateProductTypeDto, ProductType>();

            CreateMap<ProductBrandDto, ProductBrand>();
            CreateMap<ProductBrand, ProductBrandDto>();
            CreateMap<CreateProductBrandDto, ProductBrand>();
        }
    }
}
