using SIMS.Commands;
using SIMS.Controller;
using SIMS.Model;
using SIMS.UpravnikGUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.ViewManager.ViewModel
{
    class AlergeniDetailPageViewModel
    {
        public Component allergen { get; set; }
        public bool idEnabled { get; set; }

        AllergenController allergenController = new AllergenController();

        public RelayCommand Ok { get; set; }
        public RelayCommand Odustani { get; set; }

        public AlergeniDetailPageViewModel(string ID) //izmena postojecg alergena
        {
            allergen = allergenController.GetAllergen(ID);
            idEnabled = false;
            Ok = new RelayCommand(Executed_Ok, CanExecute_Ok);
            Odustani = new RelayCommand(Executed_Odustani, CanExecute_Odustani);
        }

        public AlergeniDetailPageViewModel() //nov alergen
        {
            allergen = new Component();
            idEnabled = true;
            Ok = new RelayCommand(Executed_Ok, CanExecute_Ok);
            Odustani = new RelayCommand(Executed_Odustani, CanExecute_Odustani);
        }


        public void Executed_Ok(object obj)
        {
            allergenController.CreateOrUpdate(allergen);
            UpravnikWindow.Instance.SetContent(new AlergeniPage());
            UpravnikWindow.Instance.SetLabel("Alergeni");
        }

        public bool CanExecute_Ok(object obj)
        {
            return true;
        }

        public void Executed_Odustani(object obj)
        {
            UpravnikWindow.Instance.SetContent(new AlergeniPage());
            UpravnikWindow.Instance.SetLabel("Alergeni");
        }

        public bool CanExecute_Odustani(object obj)
        {
            return true;
        }

    }
}
