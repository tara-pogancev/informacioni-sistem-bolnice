using SIMS.Repositories.SecretaryRepo;
using SIMS.Filters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIMS.Model;
using SIMS.Controller;

namespace SIMS.UpravnikGUI
{
    public partial class LekoviPage : Page
    {
        private ObservableCollection<Medication> lekovi;
        private MedicineController medicineController = new MedicineController();
        public LekoviPage()
        {
            InitializeComponent();
            lekovi = new ObservableCollection<Medication>(medicineController.GetAllMedicine());
            tabelaLekovi.ItemsSource = lekovi;
        }

        private void DodajLek_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(new LekoviDetailPage());
            UpravnikWindow.Instance.SetLabel("Nov Lek");
        }

        private void IzbrisiLek_Click(object sender, RoutedEventArgs e)
        {
            Medication SelectedLek = tabelaLekovi.SelectedItem as Medication;
            medicineController.DeleteMedicine(SelectedLek.MedicineID);
            lekovi = new ObservableCollection<Medication>(medicineController.GetAllMedicine());
            tabelaLekovi.ItemsSource = lekovi;
        }

        private void PregledajUredi_Click(object sender, RoutedEventArgs e)
        {
            Medication SelectedLek = tabelaLekovi.SelectedItem as Medication;
            if (SelectedLek == null)
            {
                return;
            }
            UpravnikWindow.Instance.SetContent(new LekoviDetailPage(SelectedLek.MedicineID));
            UpravnikWindow.Instance.SetLabel("Lek " + SelectedLek.MedicineID);
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            LekoviFilter filter = new LekoviFilter();
            filter.SetKeywordsFromInput(SearchBox.Text);
            tabelaLekovi.ItemsSource = filter.ApplyFilters(lekovi);
        }
    }
}
