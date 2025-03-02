using AutoMapper;
using ProductService.Entities;
using ProductService.EntityDTOs.Category;
using ProductService.EntityDTOs.SubCategory;
using ProductService.EntityDTOs.Product;

namespace ProductService.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category mappings
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();

            // SubCategory mappings
            CreateMap<SubCategory, SubCategoryDto>().ReverseMap();
            CreateMap<SubCategory, SubCategoryCreateDto>().ReverseMap();
            CreateMap<SubCategory, SubCategoryUpdateDto>().ReverseMap();

            // Product mappings
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.SubCategoryIds, opt => opt.MapFrom(src => src.SubCategories.Select(sc => sc.Id)))
                .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.SubCategories))
                .ReverseMap()
                .ForMember(dest => dest.SubCategories, opt => opt.Ignore());
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
        }
    }
}