using Accoon.BuildingBlocks.Common.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.BuildingBlocks.Common.Filters
{
    public class PaginationOptionFilter :  IActionFilter
    {
        private readonly PaginationOption paginationOption;
        public PaginationOptionFilter(IOptions<PaginationOption> option)
        {
            this.paginationOption = option.Value;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            object page = null;
            context.ActionArguments.TryGetValue("Page", out page);
            if ((PaginationOption)page == null)
            {
                context.ActionArguments["Page"] = this.paginationOption.Page;
            }
        }
    }
}
