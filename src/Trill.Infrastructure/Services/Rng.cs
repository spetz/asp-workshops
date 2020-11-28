using System;
using System.Linq;
using System.Security.Cryptography;
using Trill.Core.Services;

namespace Trill.Infrastructure.Services
{
    internal class Rng : IRng
    {
        private static readonly string[] SpecialChars = {"/", "\\", "=", "+", "?", ":", "&"};

        public string Generate(int length = 30)
        {
            using var rng = new RNGCryptoServiceProvider();
            var bytes = new byte[length];
            rng.GetBytes(bytes);
            var result = Convert.ToBase64String(bytes);

            return SpecialChars.Aggregate(result, (current, chars) => current.Replace(chars, string.Empty));
        }
    }
}