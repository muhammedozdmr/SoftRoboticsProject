using AutoMapper;
using SoftRobotics.Domain;
using SoftRobotics.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRobotics.Business
{
    public class RandomWordMappingProfile : Profile
    {
        public RandomWordMappingProfile()
        {
            CreateMap<RandomWord,RandomWordDto>();
            CreateMap<RandomWordDto,RandomWord>();
        }
    }
}
