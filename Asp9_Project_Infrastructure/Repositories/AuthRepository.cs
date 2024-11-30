using Asp9_Project_Core.Interfaces;
using Asp9_Project_Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Project_Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<Users> userManager;
        private readonly SignInManager<Users> signInManager;
        private readonly IConfiguration configuration;

        public AuthRepository(UserManager<Users> userManager, SignInManager<Users> signInManager , IConfiguration configuration )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }
        public async Task<string> RegisterAsync(Users user, string password)
        {
            var result = await userManager.CreateAsync(user,password);
            if (result.Succeeded)
            {
                return "User Registered Successfully";
            }
           var errorMessages = result.Errors.Select(error => error.Description).ToList();
            return string.Join(", " ,errorMessages);
        }

        public Task<string> ChangePasswordAsync(string email, string oldPasswrod, string newPassword)
        {
            throw new NotImplementedException();
        }



        public async Task<string> LoginAsync(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return "Invalid Username or Password";
            }

            var result = await signInManager.PasswordSignInAsync(user, password, false, false);
            if (!result.Succeeded)
            {
                return null;
            }
            return GenerateToken(user);
        }


        private string GenerateToken(Users users)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub , users.UserName),
                new Claim(ClaimTypes.NameIdentifier , users.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token =new JwtSecurityToken(
                configuration["JWT:Issuer"],
                configuration["JWT:Audience"],
                claims,
                signingCredentials: cred,
                expires: DateTime.Now.AddMinutes(30)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
                

    }
}
