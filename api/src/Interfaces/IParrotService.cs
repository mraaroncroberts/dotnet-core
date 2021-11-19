namespace Api.Interfaces
{    
    using Serilog;

    public interface IParrotService 
    {        
        string Speak(string message); 
    }    
}
