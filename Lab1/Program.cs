using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Lab1
{
    public class Tabel
    {
        public int num;
        public int a;
        public int b;
        public int c;
        public Tabel()
        {

        }
        public Tabel(int num, int a, int b)
        {
            this.num = num;
            this.a = a;
            this.b = b;
        }
        public Tabel(int num, int a, int b, int c)
        {
            this.num = num;
            this.a = a;
            this.b = b;
            this.c = c;
        }
    }
    class Program
    {
        public static void Swap(List<Tabel> tab, int i, int j)
        {
            Tabel mid;
            mid = tab[j];
            tab[j] = tab[i];
            tab[i] = mid;
        }
        public static void NX2(List<Tabel> tab)
        {
            int[] help = new int[4];
            int min;
            for(int i = 0; i < tab.Count - 1; i++)
            {
                for(int j = 0; j < tab.Count - i - 1; j++)
                {
                    help[0] = tab[j].a;
                    help[1] = tab[j].b;
                    help[2] = tab[j + 1].a;
                    help[3] = tab[j + 1].b;
                    min = help.Min();
                    if(min == help[1] || min == help[2])
                    {
                        Swap(tab, j, j + 1);
                    }
                }
            }
        }
        public static void NX3(List<Tabel> tab)
        {
            int minA = tab.Min(tabel => tabel.a), maxB = tab.Max(tabel => tabel.b), minC = tab.Min(tabel => tabel.c);
            Console.WriteLine("min a = " + minA + " max b = " + maxB + " min c = " + minC);
            if(minA >= maxB || minC >= maxB)
            {
                Console.WriteLine("Условие (min a >= max b) || (min c >= max b) выполнено");
                List<Tabel> twoTab = new List<Tabel>();
                for(int i = 0; i < tab.Count; i++)
                {
                    twoTab.Add(new Tabel(tab[i].num, tab[i].a + tab[i].b, tab[i].b + tab[i].c));
                }
                NX2(twoTab);
                int index;
                for(int i = 0; i < tab.Count; i++)
                {
                    index = twoTab.FindIndex(x => x.num == tab[i].num);
                    Swap(tab, i, index);
                    if (i != index) i--;
                }
            }
            else
            {
                Console.WriteLine("Условие (min a >= max b) || (min c >= max b) невыполнено");
                Perebor(tab);
            }
        }

        public static void Perebor(List<Tabel> tab)
        {
            int j = tab.Count - 2;
            int k, l, r;
            int midT, minT = -1;
            List<Tabel> minTab = new List<Tabel>(tab);
            minT = FindTime(tab);
            while(j != -1)
            {
                while (j != -1 && tab[j].num >= tab[j + 1].num) j--;
                if (j != -1)
                {
                    k = tab.Count - 1;
                    while (tab[j].num >= tab[k].num) k--;
                    Swap(tab, j, k);
                    l = j + 1;
                    r = tab.Count - 1;
                    while (l < r)
                    {
                        Swap(tab, l, r);
                        l++;
                        r--;
                    }
                    midT = FindTime(tab);
                    if (midT < minT)
                    {
                        minTab = new List<Tabel>(tab);
                        minT = midT;
                    }
                }
            }
            tab = minTab;
        }

        public static int FindTime(List<Tabel> tab)
        {
            int Ta, Tb, Tc;
            Ta = Tb = Tc = 0;
            for (int i = 0; i < tab.Count; i++)
            {
                Ta += tab[i].a;
                if (Ta - Tb > 0)
                {
                    Tb += Ta - Tb;
                }
                Tb += tab[i].b;
                if (Tb - Tc > 0)
                {
                    Tc += Tb - Tc;
                }
                Tc += tab[i].c;
            }
            return Tc;
        }
        public static void ShowTabel(List<Tabel> tab, int type)
        {
            if(type == 1)
            {
                Console.WriteLine("*****************");
                Console.WriteLine("  №  *  a  *  b  ");
                Console.WriteLine("*****************");
            }
            else
            {
                Console.WriteLine("***********************");
                Console.WriteLine("  №  *  a  *  b  *  c  ");
                Console.WriteLine("***********************");
            }
            for(int i = 0; i < tab.Count; i++)
            {
                if(type == 1)
                {
                    Console.WriteLine(tab[i].num.ToString().PadLeft(5) + "*" + tab[i].a.ToString().PadLeft(5) + "*" +
                    tab[i].b.ToString().PadLeft(5));
                    Console.WriteLine("*****************");
                }
                else
                {
                    Console.WriteLine(tab[i].num.ToString().PadLeft(5) + "*" + tab[i].a.ToString().PadLeft(5) + "*" +
                    tab[i].b.ToString().PadLeft(5) + "*" + tab[i].c.ToString().PadLeft(5));
                    Console.WriteLine("***********************");
                }
            }
        }
        public static int Gant(List<Tabel> tab, int type)
        {
            Console.Write("A: ");
            for(int i = 0; i < tab.Count; i++)
            {
                Console.Write(string.Concat(Enumerable.Repeat(tab[i].num, tab[i].a)));
            }
            Console.WriteLine();
            Console.Write("B: ");
            int Ta, Tb;
            Ta = Tb = 0;
            for(int i = 0; i < tab.Count; i++)
            {
                Ta += tab[i].a;
                if(Ta - Tb > 0)
                {
                    Console.Write(string.Concat(Enumerable.Repeat("x", Ta - Tb)));
                    Tb += Ta - Tb;
                }
                Console.Write(string.Concat(Enumerable.Repeat(tab[i].num, tab[i].b)));
                Tb += tab[i].b;
            }
            if(type == 2)
            {
                Console.WriteLine();
                Console.Write("C: ");
                int Tc;
                Ta = Tb = Tc = 0;
                for (int i = 0; i < tab.Count; i++)
                {
                    Ta += tab[i].a;
                    if (Ta - Tb > 0)
                    {
                        Tb += Ta - Tb;
                    }
                    Tb += tab[i].b;
                    if (Tb - Tc > 0)
                    {
                        Console.Write(string.Concat(Enumerable.Repeat("y", Tb - Tc)));
                        Tc += Tb - Tc;
                    }
                    Console.Write(string.Concat(Enumerable.Repeat(tab[i].num, tab[i].c)));
                    Tc += tab[i].c;
                }
                Console.WriteLine();
                return Tc;
            }
            Console.WriteLine();
            return Tb;
        }
        public static void ShowWay(List<Tabel> tab)
        {
            for(int i = 0; i < tab.Count; i++)
            {
                Console.Write((i + 1) + "(" + tab[i].num + "),");
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            StreamReader stream = new StreamReader(Console.ReadLine());
            string line;
            string[] data = new string[5];
            int help;
            List<Tabel> NX2Tabel = new List<Tabel>();
            List<Tabel> NX3Tabel = new List<Tabel>();
            List<Tabel> PereborTabel = new List<Tabel>();
            line = stream.ReadLine();
            while(line != "")
            {
                data = line.Split(' ');
                NX2Tabel.Add(new Tabel(Convert.ToInt32(data[0]), Convert.ToInt32(data[1]), Convert.ToInt32(data[2])));
                line = stream.ReadLine();
            }
            line = stream.ReadLine();
            while (line != "")
            {
                data = line.Split(' ');
                NX3Tabel.Add(new Tabel(Convert.ToInt32(data[0]), Convert.ToInt32(data[1]), Convert.ToInt32(data[2]), Convert.ToInt32(data[3])));
                line = stream.ReadLine();
            }
            while ((line = stream.ReadLine()) != null)
            {
                data = line.Split(' ');
                PereborTabel.Add(new Tabel(Convert.ToInt32(data[0]), Convert.ToInt32(data[1]), Convert.ToInt32(data[2]), Convert.ToInt32(data[3])));
            }
            stream.Close();
            Console.WriteLine("1 задание");
            Console.WriteLine("Исходная таблица");
            ShowTabel(NX2Tabel, 1);
            Console.WriteLine("Диаграмма Ганта");
            help = Gant(NX2Tabel, 1);
            Console.WriteLine("Время = " + help);
            NX2(NX2Tabel);
            Console.WriteLine("Оптимальная последовательность");
            ShowWay(NX2Tabel);
            Console.WriteLine("Таблица");
            ShowTabel(NX2Tabel, 1);
            Console.WriteLine("Диаграмма Ганта");
            help = Gant(NX2Tabel, 1);
            Console.WriteLine("Время = " + help);
            //********************************
            //2 задание
            //********************************
            Console.WriteLine("2 задание");
            Console.WriteLine("Исходная таблица");
            ShowTabel(NX3Tabel, 2);
            Console.WriteLine("Диаграмма Ганта");
            help = Gant(NX3Tabel, 2);
            Console.WriteLine("Время = " + help);
            NX3(NX3Tabel);
            Console.WriteLine("Оптимальная последовательность");
            ShowWay(NX3Tabel);
            Console.WriteLine("Таблица");
            ShowTabel(NX3Tabel, 2);
            Console.WriteLine("Диаграмма Ганта");
            help = Gant(NX3Tabel, 2);
            Console.WriteLine("Время = " + help);
            //********************************
            //3 задание
            //********************************
            Console.WriteLine("3 задание");
            Console.WriteLine("Исходная таблица");
            ShowTabel(PereborTabel, 2);
            Console.WriteLine("Диаграмма Ганта");
            help = Gant(PereborTabel, 2);
            Console.WriteLine("Время = " + help);
            NX3(PereborTabel);
            Console.WriteLine("Оптимальная последовательность");
            ShowWay(PereborTabel);
            Console.WriteLine("Таблица");
            ShowTabel(PereborTabel, 2);
            Console.WriteLine("Диаграмма Ганта");
            help = Gant(PereborTabel, 2);
            Console.WriteLine("Время = " + help);
            Console.ReadLine();
        }
    }
}
