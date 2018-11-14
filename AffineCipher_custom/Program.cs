using System;
using System.Collections.Generic;

namespace AffineCipher_custom
{
    class Program
    {
        static int Mod(int val, int mod)
        {
            return (val % mod + mod) % mod;
        }

        static readonly List<char> AlphabetCustom = new List<char> {'a', 'ą', 'b', 'c', 'ć', 'd', 'e', 'ę', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'ł', 'm', 'n', 'ń', 'o', 'ó', 'p', 'q', 'r', 's', 'ś', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'ź', 'ż' };

        static int GCD(int a, int b)
        {
            while (b != 0)
            {
                var tmp = Mod(a, b);
                a = b;
                b = tmp;
            }

            return a;
        }

        public static string Encrypt(string input, int a, int b)
        {
            input = input.ToUpper();
            string output = string.Empty;

            foreach (char ch in input)
            {
                int index = AlphabetCustom.FindIndex(x => x == Char.ToLower(ch));

                if (Char.IsLetter(ch))
                {
                    int x = index;
                    output += AlphabetCustom[Mod((a * index + b), AlphabetCustom.Count)];
                }
                else output += ch;
            }

            return output.ToUpper();
        }

        public static string Decrypt(string input, int a, int b)
        {
            input = input.ToUpper();
            string output = string.Empty;

            int aInv = ModularMultiplicativeInverse(a);

            foreach (char ch in input)
            {
                int index = AlphabetCustom.FindIndex(x => x == Char.ToLower(ch));

                if (Char.IsLetter(ch))
                {
                    int y = index;
                    output += AlphabetCustom[Mod((aInv * (y - b)), AlphabetCustom.Count)];
                }
                else output += ch;
            }

            return output.ToUpper();
        }

        public static int ModularMultiplicativeInverse(int a)
        {
            for (int x = 1; x <= AlphabetCustom.Count; x++)
            {
                var temp = Mod((a * x), AlphabetCustom.Count);
                if (temp == 1)
                    return x;
            }

            throw new Exception("Modular Multiplicative Inverse not exist.");
        }

        static void Main(string[] args)
        {
            // Text

            Console.WriteLine("Type text to encrypt:");
            string plainText = Console.ReadLine();
            Console.WriteLine();

            // Key

            Console.WriteLine($"Enter key a and b (value a must be coprime to {AlphabetCustom.Count} in order to decrypt):");

            Console.Write("a:\t");
            var a = int.Parse(Console.ReadLine());

            Console.Write("b:\t");
            var b = int.Parse(Console.ReadLine());
            Console.WriteLine();

            // Encryption from text

            Console.Write("Encrypted text:\t\t");
            var encryptedText = Encrypt(plainText, a, b);
            Console.WriteLine(encryptedText);

            // Auto decryption

            if (GCD(a, AlphabetCustom.Count) == 1)
            {
                Console.Write("Decrypted Text:\t\t");
                Console.WriteLine(Decrypt(encryptedText, a, b));
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"Key is not coprime to {AlphabetCustom.Count}, decryption impossible.");
                Console.WriteLine();
            }

            Console.WriteLine();

            // Decryption from text

            Console.WriteLine("Type text to decrypt:");
            encryptedText = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine($"Enter key a and b (value a must be coprime to {AlphabetCustom.Count}):");

            Console.Write("a:\t");
            a = int.Parse(Console.ReadLine());

            Console.Write("b:\t");
            b = int.Parse(Console.ReadLine());
            Console.WriteLine();

            if (GCD(a, AlphabetCustom.Count) == 1)
            {
                Console.Write("Decrypted Text:\t\t");
                Console.WriteLine(Decrypt(encryptedText, a, b));
            }
            else
            {
                Console.WriteLine($"Key is not coprime to {AlphabetCustom.Count}, decryption impossible.");
            }

            Console.ReadKey();
        }
    }
}