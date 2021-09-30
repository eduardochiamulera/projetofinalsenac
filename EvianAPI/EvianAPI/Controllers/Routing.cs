using Evian.Entities.Base;
using Evian.Repository.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace EvianAPI.Controllers
{
   public static class Routing
    {
        public static void Include(IApplicationBuilder app)
        {

            app.UseMvc(routes =>
            {
                routes.MapRoute(null, "", new
                {
                    Controller = "",
                    action = ""
                });
            });
        }
    }
}
