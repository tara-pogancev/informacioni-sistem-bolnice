﻿using System;
using System.Collections.Generic;
using System.Text;
using Model;
using SIMS.Daemon.PremestajOpreme;

namespace Model
{
    class RoomInventoryRepository : GenericFileRepository<string, RoomInventory, RoomInventoryRepository>
    {
        protected override string getKey(RoomInventory entity)
        {
            return entity.BrojProstorije + "_" + entity.IdInventara;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\prosinv.json";
        }

        protected override void RemoveReferences(string key)
        {

        }

        public RoomInventory Read(string brojProstorije, string idInventara)
        {
            InventoryMovingQueue.Instance.Consistify();
            return ReadNoConsistifying(brojProstorije, idInventara);
        }

        public RoomInventory ReadNoConsistifying(string brojProstorije, string idInventara)
        {
            var ret = ReadEntity(brojProstorije + "_" + idInventara);
            if (ret == null)
            {
                ret = new RoomInventory(brojProstorije, idInventara, 0);
                CreateEntity(ret);
            }
            return ret;
        }
    }
}
