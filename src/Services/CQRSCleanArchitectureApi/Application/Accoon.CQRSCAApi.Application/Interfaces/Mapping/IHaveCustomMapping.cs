using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.CQRSCAApi.Application.Interfaces.Mapping
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configuration);
    }
}
