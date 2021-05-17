using System;
using System.Collections.Generic;
using System.Text;
using Model;
using SIMS.Daemon;
using SIMS.Daemon.PremestajOpreme;

namespace Model
{
    class InventoryMovingCommandStorage : Repository<string, InventoryMovingCommand, InventoryMovingCommandStorage>
    {
        protected override string getKey(InventoryMovingCommand entity)
        {
            return entity.OpremaID + "_" + entity.DstID + "_" + entity.SrcID + "_" + entity.DateTime;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\premestajOpremeQueue.json";
        }

        protected override void RemoveReferences(string key)
        {
        }
    }
}
