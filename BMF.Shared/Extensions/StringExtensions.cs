using System;
using System.Linq;

namespace BMF.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string GetUppercaseLettersAsString(this string target)
        {
            return new string(target.Where(Char.IsLetter).Where(Char.IsUpper).ToArray());
        }

        public static string SanitizeFileName(this string fileName, char replacementChar = '_')
        {
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                fileName = fileName.Replace(c, replacementChar);
            return fileName;
        }
    }
}
