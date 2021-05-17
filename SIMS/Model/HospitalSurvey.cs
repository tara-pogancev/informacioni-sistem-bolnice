using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    class HospitalSurvey:Survey
    {
        private Dictionary<string, int> odgovoriNaPitanja;
        private int trenutniBrojPregleda;

        public HospitalSurvey() : base()
        {
            odgovoriNaPitanja = new Dictionary<string, int>();
        }

        public Dictionary<string, int> OdgovoriNaPitanja { get => odgovoriNaPitanja; set => odgovoriNaPitanja = value; }
        public int TrenutniBrojPregleda { get => trenutniBrojPregleda; set => trenutniBrojPregleda = value; }
    }
}
