using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using First_Appli.Common.DTOs;

namespace First_Appli.Service.Abstractions
{
    public interface IAuthService
    {
        Task<AuthResponse> Register(RegisterRequest request);

        Task<AuthResponse> Login(LoginRequest request);
    }
}