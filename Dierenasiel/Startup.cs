using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dierenasiel.Controllers;
using Dierenasiel.Data;
using Dierenasiel.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Dierenasiel
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
            // Hier geef je mee welke database je wenst te gebruiken voor welke EntityContext. Mocht je meer dan één database
            // hebben kan je hier meer als één context voeren (met verschillende connectie-instellingen)
            services.AddDbContext<EntityContext>(db => db.UseInMemoryDatabase("AnimalDatabase"));

            // Hier voeg je je eigen services toe, in het formaat "interface, class". Het DI-framework dat in ASP.NET zit handelt
            // het aanmaken van de objecten dan af.
            services.AddScoped<IAnimalService, AnimalDataService>();
            services.AddScoped<ITranslationService, TranslationService>();

            services.AddMvc();

            // Swagger-support toevoegen (http://swagger.io).
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Dierenasiel", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, EntityContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

                // Database initialisatie; anders is het ons overzicht scherm maar leeg :o).
                DatabaseInitializer.Initialize(context);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // Swagger(UI) toevoegen
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dierenasiel V1");
            });

            // Basis routing informatie. Dit maakt een default route aan. 
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });



        }
    }
}
