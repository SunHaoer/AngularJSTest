using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AngularTest.Models;
using AngularTest.Data;
using System;
using AngularTest.Filter;

namespace AngularTest
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
            services.AddDbContext<PhoneContext>(opt =>
                         opt.UseInMemoryDatabase("PhoneList"));
            services.AddDbContext<BrandTypeContext>(opt =>
                         opt.UseInMemoryDatabase("BrandTypeList"));
            services.AddDbContext<BrandModelContext>(opt =>
                         opt.UseInMemoryDatabase("BrandModelList"));
            services.AddDbContext<TypeYearContext>(opt =>
                         opt.UseInMemoryDatabase("TypeList"));
            services.AddDbContext<UserContext>(opt =>
                         opt.UseInMemoryDatabase("UserList"));
            services.AddDbContext<BrandTypeProductNoContext>(opt =>
                         opt.UseInMemoryDatabase("BrandTypeProductNoList"));
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(60000);
                options.Cookie.HttpOnly = true;
            });
            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(typeof(LoginFilter));
            //});
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(LoginFilter));
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
                app.UseHsts();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseSession();

            app.UseMvc();
        }
    }
}
