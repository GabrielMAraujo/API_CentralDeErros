using API_CentralDeErros.Model.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CentralDeErros.Model.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Alert, AlertDTO>().ReverseMap();
        }
    }
}
