using BCrypt.Net;
using First_Appli.Common.DTOs;
using First_Appli.Common.Model;
using First_Appli.Store.Abstractions;
using First_Appli.Service.Abstractions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace First_Appli.Service.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IAuthStore _authStore;
        private readonly IConfiguration _configuration;

        public AuthService(
        IAuthStore authStore,
        IConfiguration configuration)
        {
            _authStore = authStore;
            _configuration = configuration;
        }

        public async Task<AuthResponse> Register(RegisterRequest request)
        {
            // Hash password before storing in database
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            Employee employee = new Employee
            {
                Name = request.Name,
                Department = request.Department,
                Email = request.Email,
                PasswordHash = hashedPassword,
                Role = request.Role
            };

            await _authStore.Register(employee);

            return new AuthResponse
            {
                IsSuccess = true,
                Message = "Registration Successful"
            };
        }

        public async Task<AuthResponse> Login(LoginRequest request)
        {
            var employee = await _authStore.GetEmployeeByEmail(request.Email);

            if (employee == null)
            {
                return new AuthResponse
                {
                    IsSuccess = false,
                    Message = "Invalid Email"
                };
            }

            bool isPasswordValid =
                BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    employee.PasswordHash
                );

            if (!isPasswordValid)
            {
                return new AuthResponse
                {
                    IsSuccess = false,
                    Message = "Invalid Password"
                };
            }

            string token = GenerateJwtToken(employee);

            return new AuthResponse
            {
                IsSuccess = true,
                Message = "Login Successful",
                Token = token
            };
        }





        private string GenerateJwtToken(Employee employee)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]
                )
            );

            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
                );

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, employee.Name),

            new Claim(ClaimTypes.Email, employee.Email),

            new Claim(ClaimTypes.Role, employee.Role)
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(
                        _configuration["Jwt:DurationInMinutes"]
                    )
                ),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}