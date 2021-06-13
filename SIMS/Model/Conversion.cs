using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    public class Conversion
    {
        public static string TipProstorijeToString(RoomType tip)
        {
            return tip switch
            {
                RoomType.eximantionRoom => "Prostorija za preglede",
                RoomType.operatingRoom => "Operaciona sala",
                RoomType.patientRoom => "Bolesnička soba",
                _ => "",
            };
        }

        public static string TipOpremeToString(InventoryType tip)
        {
            return tip switch
            {
                InventoryType.dinamička => "dinamička",
                InventoryType.statička => "statička",
                _ => "",
            };
        }

        public static InventoryType StringToTipOpreme(string str)
        {
            return str switch
            {
                "dinamička" => InventoryType.dinamička,
                "statička" => InventoryType.statička,
                _ => InventoryType.dinamička,
            };
        }

        public static RoomType StringToTipProstorije(string str)
        {
            return str switch
            {
                "Prostorija za preglede" => RoomType.eximantionRoom,
                "Operaciona sala" => RoomType.operatingRoom,
                "Bolesnička soba" => RoomType.patientRoom,
                _ => RoomType.patientRoom,
            };
        }

        public static List<string> GetTipoviProstorije()
        {
            List<string> tipovi = new List<string>();
            foreach (RoomType tip in Enum.GetValues(typeof(RoomType)))
            {
                string s = TipProstorijeToString(tip);
                tipovi.Add(s);
            }
            return tipovi;
        }

        public static List<string> GetTipoviOpreme()
        {
            List<string> tipovi = new List<string>();
            foreach (InventoryType tip in Enum.GetValues(typeof(InventoryType)))
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

        public static List<string> GetDostupnostiProstorije()
        {
            List<string> tipovi = new List<string>();
            tipovi.Add("Dostupna");
            tipovi.Add("Nedostupna");
            return tipovi;
        }


    }
}
