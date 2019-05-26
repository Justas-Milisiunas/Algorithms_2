using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_2
{
    class SplitToBooks
    {
        public void testChaptersSplit()
        {
            int booksCount = 3;
            int[] chapters = new int[] { 100, 20, 30, 150, 80, 50, 60 };
            int[] result = splitChaptersToBooks(chapters.Length, booksCount, chapters);

            StringBuilder resultText = new StringBuilder();
            resultText.AppendFormat("Skyriai bus suskirstyti i {0} knygas. Knygu puslapiai: ", booksCount);
            foreach(int pagesCount in result)
            {
                resultText.AppendFormat("{0} ", pagesCount);
            }

            Console.WriteLine(resultText.ToString());
        }

        public int[] splitChaptersToBooks(int chaptersCount, int books, int[] chapters)
        {
            int[] allSummed = new int[chaptersCount];
            int sum = 0;

            //Sums all chapter's pages
            for(int i = 0; i < chaptersCount; i++)
            {
                sum += chapters[i];
                allSummed[i] = sum;
            }

            //if(books <= 1)
            //{
            //    return new int[] { allSummed[chaptersCount - 1] };
            //}
            
            if(chaptersCount == 0)
            {
                return null;
            }

            int[][] resultTable = new int[books][];
            resultTable[0] = new int[chaptersCount];
            for (int i = 0; i < chaptersCount; i++)
            {
                resultTable[0][i] = allSummed[i];
            }

            //i = k (I kiek knygu ar daliu reikia padalinti)
            for(int i = 1; i < books; i++)
            {
                //j = n (Skyriu skaicius)
                resultTable[i] = new int[chaptersCount];
                for(int j = 0; j < books; j++)
                {
                    resultTable[i][0] = allSummed[0];
                }
                for(int j = 1; j < chaptersCount; j++)
                {
                    //g = j
                    List<int> results = new List<int>();
                    for(int g = j; g >= 1; g--)
                    {
                        int result1 = allSummed[j] - allSummed[g - 1];
                        int result2 = resultTable[i - 1][g - 1];
                        results.Add(Math.Max(result1, result2));
                    }

                    resultTable[i][j] = results.Min();
                }
            }


            return resultTable[books - 1].Distinct().ToArray();
        }
    }
}
