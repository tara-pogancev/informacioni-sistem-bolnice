using SIMS.Commands;
using SIMS.UpravnikGUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace SIMS.ViewManager.ViewModel
{
    public class IzvestajPageViewModel
    {
        public DateTime pocetakInvervala { get; set; }
        public DateTime krajIntervala { get; set; }

        public IzvestajPageViewModel() {
            pocetakInvervala = DateTime.Now;
            krajIntervala = DateTime.Now;
            Klik = new RelayCommand(Executed_Klik, CanExecute_Klik);
        }

        public RelayCommand Klik { get; set; }

        public void Executed_Klik(object obj)
        {
            new PrintDialog().PrintVisual(new Izvestaj(pocetakInvervala, krajIntervala), "Izveštaj o zauzetosti prostorija");
        }

        public bool CanExecute_Klik(object obj)
        {
            return true;
        }


    }
}
