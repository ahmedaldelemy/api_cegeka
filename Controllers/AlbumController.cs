using api_cegeka.Data;
using Microsoft.AspNetCore.Mvc;
using api_cegeka.Models;
using Microsoft.EntityFrameworkCore;
namespace api_cegeka.Controllers;

[ApiController]
[Route("[controller]")]
public class AlbumController : ControllerBase
{
    private readonly ILogger<AlbumController> _logger;
    private readonly ApiDbContext _context;

    public AlbumController(ILogger<AlbumController> logger, ApiDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetAllCustomers")]
    public async Task<IActionResult> Get(){
        var allAlbums = await _context.Albums.ToListAsync();
        return Ok(allAlbums);
    }
    
    // [HttpGet("{id}", Name = "GetAlbumById")]
    // public async Task<IActionResult> Get(int id){
    //     var album = await _context.Albums.FindAsync(id);
    //     if (album == null)
    //     {
    //         return NotFound();
    //     }
    //     return Ok(album);
    // }
}