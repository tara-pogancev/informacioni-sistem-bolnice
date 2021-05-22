using Model;
using SIMS.Daemon.PremestajOpreme;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.InventoryMovingCommandRepo
{
    class InventoryMovingCommandFileRepository : GenericFileRepository<string, InventoryMovingCommand, InventoryMovingCommandStorage>,IInventoryMovingCommandRepository
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
