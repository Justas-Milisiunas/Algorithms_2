using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Algorithms_2
{
    class HashTableFind
    {
        public static readonly int[] KIEKIAI = new int[] { 100, 250, 1000, 5000, 20_000, 100_000, 500_000, 1_500_000 };
        public void Test()
        {
            Tuple<HashTable, string[]> generatedData = generateHashTable(KIEKIAI[KIEKIAI.Length - 1]);

            testHashTableFind(generatedData.Item2, generatedData.Item1);
            testHashTableFindThreaded(generatedData.Item2, generatedData.Item1);

            Console.ReadKey();
        }

        public void testHashTableFind(string[] keys, HashTable lentele)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("HASHTABLE FIND");
            builder.AppendLine("=============================================================================");
            builder.AppendLine("| Number of elements |        |    Runtime time   |        |   Operations   |");
            builder.AppendLine("===================================HASHTABLE FIND============================");

            foreach (int count in KIEKIAI)
            {
                Stopwatch t1 = new Stopwatch();
                int sum = 0;
                t1.Start();
                for (int i = 0; i < count; i++)
                {
                    if (lentele.Contains(keys[i]))
                        sum++;
                }
                t1.Stop();

                builder.AppendLine(string.Format("|{0,-20}|        |{1} ms|        |{2,-16}|", count, t1.Elapsed.ToString(), lentele.operationsCount));
            }

            builder.AppendLine("=============================================================================");
            Console.Write(builder.ToString());

            lentele.operationsCount = 0;
        }

        public void testHashTableFindThreaded(string[] keys, HashTable lentele)
        {
            int threadCount = 4;
            Thread[] threads = new Thread[threadCount];

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("HASHTABLE FIND THREADED");
            builder.AppendLine("=============================================================================");
            builder.AppendLine("| Number of elements |        |    Runtime time   |        |   Operations   |");
            builder.AppendLine("==============================HASHTABLE FIND THREADED========================");

            foreach (int count in KIEKIAI)
            {
                Stopwatch t1 = new Stopwatch();
                int sum = 0;

                t1.Start();
                for (int i = 0; i < threadCount; i++)
                {
                    int index = i;
                    threads[i] = new Thread(delegate ()
                    {
                        for(int j = (count / threadCount) * index; j < (count / threadCount) * (index + 1); j++)
                        {
                            if (lentele.Contains(keys[j]))
                                sum++;
                        }
                    });

                    threads[i].Start();
                }

                foreach (Thread thread in threads)
                {
                    thread.Join();
                }
                t1.Stop();

                builder.AppendLine(string.Format("|{0,-20}|        |{1} ms|        |{2,-16}|", count, t1.Elapsed.ToString(), lentele.operationsCount));
            }

            builder.AppendLine("=============================================================================");
            Console.Write(builder.ToString());

            lentele.operationsCount = 0;
        }

        public Tuple<HashTable, string[]> generateHashTable(int count)
        {
            Random random = new Random();
            HashTable generatedTable = new HashTable();
            string[] keys = new string[count];
            for(int i = 0; i < count; i++)
            {
                keys[i] = random.Next().ToString();
                generatedTable.Put(keys[i], random.Next());
            }

            return new Tuple<HashTable, string[]>(generatedTable, keys);
        }
    }
}
