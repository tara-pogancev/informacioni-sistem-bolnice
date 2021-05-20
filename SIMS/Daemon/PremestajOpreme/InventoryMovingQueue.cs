using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace SIMS.Daemon.PremestajOpreme
{
    public class InventoryMovingQueue
    {
        private static InventoryMovingQueue _instance = new InventoryMovingQueue();
        public static InventoryMovingQueue Instance
        {
            get
            {
                return _instance;
            }
        }

        public void PushCommand(InventoryMovingCommand command)
        {
            InventoryMovingCommandStorage.Instance.CreateOrUpdateEntity(command);
        }

        public void Consistify()
        {
            foreach (var command in InventoryMovingCommandStorage.Instance.ReadAll().Values)
            {
                if (command.DateTime <= DateTime.Now)
                {
                    InventoryMovingCommandStorage.Instance.DeleteEntity(command);
                    command.Execute();
                }
            }
        }
    }
}

