using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Conversion
    {
        public static string TipProstorijeToString(TipProstorije tip)
        {
            return tip switch
            {
                TipProstorije.zaPreglede => "Prostorija za preglede",
                TipProstorije.sala => "Operaciona sala",
                TipProstorije.bolesnicka => "Bolesnička soba",
                _ => "",
            };
        }

        public static string TipOpremeToString(TipOpreme tip)
        {
            return tip switch
            {
                TipOpreme.dinamička => "dinamička",
                TipOpreme.statička => "statička",
                _ => "",
            };
        }

        public static TipOpreme StringToTipOpreme(string str)
        {
            return str switch
            {
                "dinamička" => TipOpreme.dinamička,
                "statička" => TipOpreme.statička,
                _ => TipOpreme.dinamička,
            };
        }

        public static TipProstorije StringToTipProstorije(string str)
        {
            return str switch
            {
                "Prostorija za preglede" => TipProstorije.zaPreglede,
                "Operaciona sala" => TipProstorije.sala,
                "Bolesnička soba" => TipProstorije.bolesnicka,
                _ => TipProstorije.bolesnicka,
            };
        }

        public static List<string> getTipoviProstorije()
        {
            List<string> tipovi = new List<string>();
            foreach (TipProstorije tip in Enum.GetValues(typeof(TipProstorije)))
            {
                string s = TipProstorijeToString(tip);
                tipovi.Add(s);
            }
            return tipovi;
        }

        public static List<string> getTipoviOpreme()
        {
            List<string> tipovi = new List<string>();
            foreach (TipOpreme tip in Enum.GetValues(typeof(TipOpreme)))
            {
                string s = TipOpremeToString(tip);
                tipovi.Add(s);
            }
            return tipovi;
        }

        public static string DostupnostProstorijeToString(bool dostupnost)
        {
            return (dostupnost ? "Dostupna" : "Nedostupna");
        }

        public static bool StringToDostupnostProstorije(string dostupnost)
        {
            return dostupnost.Equals("Dostupna");
        }

        public static List<string> getDostupnostiProstorije()
        {
            List<string> tipovi = new List<string>();
            tipovi.Add("Dostupna");
            tipovi.Add("Nedostupna");
            return tipovi;
        }


    }
}
