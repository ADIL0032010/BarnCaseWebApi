using BarnCaseWebApi.Data;
using BarnCaseWebApi.DTOs;
using BarnCaseWebApi.Models;
using BarnCaseWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace BarnCaseWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthController(AppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegisterDto request)
        {
            if (_context.Users.Any(u => u.Username == request.Username)) return BadRequest("Bu isim alınmış!");
            using var hmac = new HMACSHA512();
            var user = new User
            {
                Username = request.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
                PasswordSalt = hmac.Key,
                Balance = 100
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("Kayıt başarılı.");
        }

        [HttpPost("login")]
        public async Task<ActionResult<object>> Login(UserRegisterDto request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);
            if (user == null) return BadRequest("Kullanıcı bulunamadı.");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            for (int i = 0; i < hash.Length; i++) if (hash[i] != user.PasswordHash[i]) return BadRequest("Hatalı şifre!");
            return Ok(new { token = _tokenService.CreateToken(user), username = user.Username });
        }
    }
}