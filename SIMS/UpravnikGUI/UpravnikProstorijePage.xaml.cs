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
    public partial class UpravnikProstorijePage : Page
    {
        private ObservableCollection<Room> prostorije;

        public UpravnikProstorijePage()
        {
            InitializeComponent();
            prostorije = new ObservableCollection<Room>(RoomRepository.Instance.ReadEntities());
            tabelaProstorije.ItemsSource = prostorije;
        }

        private void DodajProstorija_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(new UpravnikProstorijaDetailPage());
            UpravnikWindow.Instance.SetLabel("Nova prostorija");
        }

        private void IzbrisiProstorija_Click(object sender, RoutedEventArgs e)
        {
            Room SelectedProstorija = tabelaProstorije.SelectedItem as Room;
            RoomRepository.Instance.DeleteEntity(SelectedProstorija.Number);
            prostorije = new ObservableCollection<Room>(RoomRepository.Instance.ReadEntities());
            tabelaProstorije.ItemsSource = prostorije;
        }

        private void PregledajUredi_Click(object sender, RoutedEventArgs e)
        {
            Room SelectedProstorija = tabelaProstorije.SelectedItem as Room;
            if (SelectedProstorija == null)
            {
                MessageBox.Show("Izabrati prostoriju.");
                return;
            }
            UpravnikWindow.Instance.SetContent(new UpravnikProstorijaDetailPage(SelectedProstorija.Number));
            UpravnikWindow.Instance.SetLabel("Prostorija " + SelectedProstorija.Number);
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            tabelaProstorije.ItemsSource = ProstorijeFilter.Instance.ApplyFilters(prostorije, SearchBox.Text, false);
        }

        private void ZakaziRenoviranje_Click(object sender, RoutedEventArgs e)
        {
            Room SelectedProstorija = tabelaProstorije.SelectedItem as Room;
            if (SelectedProstorija == null)
            {
                MessageBox.Show("Izabrati prostoriju.");
                return;
            }
            UpravnikWindow.Instance.SetContent(new RenoviranjePage(SelectedProstorija.Number));
            UpravnikWindow.Instance.SetLabel("Renoviranje prostorije " + SelectedProstorija.Number);
        }
    }
}
