using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    class AnketaBolnice:Anketa
    {
        private Dictionary<string, int> odgovoriNaPitanja;

        public AnketaBolnice() : base()
        {
            odgovoriNaPitanja = new Dictionary<string, int>();
        }

        public Dictionary<string, int> OdgovoriNaPitanja { get => odgovoriNaPitanja; set => odgovoriNaPitanja = value; }
    }
}
