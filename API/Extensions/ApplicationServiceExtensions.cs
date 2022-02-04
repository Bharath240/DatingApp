using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config){


            //Added DataContext as a service and passing options using lamda expression
            //To connect our application to the database, we are passing our connection in Sqlite as below
            //Connections are provided in configuration files (appsettings.json/appsettings.Development.json)
            services.AddDbContext<DataContext>(options => {
                
                options.UseSqlite(config.GetConnectionString("DefaultConnection")); //setting up our connection
                //Now, we can access our DB from the above connection
                
            });

            services.AddScoped<ITokenInterface, TokenService>();

            return services;

        }
    }
}