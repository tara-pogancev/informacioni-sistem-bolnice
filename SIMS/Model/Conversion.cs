using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    public class Conversion
    {
        public static string RoomTypeToString(RoomType tip)
        {
            return tip switch
            {
                RoomType.eximantionRoom => "Prostorija za preglede",
                RoomType.operatingRoom => "Operaciona sala",
                RoomType.patientRoom => "Bolesnička soba",
                _ => "",
            };
        }

        public static string InventoryTypeToString(InventoryType tip)
        {
            return tip switch
            {
                InventoryType.dinamička => "dinamička",
                InventoryType.statička => "statička",
                _ => "",
            };
        }

        public static InventoryType StringToInventoryType(string str)
        {
            return str switch
            {
                "dinamička" => InventoryType.dinamička,
                "statička" => InventoryType.statička,
                _ => InventoryType.dinamička,
            };
        }

        public static RoomType StringToRoomType(string str)
        {
            return str switch
            {
                "Prostorija za preglede" => RoomType.eximantionRoom,
                "Operaciona sala" => RoomType.operatingRoom,
                "Bolesnička soba" => RoomType.patientRoom,
                _ => RoomType.patientRoom,
            };
        }

        public static List<string> GetRoomTypes()
        {
            List<string> tipovi = new List<string>();
            foreach (RoomType tip in Enum.GetValues(typeof(RoomType)))
            {
                string s = RoomTypeToString(tip);
                tipovi.Add(s);
            }
            return tipovi;
        }

        public static List<string> GetInventoryTypes()
        {
            List<string> tipovi = new List<string>();
            foreach (InventoryType tip in Enum.GetValues(typeof(InventoryType)))
            {
                string s = InventoryTypeToString(tip);
                tipovi.Add(s);
            }
            return tipovi;
        }

        public static string RoomAvailabilityToString(bool dostupnost)
        {
            return (dostupnost ? "Dostupna" : "Nedostupna");
        }

        public static bool StringToRoomAvailability(string dostupnost)
        {
            return dostupnost.Equals("Dostupna");
        }

        public static List<string> GetRoomAvailabilities()
        {
            List<string> tipovi = new List<string>();
            tipovi.Add("Dostupna");
            tipovi.Add("Nedostupna");
            return tipovi;
        }


    }
}
