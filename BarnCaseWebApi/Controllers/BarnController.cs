using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarnCaseWebApi.Data;
using BarnCaseWebApi.Models;
using System.Security.Claims;

namespace BarnCaseWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BarnController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BarnController(AppDbContext context) { _context = context; }

        [HttpPost("buy/{type}")]
        public async Task<ActionResult> Buy(string type)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var user = await _context.Users.FindAsync(userId);

            decimal cost = type switch
            {
                "inek" => 50,
                "tavuk" => 20,
                "koyun" => 40,
                "keçi" => 35,
                "ördek" => 25,
                "geyik" => 75,
                "at" => 150,
                "arı" => 80,
                "boğa" => 120,
                "tavşan" => 30,
                _ => 1000
            };

            if (user.Balance < cost) return BadRequest("Para yetersiz!");
            user.Balance -= cost;

            var newAnimal = new Animal
            {
                Type = type,
                Price = cost,
                ProductPrice = cost * 0.2m,
                BirthDate = DateTime.Now,
                LifespanInSeconds = 40,
                UserId = userId
            };

            _context.Animals.Add(newAnimal);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("status")]
        public async Task<ActionResult> GetStatus()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var user = await _context.Users.Include(u => u.Animals).FirstOrDefaultAsync(u => u.Id == userId);

            var now = DateTime.Now;
            var animalList = user.Animals.Select(a => new {
                a.Id,
                a.Type,
                a.ProductCount,
                a.LifespanInSeconds,

                BirthDate = a.BirthDate.ToString("yyyy-MM-ddTHH:mm:ss")

            });

            return Ok(new { balance = user.Balance, username = user.Username, animals = animalList });
        }

        [HttpPost("sell-product/{id}")]
        public async Task<ActionResult> SellProduct(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var user = await _context.Users.FindAsync(userId);
            var animal = await _context.Animals.FindAsync(id);

            
            if (animal == null || animal.ProductCount <= 0)
                return BadRequest("Ürün yok!");

            user.Balance += animal.ProductPrice;

            animal.ProductCount -= 1;

            await _context.SaveChangesAsync();

            return Ok(new { newBalance = user.Balance, remainingProduct = animal.ProductCount });
        }
        [HttpDelete("sell-animal/{id}")]
        public async Task<ActionResult> SellAnimal(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null) return NotFound();

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var user = await _context.Users.FindAsync(userId);

            user.Balance += (animal.Price * 0.5m);

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] string newUsername)
        {
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

            var userId = int.Parse(userIdString);
            var user = await _context.Users.FindAsync(userId);

            if (user == null) return NotFound("Kullanıcı bulunamadı!");

            user.Username = newUsername; 
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}