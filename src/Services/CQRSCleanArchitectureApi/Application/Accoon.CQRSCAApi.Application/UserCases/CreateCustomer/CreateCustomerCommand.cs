using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.CQRSCAApi.Application.UserCases.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<CustomerCreated>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}

//https://app.pluralsight.com/player?course=clean-architecture-patterns-practices-principles&author=matthew-renze&name=clean-architecture-patterns-practices-principles-m3&clip=3&mode=live

//https://github.com/mmacneil/CleanAspNetCoreWebApi/blob/master/src/Web.Api.Core/UseCases/LoginUseCase.cs

//https://alexisalulema.com/2018/04/30/ddd-clean-architecture-template/

//https://github.com/alulema/DDD-CleanArchitectureTemplate/tree/master/CleanDds.Application.CommandStack/Rates

//https://hackernoon.com/applying-clean-architecture-on-web-application-with-modular-pattern-7b11f1b89011

//https://github.com/ivanpaulovich/clean-architecture-manga/blob/master/manga/src/Manga.Application/UseCases/Register/RegisterUseCase.cs

//https://github.com/JasonGT/NorthwindTraders/blob/master/Northwind.Application/Customers/Commands/CreateCustomer/CreateCustomerCommand.cs

//https://www.youtube.com/watch?v=Zygw4UAxCdg