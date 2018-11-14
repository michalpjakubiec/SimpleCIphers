using System;

namespace VigenereCipher
{
    class Program
    {
        static int Mod(int val, int mod)
        {
            return (val % mod + mod) % mod;
        }

        static string Encrypt(string input, string key)
        {
            input = input.ToUpper();
            key = key.ToUpper();

            string output = string.Empty;
            int j = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsLetter(input[i]))
                    output += (char)((input[i] + key[j]) % 26 + 'A');
                else
                    output += input[i];

                if (j < key.Length - 1)
                    j++;
                else
                    j = 0;
            }
            return output;
        }

        public static string Decrypt(string input, string key)
        {
            input = input.ToUpper();
            key = key.ToUpper();

            string output = string.Empty;
            int j = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsLetter(input[i]))
                    output += (char)((Mod((input[i] - key[j]), 26)) + 65);
                else
                    output += input[i];

                if (j < key.Length - 1)
                    j++;
                else
                    j = 0;
            }
            return output;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Type a text to encrypt:");
            string input = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Enter key:");
            string key = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Encrypted text:\t\t");
            string encryptedText = Encrypt(input, key);
            Console.WriteLine(encryptedText);
            Console.Write("Decrypted text:\t\t");
            Console.WriteLine(Decrypt(encryptedText, key));
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("Type a text to decrypt:");
            input = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Enter key:");
            key = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Decrypted text:\t\t");
            Console.WriteLine(Decrypt(input, key));

            Console.ReadKey();
        }
    }
}