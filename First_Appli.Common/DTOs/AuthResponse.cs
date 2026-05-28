using System;
using System.Collections.Generic;
using System.Text;


namespace First_Appli.Common.DTOs
{
    public class AuthResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public string Token { get; set; }
    }
}