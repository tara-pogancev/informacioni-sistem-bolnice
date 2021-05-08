using Model;
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
        private ObservableCollection<Prostorija> prostorije;

        public UpravnikProstorijePage()
        {
            InitializeComponent();
            prostorije = new ObservableCollection<Prostorija>(ProstorijaStorage.Instance.ReadList());
            tabelaProstorije.ItemsSource = prostorije;
        }

        private void DodajProstorija_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(new UpravnikProstorijaDetailPage());
            UpravnikWindow.Instance.SetLabel("Nova prostorija");
        }

        private void IzbrisiProstorija_Click(object sender, RoutedEventArgs e)
        {
            Prostorija SelectedProstorija = tabelaProstorije.SelectedItem as Prostorija;
            ProstorijaStorage.Instance.Delete(SelectedProstorija.Broj);
            foreach(Prostorija prostorija in prostorije)
            {
                if (prostorija.Broj == SelectedProstorija.Broj)
                {
                    prostorije.Remove(prostorija);
                    return;
                }
            }
        }

        private void PregledajUredi_Click(object sender, RoutedEventArgs e)
        {
            Prostorija SelectedProstorija = tabelaProstorije.SelectedItem as Prostorija;
            if (SelectedProstorija == null)
            {
                return;
            }
            UpravnikWindow.Instance.SetContent(new UpravnikProstorijaDetailPage(SelectedProstorija.Broj));
            UpravnikWindow.Instance.SetLabel("Prostorija " + SelectedProstorija.Broj);
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            ObservableCollection<Prostorija> filtered = new ObservableCollection<Prostorija>();
            var keywords = SearchBox.Text.Split(" ");

            foreach (Prostorija prostorija in prostorije)
            {
                foreach (string keyword in keywords)
                {
                    if (!prostorija.Broj.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) &&
                        !prostorija.DostupnaToString.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) &&
                        !prostorija.TipProstorijeToString.Contains(keyword, StringComparison.InvariantCultureIgnoreCase))
                    {
                        goto NEXT_PROSTORIJA;
                    }
                }
                filtered.Add(prostorija);
            NEXT_PROSTORIJA:;
            }


            tabelaProstorije.ItemsSource = filtered;
        }

        private void ZakaziRenoviranje_Click(object sender, RoutedEventArgs e)
        {
            Prostorija SelectedProstorija = tabelaProstorije.SelectedItem as Prostorija;
            if (SelectedProstorija == null)
            {
                return;
            }
            UpravnikWindow.Instance.SetContent(new RenoviranjePage(SelectedProstorija.Broj));
            UpravnikWindow.Instance.SetLabel("Renoviranje prostorije " + SelectedProstorija.Broj);
        }
    }
}
