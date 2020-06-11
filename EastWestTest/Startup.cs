using EastWestTest.Repository.Abstract;
using EastWestTest.Repository.Context;
using EastWestTest.Service.Abstract;
using EastWestTest.Service.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace EastWestTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionString"], t =>
                {
                    t.MigrationsAssembly("EastWestTest.Repository");
                });
            });

            services.AddTransient<IUnitOfWork, DataContext>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<ISaleService, SaleService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "API",
                    Description = "EastWest API."
                });
                var filePath = Path.Combine(AppContext.BaseDirectory, "EastWestTest.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            InitDatabase(app.ApplicationServices);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
            app.UseMvc();
        }

        private static void InitDatabase(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dataContext = services.GetService<DataContext>();

                Initializer.ApplyMigrations(dataContext);
                Repository.SeedTemplate.Fill(dataContext);
            }
        }
    }
}
