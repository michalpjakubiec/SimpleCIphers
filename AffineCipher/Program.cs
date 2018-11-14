using System;

namespace AffineCipher
{
    class Program
    {
        static int Mod(int val, int mod)
        {
            return (val % mod + mod) % mod;
        }

        static int GCD(int a, int b)
        {
            while (b != 0)
            {
                var tmp = a % b;
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
                if (Char.IsLetter(ch))
                {
                    int x = ch - 65;
                    output += (char)(((a * x + b) % 26) + 65);
                }
                else output += ch;
            }

            return output;
        }

        public static string Decrypt(string input, int a, int b)
        {
            input = input.ToUpper();
            string output = string.Empty;

            int aInv = ModularMultiplicativeInverse(a);

            foreach (char ch in input)
            {
                if (Char.IsLetter(ch))
                {
                    int y = ch - 65;
                    if (y - b < 0) y = y + 26;
                    output += (char)(((aInv * (y - b)) % 26) + 65);
                }
                else output += ch;
            }

            return output;
        }

        public static int ModularMultiplicativeInverse(int a)
        {
            for (int x = 1; x <= 26; x++)
            {
                var temp = (a * x) % 26;
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

            Console.WriteLine("Enter key a and b (value a must be coprime to 26 in order to decrypt):");

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

            if (GCD(a, 26) == 1)
            {
                Console.Write("Decrypted Text:\t\t");
                Console.WriteLine(Decrypt(encryptedText, a, b));
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Key is not coprime to 26, decryption impossible.");
                Console.WriteLine();
            }

            Console.WriteLine();

            // Decryption from text

            Console.WriteLine("Type text to decrypt:");
            encryptedText = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Enter key a and b (value a must be coprime to 26):");

            Console.Write("a:\t");
            a = int.Parse(Console.ReadLine());

            Console.Write("b:\t");
            b = int.Parse(Console.ReadLine());
            Console.WriteLine();

            if (GCD(a, 26) == 1)
            {
                Console.Write("Decrypted Text:\t\t");
                Console.WriteLine(Decrypt(encryptedText, a, b));
            }
            else
            {
                Console.WriteLine("Key is not coprime to 26, decryption impossible.");
            }

            Console.ReadKey();
        }
    }
}