using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace SIMS.Model
{
    public class DataTermini
    {
        public List<Termin> Data { get; set; }

        public DataTermini()
        {
            Data = TerminStorage.Instance.ReadList();
        }

    }
}
