using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCoreFundamentals.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreFundamentals.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NetCoreFundamentals.Helpers;

namespace NetCoreFundamentals
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
            // Guardar Respuestas de Peticion en Cache
            services.AddResponseCaching();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();
            services.AddScoped<MiFiltroDeAccion>();
            services.AddTransient<ClaseB>();
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers()
               .AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
               );

            services.AddMvc(options =>
            {
                options.Filters.Add(new MiFiltroDeExcepcion());
                // Si hubiese inyeccion de dependencia en el filtro
                // options.Filters.Add(typeof(MiFiltroDeExcepcion));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
               
        }
        // Middleware
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

            app.UseResponseCaching();
            app.UseAuthentication();
            //app.UseMvc();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
          
        }
    }
}
