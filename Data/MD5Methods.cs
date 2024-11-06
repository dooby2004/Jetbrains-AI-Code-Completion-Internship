using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.VisualBasic.Devices;

namespace Modux_MD5
{
    public class MD5Methods
    {
        public static uint LShift(uint a, int s)
        {
            return a << s | a >> (32 - s);
        }

        public static byte[] EncryptMD4(byte[] input)
        {
            ulong len = Convert.ToUInt64(input.Length * 8);
            byte[] size = BitConverter.GetBytes(len);
            byte initial = 0x80;
            byte padding = 0x00;
            int extra = Convert.ToInt32(64 - ((9 + input.Length) % 64));
            byte[] padded = new byte[input.Length + extra + 9];
            Array.Copy(input, 0, padded, 0, input.Length);
            Array.Copy(new byte[] { initial }, 0, padded, input.Length, 1);
            Array.Copy(Enumerable.Repeat(padding, extra).ToArray(), 0, padded, input.Length + 1, extra);
            Array.Copy(size, 0, padded, input.Length + extra + 1, 8);
            uint[] words = Enumerable.Range(0, padded.Length / 4).Select(y => BitConverter.ToUInt32(padded, y * 4)).ToArray();

            uint a = 0x67452301;
            uint b = 0xefcdab89;
            uint c = 0x98badcfe;
            uint d = 0x10325476;

            for (int i = 0; i < words.Length / 16; i++)
            {
                uint[] x = new uint[16];
                Array.Copy(words, i * 16, x, 0, 16);
                uint aa = a;
                uint bb = b;
                uint cc = c;
                uint dd = d;

                r1(ref a, b, c, d, x[0], 3);
                r1(ref d, a, b, c, x[1], 7);
                r1(ref c, d, a, b, x[2], 11);
                r1(ref b, c, d, a, x[3], 19);
                r1(ref a, b, c, d, x[4], 3);
                r1(ref d, a, b, c, x[5], 7);
                r1(ref c, d, a, b, x[6], 11);
                r1(ref b, c, d, a, x[7], 19);
                r1(ref a, b, c, d, x[8], 3);
                r1(ref d, a, b, c, x[9], 7);
                r1(ref c, d, a, b, x[10], 11);
                r1(ref b, c, d, a, x[11], 19);
                r1(ref a, b, c, d, x[12], 3);
                r1(ref d, a, b, c, x[13], 7);
                r1(ref c, d, a, b, x[14], 11);
                r1(ref b, c, d, a, x[15], 19);

                r2(ref a, b, c, d, x[0], 3);
                r2(ref d, a, b, c, x[4], 5);
                r2(ref c, d, a, b, x[8], 9);
                r2(ref b, c, d, a, x[12], 13);
                r2(ref a, b, c, d, x[1], 3);
                r2(ref d, a, b, c, x[5], 5);
                r2(ref c, d, a, b, x[9], 9);
                r2(ref b, c, d, a, x[13], 13);
                r2(ref a, b, c, d, x[2], 3);
                r2(ref d, a, b, c, x[6], 5);
                r2(ref c, d, a, b, x[10], 9);
                r2(ref b, c, d, a, x[14], 13);
                r2(ref a, b, c, d, x[3], 3);
                r2(ref d, a, b, c, x[7], 5);
                r2(ref c, d, a, b, x[11], 9);
                r2(ref b, c, d, a, x[15], 13);

                r3(ref a, b, c, d, x[0], 3);
                r3(ref d, a, b, c, x[8], 9);
                r3(ref c, d, a, b, x[4], 11);
                r3(ref b, c, d, a, x[12], 15);
                r3(ref a, b, c, d, x[2], 3);
                r3(ref d, a, b, c, x[10], 9);
                r3(ref c, d, a, b, x[6], 11);
                r3(ref b, c, d, a, x[14], 15);
                r3(ref a, b, c, d, x[1], 3);
                r3(ref d, a, b, c, x[9], 9);
                r3(ref c, d, a, b, x[5], 11);
                r3(ref b, c, d, a, x[13], 15);
                r3(ref a, b, c, d, x[3], 3);
                r3(ref d, a, b, c, x[11], 9);
                r3(ref c, d, a, b, x[7], 11);
                r3(ref b, c, d, a, x[15], 15);

                a = a + aa;
                b = b + bb;
                c = c + cc;
                d = d + dd;
            }

            var results = new List<byte>(16);
            results.AddRange(BitConverter.GetBytes(a));
            results.AddRange(BitConverter.GetBytes(b));
            results.AddRange(BitConverter.GetBytes(c));
            results.AddRange(BitConverter.GetBytes(d));

            return results.ToArray();
        }

        public static uint f(uint x, uint y, uint z)
        {
            return (x & y) | (~x & z);
        }

        public static uint g4(uint x, uint y, uint z)
        {
            return (x & y) | (x & z) | (y & z);
        }

        public static uint h(uint x, uint y, uint z)
        {
            return x ^ y ^ z;
        }

        public static void r1(ref uint a, uint b, uint c, uint d, uint x, int s)
        {
            a = LShift((a + f(b, c, d) + x), s);
        }

        public static void r2(ref uint a, uint b, uint c, uint d, uint x, int s)
        {
            a = LShift((a + g4(b, c, d) + x + 0x5a827999), s);
        }

        public static void r3(ref uint a, uint b, uint c, uint d, uint x, int s)
        {
            a = LShift((a + h(b, c, d) + x + 0x6ed9eba1), s);
        }

        public static byte[] EncryptMD5(byte[] input)
        {
            //return System.Security.Cryptography.MD5.Create().ComputeHash(input);
            ulong len = Convert.ToUInt64(input.Length * 8);
            byte[] size = BitConverter.GetBytes(len);
            byte initial = 0x80;
            byte padding = 0x00;
            int extra = Convert.ToInt32(64 - ((9 + input.Length) % 64));
            byte[] padded = new byte[input.Length + extra + 9];
            Array.Copy(input, 0, padded, 0, input.Length);
            Array.Copy(new byte[] { initial }, 0, padded, input.Length, 1);
            Array.Copy(Enumerable.Repeat(padding, extra).ToArray(), 0, padded, input.Length + 1, extra);
            Array.Copy(size, 0, padded, input.Length + extra + 1, 8);
            uint[] words = Enumerable.Range(0, padded.Length / 4).Select(y => BitConverter.ToUInt32(padded, y * 4)).ToArray();
            
            // Source: https://en.wikipedia.org/wiki/MD5
            uint a = 0x67452301;
            uint b = 0xefcdab89;
            uint c = 0x98badcfe;
            uint d = 0x10325476;
            for (int i = 0; i < words.Length / 16; i++)
            {
                uint[] x = new uint[16];
                Array.Copy(words, i * 16, x, 0, 16);
                uint aa = a;
                uint bb = b;
                uint cc = c;
                uint dd = d;

                uint t;

                for (int j = 0; j < 16; j++)
                {
                    t = b + LShift(a + f(b, c, d) + x[j] + sines[j], s[j]);
                    a = d;
                    d = c;
                    c = b;
                    b = t;
                }
                for (int j = 16; j < 32; j++)
                {
                    t = b + LShift(a + g5(b, c, d) + x[r[j]] + sines[j], s[j]);
                    a = d;
                    d = c;
                    c = b;
                    b = t;
                }
                for (int j = 32; j < 48; j++)
                {
                    t = b + LShift(a + h(b, c, d) + x[r[j]] + sines[j], s[j]);
                    a = d;
                    d = c;
                    c = b;
                    b = t;
                }
                for (int j = 48; j < 64; j++)
                {
                    t = b + LShift(a + i5(b, c, d) + x[r[j]] + sines[j], s[j]);
                    a = d;
                    d = c;
                    c = b;
                    b = t;
                }

                a = a + aa;
                b = b + bb;
                c = c + cc;
                d = d + dd;
            }
            byte[] results = new byte[16];
            Array.Copy(BitConverter.GetBytes(a), 0, results, 0, 4);
            Array.Copy(BitConverter.GetBytes(b), 0, results, 4, 4);
            Array.Copy(BitConverter.GetBytes(c), 0, results, 8, 4);
            Array.Copy(BitConverter.GetBytes(d), 0, results, 12, 4);
            return results;
        }

        public static uint[] sines = new uint[64]
            {
                0xd76aa478, 0xe8c7b756, 0x242070db, 0xc1bdceee, 0xf57c0faf, 0x4787c62a, 0xa8304613, 0xfd469501,
                0x698098d8, 0x8b44f7af, 0xffff5bb1, 0x895cd7be, 0x6b901122, 0xfd987193, 0xa679438e, 0x49b40821,
                0xf61e2562, 0xc040b340, 0x265e5a51, 0xe9b6c7aa, 0xd62f105d, 0x02441453, 0xd8a1e681, 0xe7d3fbc8,
                0x21e1cde6, 0xc33707d6, 0xf4d50d87, 0x455a14ed, 0xa9e3e905, 0xfcefa3f8, 0x676f02d9, 0x8d2a4c8a,
                0xfffa3942, 0x8771f681, 0x6d9d6122, 0xfde5380c, 0xa4beea44, 0x4bdecfa9, 0xf6bb4b60, 0xbebfbc70,
                0x289b7ec6, 0xeaa127fa, 0xd4ef3085, 0x04881d05, 0xd9d4d039, 0xe6db99e5, 0x1fa27cf8, 0xc4ac5665,
                0xf4292244, 0x432aff97, 0xab9423a7, 0xfc93a039, 0x655b59c3, 0x8f0ccc92, 0xffeff47d, 0x85845dd1,
                0x6fa87e4f, 0xfe2ce6e0, 0xa3014314, 0x4e0811a1, 0xf7537e82, 0xbd3af235, 0x2ad7d2bb, 0xeb86d391
            };

        public static int[] s = new int[64]
            {
                7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22,
                5, 9,  14, 20, 5, 9,  14, 20, 5, 9,  14, 20, 5, 9,  14, 20,
                4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23,
                6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21
            };

        public static int[] r = new int[64]
        {
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15,
            1, 6, 11, 0, 5, 10, 15, 4, 9, 14, 3, 8, 13, 2, 7, 12,
            5, 8, 11, 14, 1, 4, 7, 10, 13, 0, 3, 6, 9, 12, 15, 2,
            0, 7, 14, 5, 12, 3, 10, 1, 8, 15, 6, 13, 4, 11, 2, 9
        };

        public static uint g5(uint x, uint y, uint z)
        {
            return (x & z) | (y & ~z);
        }

        public static uint i5(uint x, uint y, uint z)
        {
            return y ^ (x | ~z);
        }

        public static string Encrypt(string input, Func<byte[], byte[]> hashMethod)
        {
            // Source: https://stackoverflow.com/questions/11454004/calculate-a-md5-hash-from-a-string
            
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = hashMethod(inputBytes);

                return Convert.ToHexString(hashBytes);
            }
        }
        public static (int, string) DecryptFromList(string input, string[] keywords, Func<byte[], byte[]> hashMethod)
        {
            // Remove Whitespace Source: https://code-maze.com/replace-whitespaces-string-csharp/
            string hash = Regex.Replace(input.ToUpper(), @"\s", String.Empty);
            if (hash.Length == 32)
            {
                foreach (string keyword in keywords)
                {
                    if (Encrypt(keyword, hashMethod) == hash)
                    {
                        return (0, keyword);
                    }
                }
                return (1, String.Empty);
            }
            else
            {
                return (2, String.Empty);
            }
        }

        public static (int, string) DecryptFromFile(string input, Stream fileStream, Func<byte[], byte[]> hashMethod)
        {
            // Remove Whitespace Source: https://code-maze.com/replace-whitespaces-string-csharp/
            string hash = Regex.Replace(input.ToUpper(), @"\s", String.Empty);
            if (hash.Length == 32)
            {
                // Source: https://stackoverflow.com/questions/8037070/whats-the-fastest-way-to-read-a-text-file-line-by-line
                using (StreamReader streamReader = new StreamReader(fileStream, true))
                {
                    String keyword;
                    while ((keyword = streamReader.ReadLine()) != null)
                    {
                        if (Encrypt(keyword, hashMethod) == hash)
                        {
                            return (0, keyword);
                        }
                    }
                    return (1, String.Empty);
                }
            }
            else
            {
                return (2, String.Empty);
            }
        }

        public static (int, string) ForceDecrypt(string input, string[] keywords, Func<byte[], byte[]> hashMethod)
        {
            // Remove Whitespace Source: https://code-maze.com/replace-whitespaces-string-csharp/
            string hash = Regex.Replace(input.ToUpper(), @"\s", String.Empty);
            keywords = keywords.Select(x => x.ToLower()).ToArray();
            if (hash.Length == 32)
            {
                for (int i = 0; i < keywords.Length; i++)
                {
                    for (int j = 0; j < keywords.Length; i++)
                    {
                        string word1 = keywords[i];
                        string word2 = keywords[j];
                        string[] words1 = altSpellings(word1);
                        string[] words2 = altSpellings(word2);
                        /*
                        foreach (string first in words1)
                        {
                            foreach(string second in words2)
                            {
                                foreach(string symbol in symbols)
                                {
                                    foreach(string number in numbers)
                                    {
                                        string[] tests;
                                        if (symbol == "")
                                        {
                                            if (number == "")
                                            {
                                                tests = [first + second];
                                            }
                                            else
                                            {
                                                tests = [first + second + number,
                                                    number + first + second];
                                            }
                                        }
                                        else
                                        {
                                            if (number == "")
                                            {
                                                tests = [first + second + symbol,
                                                    first + symbol + second,
                                                    symbol + first + second];
                                            }
                                            else
                                            {
                                                tests = [first + second + number + symbol,
                                                    first + second + symbol + number,
                                                    first + symbol + second + number,
                                                    number + first + symbol + second,
                                                    number + symbol + first + second,
                                                    symbol + number + first + second];
                                            }
                                        }
                                        foreach (string test in tests)
                                        {
                                            Debug.WriteLine(test);
                                            if (Encrypt(test, hashMethod) == hash)
                                            {
                                                return (0, test);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        */
                        foreach (string first in words1)
                        {
                            foreach (string symbol in symbols)
                            {
                                foreach (string number in numbers)
                                {
                                    string[] tests;
                                    if (symbol == "")
                                    {
                                        if (number == "")
                                        {
                                            tests = [first];
                                        }
                                        else
                                        {
                                            tests = [first + number,
                                                number + first];
                                        }
                                    }
                                    else
                                    {
                                        if (number == "")
                                        {
                                            tests = [first + symbol,
                                                symbol + first];
                                        }
                                        else
                                        {
                                            tests = [first + number + symbol,
                                                first + symbol + number,
                                                symbol + first + number,
                                                number + first + symbol,
                                                number + symbol + first,
                                                symbol + number + first];
                                        }
                                    }
                                    foreach (string test in tests)
                                    {
                                        //Debug.WriteLine(test);
                                        if (Encrypt(test, hashMethod) == hash)
                                        {
                                            return (0, test);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return (1, String.Empty);
            }
            else
            {
                return (2, String.Empty);
            }
        }

        public static string[] altSpellings(string input)
        {
            string[] combinations = [""];
            foreach (char character in input)
            {
                if (alternate.ContainsKey(character))
                {
                    combinations = (from x in combinations
                                    from y in alternate[character]
                                    select new string(x.ToCharArray().Append(y).ToArray())).ToArray();
                }
                else
                {
                    combinations = combinations.Select(x => new string(x.ToCharArray().Append(character).ToArray())).ToArray();
                }
            }
            return combinations;
        }

        public static string[] symbols = ["!", "@", "$", ""];

        public static string[] numbers = Enumerable.Range(0, 100).Select(x => x.ToString()).Concat(new string[] { "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "" }).ToArray();

        public static Dictionary<char, char[]> alternate = new Dictionary<char, char[]>
        {
            {'a', ['a','A','@','4']},
            {'b', ['b','B']},
            {'c', ['c','C']},
            {'d', ['d','D']},
            {'e', ['e','E','3']},
            {'f', ['f','F']},
            {'g', ['g','G','9']},
            {'h', ['h','H','4']},
            {'i', ['i','I','!','1','l']},
            {'j', ['j','J']},
            {'k', ['k','K']},
            {'l', ['l','L','1']},
            {'m', ['m','M']},
            {'n', ['n','N']},
            {'o', ['o','O','0']},
            {'p', ['p','P','9']},
            {'q', ['q','Q']},
            {'r', ['r','R']},
            {'s', ['s','S','$','5','z','Z']},
            {'t', ['t','T','7','4']},
            {'u', ['u','U','v','V']},
            {'v', ['v','V','u','U']},
            {'w', ['w','W']},
            {'x', ['x','X']},
            {'y', ['y','Y']},
            {'z', ['z','Z','2','s','S']}
        };
    }
}
