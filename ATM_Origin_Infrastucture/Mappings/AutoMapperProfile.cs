using ATM_Origin.Core.DTOs;
using ATM_Origin.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_Origin_Infrastucture.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Tarjeta, TarjetaDto>();
            CreateMap<TarjetaDto, Tarjeta>();
            CreateMap<Operaciones, OperacionesDto>();
            CreateMap<OperacionesDto, Operaciones>();
        }
    }
}
