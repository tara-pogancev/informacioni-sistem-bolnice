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

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for UpravnikOpremaPage.xaml
    /// </summary>
    public partial class UpravnikOpremaPage : Page
    {
        private ObservableCollection<Oprema> opreme;
        public UpravnikOpremaPage()
        {
            InitializeComponent();
            opreme = new ObservableCollection<Oprema>(OpremaStorage.Instance.ReadList());
            tabelaOpreme.ItemsSource = opreme;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(new UpravnikOpremaDetailPage());
            UpravnikWindow.Instance.SetLabel("Nova oprema");
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            Oprema SelectedOprema = tabelaOpreme.SelectedItem as Oprema;
            OpremaStorage.Instance.Delete(SelectedOprema.Id);
            foreach (Oprema oprema in opreme)
            {
                if (oprema.Id == SelectedOprema.Id)
                {
                    opreme.Remove(oprema);
                    return;
                }
            }
        }

        private void PregledajUredi_Click(object sender, RoutedEventArgs e)
        {
            Oprema SelectedOprema = tabelaOpreme.SelectedItem as Oprema;
            if (SelectedOprema == null)
            {
                return;
            }
            UpravnikWindow.Instance.SetContent(new UpravnikOpremaDetailPage(SelectedOprema.Id));
            UpravnikWindow.Instance.SetLabel("Oprema " + SelectedOprema.Id);
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            ObservableCollection<Oprema> filtered = new ObservableCollection<Oprema>();

            var keywords = SearchBox.Text.Split(" ");

            foreach (Oprema oprema in opreme)
            {
                foreach (string keyword in keywords)
                {
                    if (!oprema.Id.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) &&
                    !oprema.Naziv.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) &&
                    !oprema.TipToString.Contains(keyword, StringComparison.InvariantCultureIgnoreCase))
                    {
                        goto NEXT_OPREMA;
                    }
                }
                filtered.Add(oprema);

            NEXT_OPREMA:;
            }

            tabelaOpreme.ItemsSource = filtered;
        }
    }
}
