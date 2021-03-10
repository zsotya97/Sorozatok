using System;
using System.Collections.Generic;
using  System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorozatok
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<string> beolvas = File.ReadAllLines("lista.txt");
            List<Adatok> adatok = new List<Adatok>();
            string temp = null;
            int i= 0;
            foreach (var item in beolvas)
            {

                i++;
                temp += $"{item}";
                if(i%5==0)
                {
                    adatok.Add(new Adatok(temp));
                    temp = "";
                    i = 0;
                }
                else
                {
                    temp += ";";
                    
                }
                
            }
            Console.WriteLine($"2.feladat\n" +
                $"A listában {adatok.Count(x=>x.Datum!="NI")} db vetítési dátummal rendelkező epizód van\n");
            double szazalek = (adatok.Count(x => x.Nezte == true));
            double eredmeny = szazalek / adatok.Count * 100;
            Console.WriteLine(szazalek);
            Console.WriteLine($"3.feladat\n" +
                $"A listában lévő epizódok {eredmeny:##.##}%-át látta\n");
            int masodperc = adatok.Where(x=>x.Nezte==true).Sum(x=>x.Perc*60);
            int ora = masodperc / 3600;
            int nap = ora /24;
            int orak = ora - nap * 24;
            int maradekperc = (masodperc - (ora * 3600))/60;
            Console.WriteLine($"4.feladat\n" +
                $"Filmnézéssel {nap} napot, {orak} órát, és {maradekperc} percet töltött.\n"); 
            Console.Write($"5.feladat\n" +
                $"Adjon meg egy dátumot! Dátum= ");
            string dTemp = Console.ReadLine();
            var datumos = adatok.Where(x => x.Datum == dTemp);
            if (datumos.Count()==0) Console.WriteLine("Nem volt ilyen adat!!");
            else
            {
                foreach (var item in datumos)
                {
                    Console.WriteLine($"{item.Evad}x{item.Epizod}\t{item.Angol}");
                }
            }
            Console.Write($"\n7.feladat\n" +
                $"Adja meg a hét egy napját (például cs) ! Nap= ");
            string hetnap = Console.ReadLine();
            var napok = adatok.Where(x => x.Hetnapja == hetnap).GroupBy(x => x.Angol);
            if (napok.Count()==0) Console.WriteLine("Az adott napon nem került adásba sorozat!");
            else
            {
                foreach (var item in napok)
                {
                    Console.WriteLine($"{item.Key}");
                }
            }

            var osszesitett = from x in adatok
                              group x by x.Angol into lista
                              select new { Film = lista.Key, ido = lista.Sum(x => x.Perc), Szama = lista.Count() };
            StreamWriter ki = new StreamWriter("summa.txt", false);
            Console.WriteLine("\n8.feladat: Statisztika");
            foreach (var item in osszesitett)
            {
                
                
                    ki.WriteLine($"{item.Film} {item.ido} {item.Szama}");
            }
            ki.Close();
            
        }

        
    }
}
