using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace lab_2_prog_in
{
    class Program
    {
        static void Main(string[] args)
        {
            myProgram mp = new myProgram();
            mp.program();
        }
    }

    class myProgram
    {
        Dictionary<string, int> userStories;
        int count;
        int V;
        List<Dictionary<string, int>> iterations;
        int countIter=0;
        public void program()
        {
            getElems("lab2-2.txt");
            foreach (var el in userStories)
                Console.WriteLine(el);
            userStories = userStories.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            int minCountIter = Convert.ToInt32(Math.Ceiling((double)userStories.Values.Sum() /(double)V));
            setIter();
            output(iterations, countIter);
            Console.ReadKey();
        }
        public void setIter()
        {
            iterations = new List<Dictionary<string, int>>();
            iterations.Add(new Dictionary<string, int>());
            int i = 1;
            int j = 0;
            int sum = 0;
            iterations[0].Add(userStories.Keys.ElementAt(0), userStories.Values.ElementAt(0));
            while (i < count)
            {
                bool b = false;
                foreach (var iter in iterations)
                {
                    sum = iter.Values.Sum();
                    if (V - sum - userStories.Values.ElementAt(i) >= 0)
                    {
                        iter.Add(userStories.Keys.ElementAt(i), userStories.Values.ElementAt(i));
                        b = true;
                        break;
                    }
                    sum = 0;
                }
                if (!b)
                {
                    iterations.Add(new Dictionary<string, int>());
                    j++;
                    iterations[j].Add(userStories.Keys.ElementAt(i), userStories.Values.ElementAt(i));
                }
                sum = 0;
                i++;
            }
            countIter = j;
        }
        public void getElems(string file)
        {
            StreamReader sr = new StreamReader(file);
            string s = sr.ReadLine();
            V = Int16.Parse(s.Remove(s.IndexOf(' '), s.Length - s.IndexOf(' ')));
            count = Int16.Parse(s.Remove(0, s.IndexOf(' ') + 1));
            Console.WriteLine("скорость - {0},  количество - {1} ", V, count);
            userStories = new Dictionary<string, int>(count);
            for (int k = 0; k < count; k++)
            {
                s = sr.ReadLine();
                string name = s.Remove(s.IndexOf(' '), s.Length - s.IndexOf(' '));
                int compl = Int16.Parse(s.Remove(0, s.IndexOf(' ') + 1));
                userStories.Add(name, compl);
            }
        }
        public void output(List<Dictionary<string,int>> iterations, int j)
        {
            for (int k = 0; k <= j; k++)
            {
                Console.WriteLine(k + 1 + "-я итерация:");
                foreach (var el in iterations[k])
                {
                    Console.WriteLine(el);

                }
                Console.WriteLine("\n");
            }
        }
    }
}
