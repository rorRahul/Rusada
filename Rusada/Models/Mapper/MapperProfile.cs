using Rusada.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
namespace Rusada.Models.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<AirlistViewModel, AirList>();
        }
    }
}