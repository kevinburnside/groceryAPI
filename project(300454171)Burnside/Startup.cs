using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using project300454171Burnside.Models;

namespace project_300454171_Burnside
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
            //services.AddDbContext<GroceryContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("GroceryContext")));
  
            services.AddMvc();

            // Register the swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info {
                    Title = "Grocery API",
                    Version = "v1",
                    Description = "An API to create a grocery list that a can be shared with other users",
                    TermsOfService = "We take all your data and sell it",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "Kevin Burnside", Email = "kevinburnside15@gmail.com" }
                });               
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Grocery API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
