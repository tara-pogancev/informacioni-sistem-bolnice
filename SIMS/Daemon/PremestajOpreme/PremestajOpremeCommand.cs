using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Windows;

namespace SIMS.Daemon.PremestajOpreme
{
    public class PremestajOpremeCommand
    {
        public DateTime DateTime { get; set; }
        public string SrcID { get; set; }
        public string DstID { get; set; }
        public string OpremaID { get; set; }
        public int Delta { get; set; }
        public PremestajOpremeCommand(DateTime dateTime, string srcID, string dstID, string opremaID, int delta)
        {
            DateTime = dateTime;
            SrcID = srcID;
            DstID = dstID;
            OpremaID = opremaID;
            Delta = delta;
        }

        public void Execute()
        {
            if (SrcID == DstID)
            {
                return;
            }

            ProsInv src = ProsInvStorage.Instance.ReadNoConsistifying(SrcID, OpremaID);
            ProsInv dst = ProsInvStorage.Instance.ReadNoConsistifying(DstID, OpremaID);

            if (src.Kolicina < Delta)
            {
                MessageBox.Show("Premeštaj opreme " + OpremaID + " iz prostorije " + SrcID + " u prostoriju " + DstID + " nije moguć zbog manjka opreme.");
                return;
            }

            src.Kolicina -= Delta;
            dst.Kolicina += Delta;

            ProsInvStorage.Instance.Update(src);
            ProsInvStorage.Instance.Update(dst);
        }
    }
}
