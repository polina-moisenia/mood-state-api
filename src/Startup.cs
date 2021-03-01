using System.Net.Http;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MoodStateApi.Services;

namespace MoodStateApi {
    public class Startup {
        private const string CORS_POLICY = "DefaultPolicy";
        public const string CORS_ANY_ORIGIN_POLICY = "AllowPostAnyOrigin";

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers()
            .AddJsonOptions(options => {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }));

            services.AddSingleton<IService, Service>();


            services.AddCors(o => o.AddPolicy(CORS_POLICY, b => b.AllowAnyOrigin()));
            services.AddCors(o => o.AddPolicy(CORS_ANY_ORIGIN_POLICY, b => {
                b.AllowAnyOrigin()
                    .WithMethods(HttpMethod.Get.Method, HttpMethod.Post.Method, HttpMethod.Options.Method)
                    .AllowAnyHeader();
            }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
            app.UseCors(CORS_ANY_ORIGIN_POLICY);

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
        }
    }
}