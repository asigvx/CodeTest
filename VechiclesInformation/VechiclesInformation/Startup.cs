using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using VechiclesInformation.Models;
using VechiclesInformation.Repositories;
using Microsoft.AspNetCore.Authentication;
using VechiclesInformation.Integrations;

namespace VechiclesInformation
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
            services.AddTransient<IVehicleRepository, VehicleRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISyncService, SyncService>();
            services.AddTransient<BasicAuthenticationHandler>();
            services.AddTransient<UserService>();
            
            var sqlconn = Configuration.GetConnectionString("DefaultConnection");
            services.AddEntityFrameworkSqlite().AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlite(sqlconn);
            });

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(AppExceptionFilter));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VechiclesInformation", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "UsersInformation", Version = "v2" });
                c.SwaggerDoc("v3", new OpenApiInfo { Title = "CustomerInformation", Version = "v3" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationContext context)
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VechiclesInformation v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
