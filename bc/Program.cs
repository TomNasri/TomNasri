using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace bc
{
    internal class Program
    {
        public class Block
        {
            //Niombre au hasard
            public uint nonce { get; set; }
            public string difficulty { get; set; }
            public DateTime date { get; set; }

            //Hash de tout le block de transaction
            public string merkel { get; set; }

            //Hash du block précédent
            public string previousHash { get; set; }
            public string version { get; set; }

            //Résultat du hash
            public string hash { get; set; }

            public static void Hash(Block block)
            {
                var sb = new StringBuilder();
                sb.Append(block.nonce.ToString("x"));
                sb.Append(block.difficulty);
                long timestamp = (long)block.date.Subtract(new DateTime(1970, 1, 1, 1, 0, 0)).TotalSeconds;
                //BitConverter.ToUInt64
                sb.Append(timestamp.ToString("x"));
                sb.Append(block.merkel);
                sb.Append(block.previousHash);
                sb.Append(block.version);

                //inversion : 123456 => 56 34 12
                //Convert to little-endian
                string rawHex = SwapOrder(sb.ToString());

                byte[] array = StringToByteArray(rawHex);

                array = SHA256.HashData(array);
                array = SHA256.HashData(array);

                //Convert to little-endian
                block.hash = SwapOrder(ByteArrayToString(array));
            }

            public Block Clone()
            {
                var block = new Block();

                block.date = date;
                block.difficulty = difficulty;
                block.merkel = merkel;
                block.nonce = nonce;
                block.previousHash = previousHash;
                block.version = version;

                return block;
            }
        }

        static void Main(string[] args)
        {
            Block original = new Block();
            original.nonce = 3246554933;
            original.difficulty = "170b8c8b";
            original.date = DateTime.UtcNow;
            original.date = new DateTime(2022, 01, 16, 22, 06, 25);
            original.merkel = "1c589466fa4441dd2aeca8ea8f5a4049a10f18e78b7df650700a6fe254b84578";
            original.previousHash = "00000000000000000008a57da0d3b47c329d6b815a765355df8c167ef7089976";
            original.version = "20002000";

            original.nonce = 3603136119;
            original.difficulty = "170b8c8b";
            original.date = DateTime.UtcNow;
            original.date = new DateTime(2022, 01, 16, 22, 17, 55);
            original.merkel = "878c2bd6518cc01b0d528193f002b5239a7097801035791d8ca9c7339556d837";
            original.previousHash = "0000000000000000000199d53a4946c7fa86f9e8fb1a7dbe490e9eddc35e9c58";
            original.version = "20004000";


            int count = 0;
            bool found = false;
            var sw = Stopwatch.StartNew();

            uint currentNonce = original.nonce - 10000000;
            object l = new object();
            int nbTask = 3;
            Task[] tasks = new Task[nbTask];

            for (int i = 0; i < nbTask; ++i)
            {
                tasks[i] = Task.Factory.StartNew((object data) =>
                {
                    Block block = data as Block;

                    while (!found)
                    {
                        lock (l)
                        {
                            count++;
                            currentNonce++;
                            block.nonce = currentNonce;
                        }

                        Block.Hash(block);

                        if (block.hash.StartsWith("0000000000"))
                        {
                            found = true;
                            Console.WriteLine($"Hash : {block.hash}");
                        }

                    }
                }, original.Clone());
            }

            Task.WaitAll(tasks);

            sw.Stop();


            Console.WriteLine($"{(count / sw.Elapsed.TotalSeconds):############0} op/s");
            //sw => count
            //?? => 1000000000
            Console.WriteLine($"{sw.Elapsed.TotalSeconds * 1000000000 / count} s. pour 1 milliard d'op");

            Console.ReadLine();
            Environment.Exit(0);
        }




        public static string SwapOrder(string input)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = input.Length - 2; i >= 0; i -= 2)
            {
                sb.Append(input[i]);
                sb.Append(input[i + 1]);
            }

            return sb.ToString();
        }

        public static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
    }
}