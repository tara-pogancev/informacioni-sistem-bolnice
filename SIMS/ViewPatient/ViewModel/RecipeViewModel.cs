using SIMS.Controller;
using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SIMS.ViewPatient.ViewModel
{
    public class RecipeViewModel
    {
        public ObservableCollection<Receipt> Receipts { get; set; }
        public Receipt SelectedItem { get; set; }
        public Receipt Receipt{get;set;}

        public RecipeViewModel()
        {
            Receipts = new ObservableCollection<Receipt> (new ReceiptController().ReadByPatient(HomePageViewModel.patient));
           
            loadDoctor();
        }

        private void loadDoctor()
        {
            if (SelectedItem!=null)
                Receipt.Doctor = new DoctorController().GetDoctor(Receipt.Doctor.Jmbg);
        }

        public void GetReceipt()
        {
            Receipt = SelectedItem;
            Receipt.Doctor = new DoctorController().GetDoctor(Receipt.Doctor.Jmbg);
        }
    }
}
