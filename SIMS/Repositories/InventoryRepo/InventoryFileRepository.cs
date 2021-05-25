using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Repositories.SecretaryRepo
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
                if (prosInv.IdInventara == key)
                {
                    RoomInventoryFileRepository.Instance.Delete(prosInv);
                }
            }

            foreach (var command in InventoryMovingCommandFileRepository.Instance.ReadAll().Values)
            {
                if (command.OpremaID == key)
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
