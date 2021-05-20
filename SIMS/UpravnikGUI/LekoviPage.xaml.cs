using Model;
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

namespace SIMS.UpravnikGUI
{
    public partial class LekoviPage : Page
    {
        private ObservableCollection<Medication> lekovi;

        public LekoviPage()
        {
            InitializeComponent();
            lekovi = new ObservableCollection<Medication>(MedicationRepository.Instance.ReadEntities());
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
            MedicationRepository.Instance.DeleteEntity(SelectedLek.MedicineID);
            lekovi = new ObservableCollection<Medication>(MedicationRepository.Instance.ReadEntities());
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
            tabelaLekovi.ItemsSource = LekoviFilter.Instance.ApplyFilters(lekovi, SearchBox.Text, false);
        }
    }
}
