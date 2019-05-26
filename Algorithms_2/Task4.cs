using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Algorithms_2
{
    class Task4
    {
        public static readonly int[] KIEKIAI = new int[] { 1, 10, 50, 200, 1000 };
        private static int mainThread;
        public void Test(int seed)
        {
            //int[][] graph = new int[11][];
            //graph[0] = new int[] { 1, 2 };
            //graph[1] = new int[] { 1, 4 };
            //graph[2] = new int[] { 1, 6 };
            //graph[3] = new int[] { 1, 8 };
            //graph[4] = new int[] { 2, 3 };
            //graph[5] = new int[] { 4, 3 };
            //graph[6] = new int[] { 4, 5 };
            //graph[7] = new int[] { 5, 6 };
            //graph[8] = new int[] { 6, 9 };
            //graph[9] = new int[] { 7, 6 };
            //graph[10] = new int[] { 8, 7 };

            int[][] graph = generateDirectedAcyclicGraph(1000, seed);

            mainThread = System.Threading.Thread.CurrentThread.ManagedThreadId;

            Console.WriteLine("Testing finding directed acyclic graph longest path algorithm using resursion");
            foreach (int count in KIEKIAI)
            {
                Stopwatch t1 = new Stopwatch();
                t1.Start();

                for (int i = 0; i < count; i++)
                {
                    longestPathResursive(graph, 1);
                }

                t1.Stop();
                Console.WriteLine(string.Format("{0} - {1}", count, t1.Elapsed.ToString()));
            }
        }

        private int longestPathResursive(int[][] graph, int vertex)
        {
            int[][] childs = vertexChilds(graph, vertex);
            if (childs.Length == 0)
                return 0;

            if(System.Threading.Thread.CurrentThread.ManagedThreadId == mainThread)
            {
                Thread[] threads = new Thread[childs.Length];
                int[] results = new int[childs.Length];

                for (int i = 0; i < childs.Length; i++)
                {
                    int index = i;
                    threads[i] = new Thread(delegate ()
                    {
                        results[index] = longestPathResursive(graph, childs[index][1]) + 1;
                    });

                    threads[i].Start();
                }

                foreach(Thread thread in threads)
                {
                    thread.Join();
                }

                return results.Max();
            }

            int[] child = childs[0];
            graph = graph.Where(x => !x.SequenceEqual(child)).ToArray();

            return Math.Max(longestPathResursive(graph, vertex), 1 + longestPathResursive(graph, child[1]));
        }

        private int[][] vertexChilds(int[][] graph, int vertex)
        {
            List<int[]> childs = new List<int[]>();
            for (int i = 0; i < graph.Length; i++)
            {
                if (graph[i][0] == vertex)
                    childs.Add(new int[] { graph[i][0], graph[i][1] });
            }

            return childs.ToArray();
        }

        private static int[][] generateDirectedAcyclicGraph(int length, int seed)
        {
            List<int[]> graph = new List<int[]>();
            Random random = new Random(seed);

            for(int i = 0; i < length; i++)
            {
                graph.Add(new int[] { random.Next(1, 1000), random.Next(1, 1000) });
            }

            return graph.ToArray();
        }
    }
}
