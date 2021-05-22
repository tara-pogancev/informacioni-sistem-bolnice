using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Repositories.SecretaryRepo;
using SIMS.Daemon;
using SIMS.Daemon.PremestajOpreme;
using SIMS.Repositories.InventoryMovingCommandRepo;

namespace SIMS.Repositories.SecretaryRepo
{
    class InventoryMovingCommandFileRepository : GenericFileRepository<string, InventoryMovingCommand, InventoryMovingCommandFileRepository>,IInventoryMovingCommandRepository
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
