using System.Linq;
using KpiPractice.Core.Shops;
using KpiPractice.Core.Goods;
using KpiPractice.Data;
using KpiPractice.Data.Shops;
using KpiPractice.Data.Goods;
using KpiPractice.Orchestrators.Shops;
using KpiPractice.Orchestrators.Goods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace kpi_practice_onion
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
            services.AddAutoMapper(typeof(ShopDaoProfile),  typeof(GoodDaoProfile),
                typeof(ShopOrchProfile), typeof(GoodOrchProfile));
            string connString = "Host=localhost;Port=5432;Database=ShopDb;Username=postgres;Password=123546";
            services.AddDbContext<ShopContext>(options => options.UseNpgsql(connString));

            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddScoped<IShopService, ShopService>();

            services.AddScoped<IGoodService, GoodService>();
            services.AddScoped<IGoodRepository, GoodRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "kpi_practice_onion", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "kpi_practice_onion v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}