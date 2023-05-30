using System.Net;
namespace api_cegeka.Services;

public class GreetingService
{
    public string greeting(string name = "")
    {
        if(string.IsNullOrEmpty(name))
        {
            return "Hello World!";
        }
        else
        {
            return $"Hello {name}";
        }
        
    }
    
}