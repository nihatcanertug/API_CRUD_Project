using API_CRUD_Project.Models;
using API_CRUD_Project.Models.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD_Project.DataAccess.Mapper
{
    public class BoxerMapper:Profile
    {
        // The mapping of our data transfer objects created with the asset on a job basis is done very easily with the AutoMapper class. We use the CreateMap () method for this process. If you examine this method closely, the first parameter is the source code, in our example, the "Boxer.cs" class is the destination code, the "BoxerDto.cs" class.

        // However, the property names in the source code and the property names in destinion source, ie in Dto, must be the same. Otherwise, we have to do the mapping process, that is, mapping them one by one. In this process, we will use the ReverseMap () method in AutoMapper.

        public BoxerMapper() => CreateMap<Boxer, BoxerDto>().ReverseMap();
    }
}
