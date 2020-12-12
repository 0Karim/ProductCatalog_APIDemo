using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArc.API.Models;
using CleanArc.Context;
using CleanArch.Models.Entities;
using CleanArch.Repositories.Repository;
using CleanArch.Services.Interfaces;
using CleanArch.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace CleanArc.API
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
            #region Configure Database

            services.AddDbContext<CleanArchDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            #endregion

            services.AddControllers();

            #region Register Services

            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IProductService, ProductService>();

            #endregion

            #region Logger

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<Product>>();
            services.AddSingleton(typeof(ILogger), logger);

            #endregion

            #region Auto Mapper Config

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion

            #region Configure Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Swagger Demo",
                    Description = "Swagger Demo for ProductController",
                    TermsOfService = new Uri("https://localhost:44331/api/product"),
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Name = "Karim Alaa",
                        Email = "karimalaa298@gmail.com",
                        Url = new Uri("https://localhost:44331/api/product"),
                    }
                });
            });


            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
