using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Repositories.SecretaryRepo;
using SIMS.Daemon.PremestajOpreme;
using SIMS.Model;

namespace SIMS.Repositories.RoomInventoryRepo
{
    class RoomInventoryFileRepository : GenericFileRepository<string, RoomInventory, RoomInventoryFileRepository>,IRoomInventoryRepository
    {
        protected override string getKey(RoomInventory entity)
        {
            return entity.RoomNumber + "_" + entity.ID;
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
            var ret = FindById(brojProstorije + "_" + idInventara);
            if (ret == null)
            {
                ret = new RoomInventory(brojProstorije, idInventara, 0);
                Save(ret);
            }
            return ret;
        }

        protected override void ShouldSerialize(RoomInventory entity)
        {
            
        }
    }
}
