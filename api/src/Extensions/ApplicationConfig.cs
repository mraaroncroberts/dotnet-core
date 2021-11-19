namespace Microsoft.Extensions.DependencyInjection
{
    /// <sumary>
    /// ApplicationConfig extension
    /// </summary>
    public static class ApplicationConfig
    {
        /// <summary>
        /// Configures our Application
        /// </summary/>
        /// <remarks>access config via  IConfiguration config</remarks>
        public static WebApplication UseApplicationConfig(this WebApplication app)
        {     
            //
            // Use Serilog
            //   
            // Write streamlined request completion events, instead of the more verbose ones from the framework.
            // To use the default framework request logging instead, remove this line and set the "Microsoft"
            // level in appsettings.json to "Information".
            app.UseSerilogRequestLogging();
            
            // app.UseHttpsRedirection();                            
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseCors(p =>
            {
                p.AllowAnyOrigin();
                p.WithMethods("GET");
                p.AllowAnyHeader();
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // app.UseExceptionHandler("/Error");

                //
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.                
                // app.UseHsts();
                app.UseSwagger(options => {                                
                    // Swagger Options here
                });
                app.UseSwaggerUI(options => {
                    // Swagger UI Options here
                });
            }

            return app;
        }
    }
}

