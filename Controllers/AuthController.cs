﻿using Microsoft.AspNetCore.Mvc;
using CoreDotNetToken.Models;
using CoreDotNetToken.Services;

namespace dotnetcoreapiwithmongo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // will return validation errors
            }

            var result = await _authService.RegisterUserAsync(user.Username, user.Password);
            if (!result) return BadRequest("User already exists");

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            var token = _authService.ValidateUserAndGenerateToken(model.Username, model.Password);
            if (string.IsNullOrEmpty(token)) return Unauthorized();

            return Ok(new { Token = token });
        }
    }
}

