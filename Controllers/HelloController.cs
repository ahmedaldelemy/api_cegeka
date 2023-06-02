using api_cegeka.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_cegeka.Controllers;

[ApiController]
[Route("[controller]")]
public class HelloController : ControllerBase
{
    
    private readonly GreetingService gs;
    
    public HelloController(GreetingService gs)
    {
        this.gs = gs;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> Hello()
    {
        return Ok(gs.greeting());
    }
    
    [HttpGet("v2")]
    public async Task<IActionResult> Hellov2(string name)
    {
        return Ok(gs.greeting(name));
    }
}