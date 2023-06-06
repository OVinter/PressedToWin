
using HttpRequestsLibrary;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace CanonPP.Concurs
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

            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            


            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });




            services.AddHealthChecks();



            services.AddMvcCore()
            .AddApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.DocInclusionPredicate((version, apiDescription) =>
                {
                    var values = apiDescription.RelativePath.Split('/').Skip(2);

                    apiDescription.RelativePath = $"api/{version}/{string.Join("/", values)}";

                    var versionParameter = apiDescription.ParameterDescriptions.SingleOrDefault(p => p.Name == "version");

                    if (versionParameter != null)
                    {
                        apiDescription.ParameterDescriptions.Remove(versionParameter);
                    }

                    foreach (var parameter in apiDescription.ParameterDescriptions)
                    {
                        parameter.Name = char.ToLowerInvariant(parameter.Name[0]) + parameter.Name.Substring(1);
                    }

                    return true;
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
            });

                c.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "PressedToWin API v1.0",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });
                app.UseHsts();
            }


            // Add prefix for FrontDoor
            app.UsePathBase(new PathString("/cpm"));

            app.UseRouting();

            app.UseAuthentication();

            app.UseHttpsRedirection();

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthcheck");

                // When Always On is turned on, the front-end load balancer sends a GET request to the application root every five minutes. 
                // https://docs.microsoft.com/en-us/azure/app-service/configure-common?tabs=portal
                // without this root response, there would be a 404 error thrown
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("OK");
                });
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "PressedToWin API v1.0");
                c.DocumentTitle = "PressedToWin API v1.0";
            });
        }


    }
}
