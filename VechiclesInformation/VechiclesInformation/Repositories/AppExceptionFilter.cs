using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.Threading.Tasks;

namespace VechiclesInformation.Repositories
{
    public class AppExceptionFilter : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            //0001 -> Global error
            context.Result = new ObjectResult(new { id = "0001", error = context.Exception.Message, currentDate = DateTime.Now })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
            return base.OnExceptionAsync(context);
        }
    }
}
