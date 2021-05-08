using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace SIMS.Daemon.PremestajOpreme
{
    public class PremestajOpremeQueue
    {
        private static PremestajOpremeQueue _instance = new PremestajOpremeQueue();
        public static PremestajOpremeQueue Instance
        {
            get
            {
                return _instance;
            }
        }

        public void PushCommand(PremestajOpremeCommand command)
        {
            PremestajOpremeCommandStorage.Instance.CreateOrUpdate(command);
        }

        public void Consistify()
        {
            foreach (var command in PremestajOpremeCommandStorage.Instance.ReadAll().Values)
            {
                if (command.DateTime <= DateTime.Now)
                {
                    PremestajOpremeCommandStorage.Instance.Delete(command);
                    command.Execute();
                }
            }
        }
    }
}

