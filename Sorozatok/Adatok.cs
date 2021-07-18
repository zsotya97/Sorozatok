using System;
using System.Collections.Generic;

namespace Sorozatok
{
    class Adatok
    {
        public DateTime Datum { get; set; }
        public string Angol { get; set; }
        public int Evad { get; set; }
        public int Epizod { get; set; }
        public int Perc { get; set; }
        public bool Nezte { get; set; }
        public Adatok(string sor)
        {
            DateTime ido;
            IList<string> temp = sor.Split(';');
            DateTime.TryParse(temp[0], out ido);
            Datum = ido;
            Angol = temp[1];
            Evad = int.Parse(temp[2].Split('x')[0]);
            Epizod = int.Parse(temp[2].Split('x')[1]);
            Perc = int.Parse(temp[3]);
            Nezte = int.Parse(temp[4]) == 1;
        }
        public string Hetnapja
        {
            get
            {
                try
                {
                    int ev = Datum.Year;
                    int ho = Datum.Month;
                    int nap = Datum.Day;
                    string[] napok = { "v", "h", "k", "sze", "cs", "p", "szo" };
                    int[] honapok = { 0, 3, 2, 5, 0, 3, 5, 1, 4, 6, 2, 4 };
                    if (ho < 3) ev--;
                    string hetnapja = napok[(ev + ev / 4 - ev / 100 + ev / 400 + honapok[ho - 1] + nap) % 7];
                    return hetnapja;
                }
                catch (Exception)
                {

                    return "ismeretlen";
                }
            }
        }

    }
}
