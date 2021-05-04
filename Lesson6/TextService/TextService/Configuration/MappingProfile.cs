using AutoMapper;
using DatabaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextClient.Entity;

namespace TextService.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Text, TextFile>();
        }
    }
}
