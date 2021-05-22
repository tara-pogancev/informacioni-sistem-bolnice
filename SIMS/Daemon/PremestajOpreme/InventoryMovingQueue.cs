using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Repositories.PatientRepo;

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
            InventoryMovingCommandFileRepository.Instance.CreateOrUpdate(command);
        }

        public void Consistify()
        {
            foreach (var command in InventoryMovingCommandFileRepository.Instance.ReadAll().Values)
            {
                if (command.DateTime <= DateTime.Now)
                {
                    InventoryMovingCommandFileRepository.Instance.Delete(command);
                    command.Execute();
                }
            }
        }
    }
}

