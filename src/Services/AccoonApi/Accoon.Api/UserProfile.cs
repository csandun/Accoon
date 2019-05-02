﻿using Accoon.Api.BussinessServices.Entities.EntityDTOs;
using Accoon.Api.DataServices.Entities.CustomEntities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accoon.Api
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<CustomerDto, CustomerEntity>();
            CreateMap<CustomerEntity, CustomerDto>();
        }
    }
}
