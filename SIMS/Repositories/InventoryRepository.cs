using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class InventoryRepository : GenericFileRepository<string, Inventory, InventoryRepository>
    {
        protected override string getPath()
        {
            return @".\..\..\..\Data\oprema.json";
        }
        protected override string getKey(Inventory dinamickaOprema)
        {
            return dinamickaOprema.Id;
        }

        protected override void RemoveReferences(string key)
        {
            foreach (var prosInv in RoomInventoryRepository.Instance.ReadAll().Values)
            {
                if (prosInv.IdInventara == key)
                {
                    RoomInventoryRepository.Instance.Delete(prosInv);
                }
            }

            foreach (var command in InventoryMovingCommandStorage.Instance.ReadAll().Values)
            {
                if (command.OpremaID == key)
                {
                    InventoryMovingCommandStorage.Instance.Delete(command);
                }
            }
        }
    }
}
