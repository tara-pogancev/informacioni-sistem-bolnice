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
            InitializeComponent();
            foreach (Oprema op in SvaOprema)
            {
                op.BrojProstorije = Prostorija.Broj;
            }
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
            tabelaInventara.ItemsSource = Filter();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            tabelaInventara.ItemsSource = Filter();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            tabelaInventara.ItemsSource = Filter();
        }

        private bool FilterTextBox(Oprema oprema, string keyword)
        {
            return (oprema.Id.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    oprema.Naziv.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    oprema.Kolicina.ToString().Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                    oprema.TipToString.Contains(keyword, StringComparison.InvariantCultureIgnoreCase));
        }

        private bool FilterCheckBox(Oprema oprema)
        {
            return (bool)!CheckBox.IsChecked || oprema.Kolicina != 0;
        }

        private ObservableCollection<Oprema> Filter()
        {
            ObservableCollection<Oprema> filtered = new ObservableCollection<Oprema>();
            var keywords = SearchBox.Text.Split(" ");

            foreach (Oprema oprema in SvaOprema)
            {
                foreach (string keyword in keywords)
                {
                    if (!FilterTextBox(oprema, keyword) || !FilterCheckBox(oprema))
                    {
                        goto NEXT;
                    }
                }
                filtered.Add(oprema);
            NEXT:;
            }
            return filtered;
        }
    }
}
