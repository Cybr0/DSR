using System;
using System.Collections.Generic;
using System.Text;

namespace DSRTestTask.Helpers
{
    public static class StringGenerator
    {
        private static string stringChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private static string digitChars = "0123456789";
        private static string symbolChars = "@#$%&*()-+";

        public static string GenerateName(string mask)
        {
            string chars = stringChars + digitChars + symbolChars;
            if (mask == "(2-25)")
            {
                return GenerateString(3, 26, chars);
            }
            else if (mask == ">2")
            {
                return GenerateString(2, 2, chars);
            }
            else if (mask == "<25")
            {
                return GenerateString(27, 50, chars);
            }
            return null;
        }

        public static string GenerateMail(string mask)
        {
            string chars = stringChars + digitChars;
            if (mask == "mail")
            {
                return $"{GenerateString(3, 10, chars)}@gmail.com";
            }
            else if (mask == "other")
            {
                return $"{GenerateString(3, 10, chars)}@ma[]il.com";
            }
            else if (mask == "@@")
            {
                return $"{GenerateString(3, 10, chars)}@@mail.com";
            }
            return null;
        }

        public static string GeneratePhoneNumber(string mask)
        {
            string chars = digitChars;
            if (mask == "(7-12)")
            {
                return GenerateString(8, 13, chars);
            }
            else if (mask == ">7")
            {
                return GenerateString(2, 7, chars);
            }
            else if (mask == "<12")
            {
                return GenerateString(14, 50, chars);
            }
            else if (mask == "other chars")
            {
                chars += stringChars + symbolChars;
                return GenerateString(14, 50, chars);
            }
            return null;
        }

        private static string GenerateString(int minLength, int maxLength, string chars)
        {
            var random = new Random();
            int stringLength = random.Next(minLength, maxLength);
            var stringChars = new char[stringLength - 1];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new string(stringChars);
        }
    }
}
