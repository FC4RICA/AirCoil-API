﻿using AirCoil_API.Dto;
using AirCoil_API.Models;
using AutoMapper;

namespace AirCoil_API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarDto>().ReverseMap();
            CreateMap<CreateCarDto, Car >();
            CreateMap<Province, ProvinceDto>().ReverseMap();
            CreateMap<CreateProvinceDto, Province>();
            CreateMap<Model, ModelDto>().ReverseMap();
            CreateMap<CreateModelDto, Model>();
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<CreateBrandDto, Brand>();
            CreateMap<ServiceCenter, ServiceCenterDto>().ReverseMap();
            CreateMap<CreateServiceCenterDto, ServiceCenter>();
            CreateMap<Branch, BranchDto>().ReverseMap();
            CreateMap<CreateBranchDto, Branch>();
            CreateMap<Job, JobDto>().ReverseMap();
            CreateMap<Result, ResultDto>().ReverseMap();
            CreateMap<CreateResultDto, Result>();
        }
    }
}
