using System;
using System.IO;
using System.Collections.Generic;
//using System.Text;
using static System.Math;
namespace lab1
{
    class Program
    {
        static void ToBase64(string path)
        {
            string input_str = File.ReadAllText(path);
            string char_set = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzАБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯабвгґдеєжзиіїйклмнопрстуфхцшщьюя0123456789+/";
            List<char> res_str = new List<char>();
            int index, bits = 0, padding = 0, val = 0, count = 0, temp;
            for(int i = 0; i < input_str.Length; i += 3)
            {
                val = 0;
                count = 0;
                bits = 0;
                for(int j = i; j < input_str.Length && j <= i + 2; j++)
                {
                    val <<= 8;//binary data of input_str
                    val |= input_str[j];//(c + 0 = c) stores character in val 
                    count++;
                }
                bits = count * 8;
                padding = bits % 3;//calculates how many "=" to append after res_str. 
                
                // extracts all bits from val (6 at a time)  
                // and find the value of each block 
                while (bits != 0)
                {
                    if (bits >= 6)
                    {
                        temp = bits - 6;
                        index = (val >> temp) & 63;
                        bits -= 6;
                    }
                    else
                    {
                        temp = 6 - bits;
                        index = (val << temp) & 63;// append zeros to right if bits are less than 6 
                        bits = 0;
                    }
                    res_str.Add(char_set[index]);
                }
            }
            for(int i = 1; i <= padding; i++){
                res_str.Add('=');
            }
            string newpath = path.Substring(0, path.IndexOf("."));
            newpath += "_base64";
            if (File.Exists(newpath)) File.Delete(newpath);
            File.WriteAllText(newpath, new string(res_str.ToArray()));
            PT1(newpath, 3);
        }
        static float Entropy(List<float> p)
        {
            float H = 0;
            for(int i = 1; i <= p.Count; i++)
            {
                H += p[i - 1] * (float)Log2(p[i - 1]);
            }
            return -H;
        }
        /// <summary>
        /// PT1
        /// </summary>
        /// <param name="path">path to file</param>
        /// <param name="choice">What would you like to do?
        /// 1-count occurences and frequency of every character
        /// 2-count average entropy
        /// 3-count amount of information
        /// 4-all of the above</param>
        static void PT1(string path,int choice)
        {
            if (choice < 1 || choice > 4)
            {
                Console.WriteLine("NO");
                return;
            }
            Dictionary<char, int> occurrences = new Dictionary<char, int>();
            string text = File.ReadAllText(path);
            foreach (char c in text)
            {
                //if (c == '\r') continue;//not counting \n because windows treats enter as two separate symbols and it doubles its number
                if (!occurrences.ContainsKey(c))
                {
                    occurrences.Add(c, 1);
                }
                else
                {
                    occurrences[c]++;
                }
            }
            int n = 0;//how many elements
            foreach (KeyValuePair<char, int> pair in occurrences)
            {
                n += pair.Value;
            }
            List<float> p = new List<float>();
            foreach (KeyValuePair<char, int> pair in occurrences)
            {
                p.Add((float)pair.Value / n);
                if(choice==1||choice==4)
                    Console.WriteLine($"\'{pair.Key}\':\r\n{pair.Value} occurrences and frequency of {(float)pair.Value / n}\r\n");
            }
            if(choice==2||choice==4)
                Console.WriteLine($"Average entropy is {Entropy(p)} bits");
            if(choice==3||choice==4)
                Console.WriteLine($"Amount of information is {Entropy(p) * n * 0.125} bytes");
        }
        static void Main(string[] args)
        {
            //Console.OutputEncoding = Encoding.UTF8;
            #region Definitions
            string file1 = "Texts\\Egg\\Egg.txt";
            string file1_7z = "Texts\\Egg\\Egg.7z";
            string file1_bz2 = "Texts\\Egg\\Egg.bz2";
            string file1_gz = "Texts\\Egg\\Egg.gz";
            string file1_xz = "Texts\\Egg\\Egg.xz";
            string file1_zip = "Texts\\Egg\\Egg.zip";

            string file2 = "Texts\\Hymn\\Hymn.txt";
            string file2_7z = "Texts\\Hymn\\Hymn.7z";
            string file2_bz2 = "Texts\\Hymn\\Hymn.bz2";
            string file2_gz = "Texts\\Hymn\\Hymn.gz";
            string file2_xz = "Texts\\Hymn\\Hymn.xz";
            string file2_zip = "Texts\\Hymn\\Hymn.zip";

            string file3 = "Texts\\Sizif\\Sizif.txt";
            string file3_7z = "Texts\\Sizif\\Sizif.7z";
            string file3_bz2 = "Texts\\Sizif\\Sizif.bz2";
            string file3_gz = "Texts\\Sizif\\Sizif.gz";
            string file3_xz = "Texts\\Sizif\\Sizif.xz";
            string file3_zip = "Texts\\Sizif\\Sizif.zip";
            #endregion
            Console.WriteLine("--------------------------File1--------------------------");
            PT1(file1, 4);
            Console.WriteLine($"File size is {new FileInfo(file1).Length} bytes");

            Console.WriteLine($"7z:{new FileInfo(file1_7z).Length} bytes");
            Console.WriteLine($"bz2:{new FileInfo(file1_bz2).Length} bytes");
            Console.WriteLine($"gz:{new FileInfo(file1_gz).Length} bytes");
            Console.WriteLine($"xz:{new FileInfo(file1_xz).Length} bytes");
            Console.WriteLine($"zip:{new FileInfo(file1_zip).Length} bytes");

            Console.WriteLine("--------------------------File2--------------------------");
            PT1(file2, 4);
            Console.WriteLine($"File size is {new FileInfo(file2).Length} bytes");

            Console.WriteLine($"7z:{new FileInfo(file2_7z).Length} bytes");
            Console.WriteLine($"bz2:{new FileInfo(file2_bz2).Length} bytes");
            Console.WriteLine($"gz:{new FileInfo(file2_gz).Length} bytes");
            Console.WriteLine($"xz:{new FileInfo(file2_xz).Length} bytes");
            Console.WriteLine($"zip:{new FileInfo(file2_zip).Length} bytes");

            Console.WriteLine("--------------------------File3--------------------------");
            PT1(file3, 4);
            Console.WriteLine($"File size is {new FileInfo(file3).Length} bytes");

            Console.WriteLine($"7z:{new FileInfo(file3_7z).Length} bytes");
            Console.WriteLine($"bz2:{new FileInfo(file3_bz2).Length} bytes");
            Console.WriteLine($"gz:{new FileInfo(file3_gz).Length} bytes");
            Console.WriteLine($"xz:{new FileInfo(file3_xz).Length} bytes");
            Console.WriteLine($"zip:{new FileInfo(file3_zip).Length} bytes");

            Console.WriteLine("-------------------------PT2-------------------------\r\n");
            Console.WriteLine("--------------------------File1--------------------------");
            ToBase64(file1);
            Console.WriteLine("bz2 archive to base64");
            ToBase64(file1_bz2);
            Console.Write("Source file: ");
            PT1(file1, 3);

            Console.WriteLine("\r\n--------------------------File2--------------------------");
            ToBase64(file2);
            Console.WriteLine("bz2 archive to base64");
            ToBase64(file2_bz2);
            Console.Write("Source file: ");
            PT1(file2, 3);

            Console.WriteLine("\r\n--------------------------File3--------------------------");
            ToBase64(file3);
            Console.WriteLine("bz2 archive to base64");
            ToBase64(file3_bz2);
            Console.Write("Source file: ");
            PT1(file3, 3);
        }
    }
}
