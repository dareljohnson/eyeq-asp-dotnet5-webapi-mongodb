using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using API.Data;
using API.Middleware;
using Microsoft.Extensions.Options;


namespace API
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
            services.Configure<MusicDatabaseSettings>(
                Configuration.GetSection(nameof(MusicDatabaseSettings)));

            services.AddSingleton<IMusicDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MusicDatabaseSettings>>().Value);

            services.AddTransient<IMusicService, MusicService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

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
