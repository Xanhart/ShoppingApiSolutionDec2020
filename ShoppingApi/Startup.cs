using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShoppingApi.Data;
using ShoppingApi.Profiles;
using ShoppingApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddScoped<ILookupProducts, EfSqlProducts>();
            services.AddScoped<ICommandProducts, EfSqlProducts>();
            services.AddDbContext<ShoppingDataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("shopping"));
            });
            var mapperConfiguration = new MapperConfiguration(opt =>
            {
               opt.AddProfile(new ProductsProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();

            services.AddSingleton<IMapper>(mapper);
            services.AddSingleton<MapperConfiguration>(mapperConfiguration);


            // this is to get the markup percentage and apply it to the cost of the product. Kinda wonky but 
            var configForPringing = new ConfigurationForPricing();
            Configuration.GetSection(configForPringing.SectionName).Bind(configForPringing);
            services.Configure<ConfigurationForPricing>(Configuration.GetSection(configForPringing.SectionName));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
