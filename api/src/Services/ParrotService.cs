namespace Api.Services
{    
    using Api.Interfaces;
    using Serilog;

    public class ParrotService : IParrotService
    {       
        private readonly ILogger _logger;

        public ParrotService(ILogger logger)
        {
            _logger = logger;
        }

        public string Speak(string message)
        {   
            _logger.Information("Parrot here!");         

            return $"{message}";
        }
    }
}
