using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AngularTest.Models;
using AngularTest.Data;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc().AddJsonOptions(
                            json => {
                                json.SerializerSettings.DateFormatString = "yyyy-mm-dd";
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
            app.UseMvc();
        }
    }
}
