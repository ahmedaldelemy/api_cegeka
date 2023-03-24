using api_cegeka.Data;
using Microsoft.AspNetCore.Mvc;
using api_cegeka.Models;
using Microsoft.EntityFrameworkCore;

namespace api_cegeka.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{

    private readonly ILogger<CustomerController> _logger;
    private readonly ApiDbContext _context;

    public CustomerController(ILogger<CustomerController> logger, ApiDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetAllCustomers")]
    public async Task<IActionResult> Get(){
            var allCustomers = await _context.Customers.ToListAsync();
            return Ok(allCustomers);
    }
}
