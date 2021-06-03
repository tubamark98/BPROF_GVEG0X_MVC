using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels
{
    public class TokenViewModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
