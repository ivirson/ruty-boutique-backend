using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Store.BLL.Domain;
using Store.Data;

namespace Store.API
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Loja Virtual Ruty Boutique",
                        Version = "v1",
                        Description = "API REST criada com o ASP.NET Core para o projeto de uma Loja Virtual",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Ivirson Daltro",
                            Url = new Uri("https://github.com/ivirson/ruty-boutique-backend")
                        }
                    });

                string applicationPath =
                    PlatformServices.Default.Application.ApplicationBasePath;
                string applicationName =
                    PlatformServices.Default.Application.ApplicationName;
                string xmlDocPath =
                    Path.Combine(applicationPath, $"{applicationName}.xml");

                c.IncludeXmlComments(xmlDocPath);
            });

            services.AddScoped<DataContext>();
            services.AddScoped<ProductsBLL>();
            services.AddScoped<ErrorLogBLL>();
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ruty Boutique");
            });
        }
    }
}
