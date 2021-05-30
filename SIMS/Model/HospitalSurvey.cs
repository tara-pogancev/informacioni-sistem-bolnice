using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    class HospitalSurvey:Survey
    {
        private Dictionary<string, int> answers;
        private int numberOfCheckups;

        public HospitalSurvey() : base()
        {
            answers = new Dictionary<string, int>();
        }

        public Dictionary<string, int> Answers { get => answers; set => answers = value; }
        public int NumberOfCheckups { get => numberOfCheckups; set => numberOfCheckups= value; }
    }
}
