// File:    Prostorija.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 6:41:35 PM
// Purpose: Definition of Class Prostorija

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SIMS.Repositories.SecretaryRepo
{
    public class Room
    {
        public DateTime? RenovationStart { get; set; }
        public DateTime? RenovationEnd { get; set; }
        public Dictionary<string, int> InventoryAmounts
        {
            get; set;
        }

        public Room()
        {
            InventoryAmounts = new Dictionary<string, int>();
            this.Number = "";
            this.RoomType = RoomType.bolesnicka;
            Serialize = true;
        }

        public Room(string Number, RoomType RoomType)
        {
            InventoryAmounts = new Dictionary<string, int>();
            this.Number = Number;
            this.RoomType = RoomType;
            Serialize = true;
        }

        public Room(string Number, RoomType RoomType, Dictionary<string, int> InventoryAmounts)
        {
            this.Number = Number;
            this.RoomType = RoomType;
            this.InventoryAmounts = InventoryAmounts;
            Serialize = true;
        }


        [JsonIgnore]
        public string AvailableToString
        {
            get
            {
                return Conversion.DostupnostProstorijeToString(Available);
            }
        }

        [JsonIgnore]
        public string TypeToString
        {
            get
            {
                return Conversion.TipProstorijeToString(RoomType);
            }
        }
        
        public RoomType RoomType { get; set; }
        public string Number { get; set; }
        public bool Available {
            get
            {
                bool renovating = false;
                if (RenovationStart != null && RenovationEnd != null)
                {
                    renovating = RenovationStart < DateTime.Now && RenovationEnd > DateTime.Now;
                }
                return !renovating;
            }
        }
        
        public bool Serialize { get; set; }

        public bool ShouldSerializeAvailable()
        {
            return Serialize;
        }

        public bool ShouldSerializeRoomType()
        {
            return Serialize;
        }

        public bool ShouldSerializeInventoryAmount()
        {
            return Serialize;
        }

        public bool ShouldSerializeRenovationStart()
        {
            return Serialize;
        }

        public bool ShouldSerializeRenovationEnd()
        {
            return Serialize;
        }

    }
}