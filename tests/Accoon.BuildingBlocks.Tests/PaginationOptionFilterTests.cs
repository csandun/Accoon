using Accoon.BuildingBlocks.Common.Entities;
using Accoon.BuildingBlocks.Common.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Accoon.BuildingBlocks.Tests
{
    public class PaginationOptionFilterTests
    {
        private static ActionExecutingContext CreateContext(PaginationOption option)
        {
            var actionContext = new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor());
            var actionArguments = new Dictionary<string, object> { { "option", option } };
            return new ActionExecutingContext(actionContext, new List<IFilterMetadata>(), actionArguments, controller: null);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, -5)]
        public void Filter_Should_Set_Defaults_When_Page_Or_Size_Invalid(int page, int size)
        {
            var defaults = new PaginationOption { Page = 1, Size = 10 };
            var filter = new PaginationOptionFilter(Options.Create(defaults));
            var param = new PaginationOption { Page = page, Size = size };
            var context = CreateContext(param);

            filter.OnActionExecuting(context);

            Assert.Equal(defaults.Page, param.Page);
            Assert.Equal(defaults.Size, param.Size);
        }
    }
}
