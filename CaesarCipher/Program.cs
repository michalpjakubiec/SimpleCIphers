using System;
using System.Collections.Generic;

namespace CaesarCipher
{
    class Program
    {
        static int Mod(int val, int mod)
        {
            return (val % mod + mod) % mod;
        }

        static readonly List<char> AlphabetCustom = new List<char> {'a', 'ą', 'b', 'c', 'ć', 'd', 'e', 'ę', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'ł', 'm', 'n', 'ń', 'o', 'ó', 'p', 'q', 'r', 's', 'ś', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'ź', 'ż' };

        public static char Caesar(char ch, int shift)
        {
            if (!char.IsLetter(ch))
            {
                return ch;
            }

            int index = AlphabetCustom.FindIndex(x => x == Char.ToLower(ch));

            return (char.IsLower(ch) ?
                AlphabetCustom[Mod(index + shift, AlphabetCustom.Count)] :
                Char.ToUpper(AlphabetCustom[Mod(index + shift, AlphabetCustom.Count)]));
        }

        public static string Encrypt(string input, int shift)
        {
            string output = string.Empty;

            foreach (char ch in input)
            {
                output += Caesar(ch, shift);
            }

            return output;
        }

        public static string Decrypt(string input, int shift)
        {
            return Encrypt(input, AlphabetCustom.Count - shift);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Type a text to encrypt (spaces and special chars may be included):");
            string input = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Enter shift:");
            int shift = int.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.Write("Encrypted text:\t\t");
            string encryptedText = Encrypt(input, shift);
            Console.WriteLine(encryptedText);
            Console.Write("Decrypted text:\t\t");
            Console.WriteLine(Decrypt(encryptedText, shift));
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("Type a text to decrypt:");
            encryptedText = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Enter shift:");
            shift = int.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.Write("Decrypted text:\t\t");
            Console.WriteLine(Decrypt(encryptedText, shift));
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}