using SIMS.Commands;
using SIMS.Controller;
using SIMS.DTO;
using SIMS.Model;
using SIMS.UpravnikGUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SIMS.ViewManager.ViewModel
{
    class PremestiOpremuPageViewModel
    {
        private InventoryController inventoryController = new InventoryController();
        private UpravnikInventarProstorijePage ParentPage;
        public string BrojProstorije { get; set; }
        public string BrojProstorijePremestaja { get; set; }
        public string Kolicina { get; set; }
        public DateTime? DatumPremestaja { get; set; }
        public Inventory Oprema { get; set; }
        public Visibility DatumPickerVisibility { get; set; }
        public Visibility DatumLabelVisibility { get; set; }
        public RelayCommand Ok { get; set; }
        public RelayCommand Odustani { get; set; }
        public PremestiOpremuPageViewModel(UpravnikInventarProstorijePage ParentPage, string BrojProstorije, Inventory Oprema)
        {
            Ok = new RelayCommand(Executed_Ok, CanExecute_Ok);
            Odustani = new RelayCommand(Executed_Odustani, CanExecute_Odustani);

            this.ParentPage = ParentPage;
            this.BrojProstorije = BrojProstorije;
            this.Oprema = Oprema;

            if (Oprema.Type != InventoryType.statička)
            {
                DatumPickerVisibility = Visibility.Hidden;
                DatumLabelVisibility = Visibility.Hidden;
            }
        }


        public void Executed_Ok(object obj)
        {
            inventoryController.MoveInventory(new MovingInventoryDTO(BrojProstorije, BrojProstorijePremestaja, Kolicina, DatumPremestaja, Oprema.ID));

            ParentPage.Update();

            UpravnikWindow.Instance.SetContent(ParentPage);
            UpravnikWindow.Instance.SetLabel("Prostorija " + BrojProstorije);
        }

        public bool CanExecute_Ok(object obj)
        {
            return true;
        }


        public void Executed_Odustani(object obj)
        {
            UpravnikWindow.Instance.SetContent(ParentPage);
            UpravnikWindow.Instance.SetLabel("Prostorija " + BrojProstorije);
        }

        public bool CanExecute_Odustani(object obj)
        {
            return true;
        }
    }
}
