using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Windows;
using SIMS.Model;

namespace SIMS.Daemon.PremestajOpreme
{
    public class InventoryMovingCommand
    {
        public DateTime DateTime { get; set; }
        public string SrcID { get; set; }
        public string DstID { get; set; }
        public string OpremaID { get; set; }
        public int Delta { get; set; }
        public InventoryMovingCommand(DateTime dateTime, string srcID, string dstID, string opremaID, int delta)
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

            RoomInventory src = RoomInventoryFileRepository.Instance.ReadNoConsistifying(SrcID, OpremaID);
            RoomInventory dst = RoomInventoryFileRepository.Instance.ReadNoConsistifying(DstID, OpremaID);

            if (src.Kolicina < Delta)
            {
                MessageBox.Show("Premeštaj opreme " + OpremaID + " iz prostorije " + SrcID + " u prostoriju " + DstID + " nije moguć zbog manjka opreme.");
                return;
            }

            src.Kolicina -= Delta;
            dst.Kolicina += Delta;

            RoomInventoryFileRepository.Instance.Update(src);
            RoomInventoryFileRepository.Instance.Update(dst);
        }
    }
}
