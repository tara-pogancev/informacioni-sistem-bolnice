﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.SecretaryRepo
{
    public class Inventory
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public InventoryType Type { get; set; }

        public string RoomNumber { get; set; } //neophodno za bindovanje

        public Inventory()
        {
            Name = "";
            ID = "";
            Type = InventoryType.statička;
        }
        public Inventory(string naziv, string id, InventoryType TipOpreme)
        {
            this.Name = naziv;
            this.ID = id;
            this.Type = TipOpreme;
        }

        public int Amount
        {
            get
            {
                if (RoomNumber == null)
                {
                    return 0;
                }
                return RoomInventoryFileRepository.Instance.Read(RoomNumber, ID).Kolicina;
            }
        }

        public string TypeToString
        {
            get
            {
                return Conversion.TipOpremeToString(Type);
            }
        }

    }
}
