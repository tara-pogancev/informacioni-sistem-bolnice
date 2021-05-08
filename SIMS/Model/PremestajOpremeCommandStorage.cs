using System;
using System.Collections.Generic;
using System.Text;
using Model;
using SIMS.Daemon;
using SIMS.Daemon.PremestajOpreme;

namespace Model
{
    class PremestajOpremeCommandStorage : Storage<string, PremestajOpremeCommand, PremestajOpremeCommandStorage>
    {
        protected override string getKey(PremestajOpremeCommand entity)
        {
            return entity.OpremaID + "_" + entity.DstID + "_" + entity.SrcID + "_" + entity.DateTime;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\premestajOpremeQueue.json";
        }

        protected override void RemoveReferences(string key)
        {
        }
    }
}
