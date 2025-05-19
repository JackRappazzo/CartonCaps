using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CartonCaps.Core.Services.DeferredLinking
{
    public class ShortCodeGenerator : IShortCodeGenerator
    {

        private static readonly char[] characterSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();

        private RandomNumberGenerator randomNumberGenerator;

        public ShortCodeGenerator()
        {
            randomNumberGenerator = RandomNumberGenerator.Create();
        }

        public string GenerateShortCode(int length)
        {
            var stringBuilder = new StringBuilder(length);
            var data = new byte[length];

            randomNumberGenerator.GetBytes(data);

            foreach (var randomNumber in data)
            {
                //Condenses random number to be between 0 and the length of the character set
                //by calculating its remainder
                var charIndex = randomNumber % characterSet.Length;

                stringBuilder.Append(characterSet[charIndex]);
            }

            return stringBuilder.ToString();

        }
    }
}
