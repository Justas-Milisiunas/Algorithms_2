using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_2
{
    class DAGLongestPath
    {
        public static readonly int[] KIEKIAI = new int[] { 1, 10, 50, 200, 1000 };
        public void TestRecursion(int seed)
        {
            //int[][] graph = new int[11][];
            //graph[0] = new int[] { 1, 2 };
            //graph[1] = new int[] { 1, 4 };
            //graph[2] = new int[] { 1, 6 };
            //graph[3] = new int[] { 1, 8 };
            //graph[4] = new int[] { 2, 3 };
            //graph[5] = new int[] { 4, 3 };
            //graph[6] = new int[] { 4, 5 };
            //graph[7] = new int[] { 6, 5 };
            //graph[8] = new int[] { 6, 9 };
            //graph[9] = new int[] { 7, 6 };
            //graph[10] = new int[] { 8, 7 };

            int[][] graph = generateDirectedAcyclicGraph(1000, seed);

            Console.WriteLine("Testing finding directed acyclic graph longest path algorithm using threaded recursions");
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

            int[] child = childs[0];
            graph = graph.Where(x => !x.SequenceEqual(child)).ToArray();

            return Math.Max(longestPathResursive(graph, vertex), 1 + longestPathResursive(graph, child[1]));
        }

        private int longestPath(int[][] graph, int vertex)
        {
            bool[] visited = new bool[graph.Length];
            foreach(int[] vt in graph)
            {
                if(!visited[vt[0]])
                {

                }
            }

            return 0;
        }

        private int visitAllChilds(int[][] graph, bool[] visited, int[] lengths, int vertex)
        {
            int[][] childs = graph.Where(x => x[0] == vertex).ToArray();
            foreach(int[] child in childs)
            {
                if(!visited[child[0]])
                {
                    lengths[child[0]]++;
                    //visitAllChilds(graph, visited, child[0]);
                    visited[child[0]] = true;

                }
            }

            return 0;
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

            for (int i = 0; i < length; i++)
            {
                graph.Add(new int[] { random.Next(1, 1000), random.Next(1, 1000) });
            }

            return graph.ToArray();
        }
    }
}
