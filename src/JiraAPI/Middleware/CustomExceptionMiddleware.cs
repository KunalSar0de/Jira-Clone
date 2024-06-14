using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Jira.Exceptions;

namespace JiraAPI.Middleware
{
    public class CustomExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                string finalExResult = string.Empty;
                int statusCode = 500;

                if(ex is ThrowValidationException)
                {   
                    var validationEx = (ThrowValidationException)ex;

                    if(validationEx.Errors.Count > 0)
                    {
                        var listOfErrors = new List<ErrorDetails>();
                        foreach(var error in validationEx.Errors.AllKeys)
                        {
                            var listErrorMessage = new ErrorDetails{
                                ErrorCode = error,
                                ErrorMessage = GetErrorMessage(error)
                            };

                            listOfErrors.Add(listErrorMessage);
                        }

                        finalExResult = JsonSerializer.Serialize(listOfErrors);
                    }else
                    {
                        var errorMessage = new ErrorDetails{
                            ErrorCode = validationEx.Message,
                            ErrorMessage = GetErrorMessage(validationEx.Message)
                        };

                        finalExResult = JsonSerializer.Serialize(errorMessage);
                    }

                    statusCode = finalExResult.Contains("notfound", StringComparison.OrdinalIgnoreCase) 
                        ? (int)HttpStatusCode.NotFound 
                        : (int)HttpStatusCode.BadRequest;
                    
                }
                else
                {
                    var errorMessage = new ErrorStackDetails{
                            ErrorMessage = ex.Message,
                            StackTrace = ex.StackTrace
                            
                        };

                    finalExResult = JsonSerializer.Serialize(errorMessage);
                }

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(finalExResult);
            }
        }

        private string GetErrorMessage(string error)
        {
            if(string.IsNullOrEmpty(error))
            {
                return string.Empty;
            }else
            {
                var split = error.Split('_');
                var text = split[1];
                
                var newText = new StringBuilder(text.Length * 2);
                newText.Append(text[0]);

                for (int i = 1; i < text.Length; i++)
                {
                    if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                    {
                        newText.Append(' ');
                    }
                    newText.Append(text[i]);
                }

                return newText.ToString();
            }
        }
    }
}