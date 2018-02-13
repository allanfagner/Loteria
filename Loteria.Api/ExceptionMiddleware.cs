using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loteria.Api
{
    //Apenas para efeito de demonstração todas as exceções são tradadas da mesma forma e retornam texto puro
    //Em produção este middleware pode ter tramento diferenciado para exceções específicas
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
