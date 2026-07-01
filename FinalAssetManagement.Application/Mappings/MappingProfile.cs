using AutoMapper;
using FinalAssetManagement.Application.DTOs.Asset;
using FinalAssetManagement.Application.DTOs.Category;
using FinalAssetManagement.Application.DTOs.Transaction;
using FinalAssetManagement.Application.DTOs.User;
using FinalAssetManagement.Core.Entities;

namespace FinalAssetManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap<Source, Destination>();
            // Database works only with Entities, so data from DB is mapped from Entity => DTO.
            // Data received from client as DTO must be mapped to Entity before saving to database.

            //========================================================
            //                   Asset Mapping
            //========================================================

            CreateMap<Asset, AssetDto>()
                     .ForMember(dest => dest.CategoryName,
                      opt => opt.MapFrom(src => src.Category.Name))
                     .ForMember(dest => dest.UserName,
                      opt => opt.MapFrom(src =>
                                        src.User != null ? src.User.UserName : null));

            CreateMap<Asset, AssetDetailsDto>();

            //========================================================
            //                 Category Mapping
            //========================================================

            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryDetailsDto>();

            //========================================================
            //                   User Mapping
            //========================================================

            CreateMap<User, UserDto>();
            CreateMap<User, UserDetailsDto>();

            //========================================================
            //                 Transaction Mapping
            //========================================================

            CreateMap<Transaction, TransactionDto>()
                .ForMember(dest => dest.TransactionType,
                                   opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.AssetName,
                                   opt => opt.MapFrom(src => src.Asset.Name));

            CreateMap<Transaction, TransactionDetailsDto>()
                .ForMember(dest => dest.TransactionType,
                           opt => opt.MapFrom(src => src.Type.ToString()));

            //========================================================
        }
    }
}
