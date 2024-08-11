using AirCoil_API.Dto;
using AirCoil_API.Models;
using AutoMapper;

namespace AirCoil_API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarDto>();
            CreateMap<Province, ProvinceDto>().ReverseMap();
            CreateMap<CreateProvinceDto, Province>();
            CreateMap<Model, ModelDto>().ReverseMap();
            CreateMap<CreateModelDto, Model>();
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<CreateBrandDto, Brand>();
        }
    }
}
