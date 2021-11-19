//
// Create the app builder
var builder = WebApplication.CreateBuilder(args);

// Create a logger
// Log.Logger = new LoggerConfiguration()
//     .WriteTo.Console()
//     .CreateBootstrapLogger();

// Configure our services and pipeline
builder.Services.AddServiceCollectionConfig();

try
{
    //
    // Configure Serilog 
    builder.Host.UseSerilog((webBuilderContext, serviceProvider, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(webBuilderContext.Configuration)                                
        .ReadFrom.Services(serviceProvider));          

    //
    //  Host configuration    
    builder.Host.ConfigureHostOptions(hostOptions =>{
        // Wait 30 seconds for graceful shutdown
        hostOptions.ShutdownTimeout = TimeSpan.FromSeconds(30);    
    });         

    //
    // Build the app
    var app = builder.Build();

    // Use our app config
    app.UseApplicationConfig();

    //
    // home
    app.MapGet("/", () => {          
        return "Welcome to dotnet core!";
    });

    //
    // parrot route
    app.MapGet("/parrot",  (IParrotService service, IHttpContextAccessor context) => {        
        string agent = context.HttpContext != null ? context.HttpContext.Request.Headers["User-Agent"] : "UNKNNOW-AGENT";               
        return service.Speak($"Hello, {agent}.  It is: {DateTime.Now.ToShortTimeString()}");
    });

    // app.MapDefaultControllerRoute();
    // app.MapRazorPages();

    Log.Information("{Assembly} configured! Running: {Environment}",
        System.Reflection.Assembly.GetExecutingAssembly().GetName(), 
        app.Environment.EnvironmentName);

    app.Run();
}
catch(Exception error)
{
    Log.Fatal(error, "Unable to launch Application!");
}
finally
{
    Log.Information("Application shutdown!");
    Log.CloseAndFlush();
}