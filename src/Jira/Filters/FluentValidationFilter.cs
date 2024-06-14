using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Jira.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebUtilities;

namespace Jira.Filters
{
    public class FluentValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                
                var errors = context.ModelState
                    .Where(m => m.Value.Errors.Count > 0)
                    .SelectMany(m => m.Value.Errors
                        .Select(e => new ErrorDetails
                        {
                            ErrorCode = "Key_Invalid" + m.Key,
                            ErrorMessage = e.ErrorMessage,
                        }))
                        .ToList();

                var responseObj = new
                {
                    Message = "Bad Request",
                    errors = errors
                };

                context.Result = new JsonResult(responseObj)
                {
                    StatusCode = 400
                };
            }
        }

        public class Errors
        {
            public string Key { get; set; }
            public string Description { get; set; }

        }
    }
}