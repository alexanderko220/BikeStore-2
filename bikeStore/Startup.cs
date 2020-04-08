using bikeStore.Data;
using bikeStore.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using BikeStore.Models.Bikes;
using BikeStore.Data.Repository;
using BikeStore.Models.Categories;
using BikeStore.Data.Entities;
using BikeStore.Models.Dictionaries;
using BikeStore.Models.Specifications;
using bikeStore.Data.Entities;
using Microsoft.AspNetCore.Http.Features;

namespace bikeStore
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
            
            services.AddDbContext<StoreDbContext>(cfg => { cfg.UseSqlServer(Configuration.GetConnectionString("StoreConnectionString")); });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddJsonOptions( options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            #region Dependency Injection Config
            services.AddScoped(typeof(IRepo<>), typeof(BaseRepo<>));
            services.AddScoped(typeof(IRepo<Color>), typeof(BaseRepo<Color>));
            services.AddScoped(typeof(IRepo<Size>), typeof(BaseRepo<Size>));
            services.AddScoped<IBikeRepository, BikeRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISpecificationRepository, SpecificationRepository>();
            services.AddScoped(typeof(IRepo<SpecificationCategory>), typeof(BaseRepo<SpecificationCategory>));
            services.AddScoped(typeof(IRepo<BikesColors>), typeof(BaseRepo<BikesColors>));
            services.AddScoped(typeof(IRepo<BikesSizes>), typeof(BaseRepo<BikesSizes>));
            #endregion

            #region Mapper Config
            //services.AddAutoMapper(typeof(MappingProfile));
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BikeProfile());
                mc.AddProfile(new CategoryProfile());
                mc.AddProfile(new CategoryDictionaryProfile());
                mc.AddProfile(new ColorProfile());
                mc.AddProfile(new SizeProfile());
                mc.AddProfile(new SpecificationCategoryProfile());
                mc.AddProfile(new SpecificationProfile());
            });
            #endregion

            services.AddSingleton(mappingConfig.CreateMapper());

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            // To avoid the MultiPartBodyLength error
            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = 1024;
                o.MultipartBodyLengthLimit = 2500000;
                o.MemoryBufferThreshold = 1024 * 1024 * 4;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
