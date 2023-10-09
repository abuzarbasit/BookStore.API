using BookStore.API.Data;
using BookStore.API.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BookStore.API
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

            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BookStoreDB")));

            



            services.AddControllers();
            

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Configure error handling and other middleware for production.
            }

            // Configure routing, authentication, and other middleware here.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // If you're using controllers in your Web API.
            });
        }
    }
}
