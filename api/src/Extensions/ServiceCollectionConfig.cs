namespace Microsoft.Extensions.DependencyInjection
{   
    using Microsoft.OpenApi.Models;
    
    /// <sumary>
    /// The Serice Collection Config extension to wire in our services
    /// </summary>
    public static class ServiceCollectionConfig
    {
        /// <summary>
        /// Adds the Services into the IServiceCollection
        /// </summary/>
        /// <remarks>access config via  IConfiguration config</remarks>
        public static IServiceCollection AddServiceCollectionConfig(this IServiceCollection services)
        {     
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddCors(); 

            //
            // Dependent Services
            services.AddHttpContextAccessor();               

            //
            // Add our services            
            services.AddScoped<IParrotService, ParrotService>(); 

            //
            // Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });                   
            });
              
            return services;
        }
    }
}
