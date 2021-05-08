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
        private ObservableCollection<Lek> lekovi;

        public LekoviPage()
        {
            InitializeComponent();
            lekovi = new ObservableCollection<Lek>(LekStorage.Instance.ReadList());
            tabelaLekovi.ItemsSource = lekovi;
        }

        private void DodajLek_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(new LekoviDetailPage());
            UpravnikWindow.Instance.SetLabel("Nov Lek");
        }

        private void IzbrisiLek_Click(object sender, RoutedEventArgs e)
        {
            Lek SelectedLek = tabelaLekovi.SelectedItem as Lek;
            LekStorage.Instance.Delete(SelectedLek.MedicineID);
            foreach (Lek Lek in lekovi)
            {
                if (Lek.MedicineID == SelectedLek.MedicineID)
                {
                    lekovi.Remove(Lek);
                    return;
                }
            }
        }

        private void PregledajUredi_Click(object sender, RoutedEventArgs e)
        {
            Lek SelectedLek = tabelaLekovi.SelectedItem as Lek;
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
