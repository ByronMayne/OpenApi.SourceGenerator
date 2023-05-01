using System;

namespace OpenApi.SourceGenerator
{
    internal static class NamingConventionFormat
    {
        public static string ToParameterName(string value)
        {
            if (value == null || value.Length == 0)
            {
                return value;
            }

            char[] characters = PreparseArray(value);
            characters[0] = char.ToLower(characters[0]);
            return new string(characters);
        }

        public static string ToTypeName(string value)
        {
            if (value == null || value.Length == 0)
            {
                return value;
            }

            char[] characters = PreparseArray(value);
            characters[0] = char.ToUpper(characters[0]);
            return new string(characters);
        }

        public static string ToMethodName(string value)
        {
            if (value == null || value.Length == 0)
            {
                return value;
            }

            char[] characters = PreparseArray(value);
            characters[0] = char.ToUpper(characters[0]);
            return new string(characters);
        }

        public static char[] PreparseArray(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Array.Empty<char>();
            }

            int length = value.Length;
            int count = 0;

            char[] letters = value.ToCharArray();

            for (int i = 0; i < length; i++)
            {
                char letter = letters[i];

                if (char.IsWhiteSpace(letter))
                {
                    letter = '_';
                }

                if (char.IsLetter(letter) || char.IsNumber(letter) || letter == '_')
                {
                    letters[count] = letter;
                    count++;
                }
            }
            Array.Resize(ref letters, length);
            return letters;
        }
    }
}
