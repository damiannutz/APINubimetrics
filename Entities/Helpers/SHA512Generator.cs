using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Entities.Helpers
{
    public static class SHA512Generator
    {
        public static string HashText(string text)
        {
            return Convert.ToBase64String(new SHA512Managed().ComputeHash(Encoding.UTF8.GetBytes(text)));
        }
    }
}
