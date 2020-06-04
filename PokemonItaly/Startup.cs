using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PokemonItaly.Interface.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using PokemonItaly.Service.Services;
using PokemonItaly.Data.Interfaces;
using PokemonItaly.Data.Repository;
using PokemonItaly.API.Extensions;
using PokemonItaly.API.Filters;

namespace PokemonItaly
{
    public class Startup
    {
        #region Declaration
        public IPokemonService pokemonService;
        public IPokemonRepository  pokemonRepository;
        public ITranslatorRepository translatorRepository;
        public IConfiguration Configuration { get; }

        #endregion

        #region Constructor
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

           
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));


            services.AddSwaggerGen(c =>
            {
                var swaggerOption = new SwaggerOptions();
                var section = Configuration.GetSection("Swagger");
                section.Bind(swaggerOption);
            });

            services.AddScoped<ActionFilter>();

            services.AddSingleton<IPokemonService, PokemonService>();
            services.AddHttpClient<IPokemonRepository, PokemonRepository>();
            services.AddHttpClient<ITranslatorRepository, TranslatorRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureExceptionMiddleware();

            app.UseCors("ApiCorsPolicy");

            //app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Italy Software Challenge API");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
