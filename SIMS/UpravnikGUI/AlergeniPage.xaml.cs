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
using Model;
using SIMS.Filters;

namespace SIMS.UpravnikGUI
{
    public partial class AlergeniPage : Page
    {
        private ObservableCollection<Alergen> alergeni;

        public AlergeniPage()
        {
            InitializeComponent();
            alergeni = new ObservableCollection<Alergen>(AlergeniStorage.Instance.ReadList());
            tabelaAlergeni.ItemsSource = alergeni;
        }

        private void DodajAlergen_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(new AlergeniDetailPage());
            UpravnikWindow.Instance.SetLabel("Nov alergen");
        }

        private void IzbrisiAlergen_Click(object sender, RoutedEventArgs e)
        {
            Alergen SelectedAlergen = tabelaAlergeni.SelectedItem as Alergen;
            AlergeniStorage.Instance.Delete(SelectedAlergen.ID);
            foreach (Alergen alergen in alergeni)
            {
                if (alergen.ID == SelectedAlergen.ID)
                {
                    alergeni.Remove(alergen);
                    return;
                }
            }
        }

        private void PregledajUredi_Click(object sender, RoutedEventArgs e)
        {
            Alergen SelectedAlergen = tabelaAlergeni.SelectedItem as Alergen;
            if (SelectedAlergen == null)
            {
                return;
            }
            UpravnikWindow.Instance.SetContent(new AlergeniDetailPage(SelectedAlergen.ID));
            UpravnikWindow.Instance.SetLabel("Alergen " + SelectedAlergen.ID);
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            tabelaAlergeni.ItemsSource = AlergeniFilter.Instance.ApplyFilters(alergeni, SearchBox.Text, false);
        }
    }
}
