using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;
using SIMS.Repositories.InventoryMovingCommandRepo;
using SIMS.Repositories.RoomInventoryRepo;

namespace SIMS.Repositories.InventoryRepo
{
    public class InventoryFileRepository : GenericFileRepository<string, Inventory, InventoryFileRepository>,IInventoryRepository
    {
        protected override string getPath()
        {
            return @".\..\..\..\Data\oprema.json";
        }
        protected override string getKey(Inventory dinamickaOprema)
        {
            return dinamickaOprema.ID;
        }

        protected override void RemoveReferences(string key)
        {
            foreach (var prosInv in RoomInventoryFileRepository.Instance.ReadAll().Values)
            {
                if (prosInv.ID == key)
                {
                    RoomInventoryFileRepository.Instance.Delete(prosInv);
                }
            }

            foreach (var command in InventoryMovingCommandFileRepository.Instance.ReadAll().Values)
            {
                if (command.InventoryID == key)
                {
                    InventoryMovingCommandFileRepository.Instance.Delete(command);
                }
            }
        }

        protected override void ShouldSerialize(Inventory entity)
        {
            //ne treba logika za serijalizaciju
        }
    }
}
