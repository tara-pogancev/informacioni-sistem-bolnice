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
    public partial class UpravnikInventarProstorijePage : Page
    {
        Prostorija Prostorija;
        UpravnikProstorijaDetailPage ParentPage;
        ObservableCollection<Oprema> SvaOprema;

        public UpravnikInventarProstorijePage(Prostorija prostorija, UpravnikProstorijaDetailPage parent)
        {
            Prostorija = prostorija;
            ParentPage = parent;
            SvaOprema = new ObservableCollection<Oprema>(OpremaStorage.Instance.ReadList());
            foreach (Oprema op in SvaOprema)
            {
                op.BrojProstorije = Prostorija.Broj;
            }
            InitializeComponent();
            tabelaInventara.ItemsSource = SvaOprema;
        }


        private void Premesti_Click(object sender, RoutedEventArgs e)
        {
            Oprema SelectedOprema = tabelaInventara.SelectedItem as Oprema;
            if (SelectedOprema == null)
            {
                return;
            }
            UpravnikWindow.Instance.SetContent(new UpravnikPremestiOpremu(this, Prostorija.Broj, SelectedOprema));
            UpravnikWindow.Instance.SetLabel("Premesti iz prostorije " + Prostorija.Broj + " - " + SelectedOprema.Naziv);
        }

        private void PromeniKolicinu_Click(object sender, RoutedEventArgs e)
        {
            Oprema SelectedOprema = tabelaInventara.SelectedItem as Oprema;
            if (SelectedOprema == null)
            {
                return;
            }
            UpravnikWindow.Instance.SetContent(new UpravnikKolicinaOpreme(this, Prostorija.Broj, SelectedOprema));
            UpravnikWindow.Instance.SetLabel("Prostorija " + Prostorija.Broj + " - " + SelectedOprema.Naziv);
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(ParentPage);
            UpravnikWindow.Instance.SetLabel("Prostorija " + Prostorija.Broj);
        }

        internal void Update()
        {
            SvaOprema = new ObservableCollection<Oprema>(OpremaStorage.Instance.ReadList());
            tabelaInventara.Items.Refresh();
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            foreach (Oprema op in SvaOprema)
            {
                op.BrojProstorije = Prostorija.Broj;
            }
            tabelaInventara.ItemsSource = InventarProstorijeFilter.Instance.ApplyFilters(SvaOprema, SearchBox.Text, (bool)CheckBox.IsChecked);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (Oprema op in SvaOprema)
            {
                op.BrojProstorije = Prostorija.Broj;
            }
            tabelaInventara.ItemsSource = InventarProstorijeFilter.Instance.ApplyFilters(SvaOprema, SearchBox.Text, (bool)CheckBox.IsChecked);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (Oprema op in SvaOprema)
            {
                op.BrojProstorije = Prostorija.Broj;
            }
            tabelaInventara.ItemsSource = InventarProstorijeFilter.Instance.ApplyFilters(SvaOprema, SearchBox.Text, (bool)CheckBox.IsChecked);
        }
    }
}
