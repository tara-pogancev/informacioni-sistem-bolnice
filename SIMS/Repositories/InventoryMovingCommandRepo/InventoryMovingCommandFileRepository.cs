using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Repositories.SecretaryRepo;
using SIMS.Daemon;
using SIMS.Daemon.PremestajOpreme;
using SIMS.Repositories.InventoryMovingCommandRepo;

namespace SIMS.Repositories.InventoryMovingCommandRepo
{
    class InventoryMovingCommandFileRepository : GenericFileRepository<string, InventoryMovingCommand, InventoryMovingCommandFileRepository>,IInventoryMovingCommandRepository
    {
        protected override string getKey(InventoryMovingCommand entity)
        {
            return entity.InventoryID + "_" + entity.DestinationRoomNumber + "_" + entity.SourceRoomNumber + "_" + entity.DateTime;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\premestajOpremeQueue.json";
        }

        protected override void RemoveReferences(string key)
        {
        }

        protected override void ShouldSerialize(InventoryMovingCommand entity)
        {
           //ne treba logika za serijalizaciju
        }
    }
}
