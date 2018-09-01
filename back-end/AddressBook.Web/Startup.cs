using AddressBook.DataRepository;
using AddressBook.Web.Configs;
using AddressBook.Web.ErrorHandlers;
using AddressBook.Web.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace AddressBook.Web
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
            services.AddCors(o =>
            {
                o.AddPolicy("Everything", p =>
                {
                    p.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });

            // Set up database context
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Set up dependency injections
            DiConfiguration.SetUp(services);

            // Add SignalR
            services.AddSignalR();

            // Set up Mvc
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Http Error Handler
            app.UseHttpStatusCodeExceptionMiddleware();

            app.UseCors("Everything");

            // This will serve wwwroot/index.html when path is '/'
            app.UseDefaultFiles();

            // This will serve js, css, images etc.
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=3600";
                }
            });

            // This ensures index.html is served for any requests with a path
            // and prevents a 404 when the user refreshes the browser
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.HasValue &&
                    context.Request.Path.Value != "/" &&
                    !context.Request.Path.Value.StartsWith("/api") &&
                    !context.Request.Path.Value.StartsWith("/hub"))
                {
                    context.Response.ContentType = "text/html";

                    await context.Response.SendFileAsync(
                        env.ContentRootFileProvider.GetFileInfo("wwwroot/index.html")
                    );

                    return;
                }

                await next();
            });

            // Use SignalR
            app.UseSignalR(routes =>
            {
                routes.MapHub<TagHub>("/hub/tags");
            });

            // Use Mvc
            app.UseMvc();
        }
    }
}
