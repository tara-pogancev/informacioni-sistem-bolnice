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

namespace SIMS.SekretarGUI
{
    /// <summary>
    /// Interaction logic for SekretarPacijentiPage.xaml
    /// </summary>
    public partial class SekretarPacijentiPage : Page
    {
        private ObservableCollection<Pacijent> pacijenti;
        private static SekretarPacijentiPage instance = null;

        public static SekretarPacijentiPage GetInstance()
        {
            if (instance == null)
                instance = new SekretarPacijentiPage();
            return instance;
        }
        private SekretarPacijentiPage()
        {
            InitializeComponent();

            PacijentStorage storage = new PacijentStorage();
            pacijenti = new ObservableCollection<Pacijent>(storage.ReadList());

            tabelaPacijenata.ItemsSource = pacijenti;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            //Dodaj dodaj = new Dodaj();
            //dodaj.Show();

            this.NavigationService.Navigate(new DodajPacijentaPage());
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (tabelaPacijenata.SelectedItem == null)
            {
                MessageBox.Show("Morate izabrati pacijenta za izmenu.", "Pacijent nije izabran");
            }
            else
            {
                //Izmeni izmeni = new Izmeni((Pacijent)tabelaPacijenata.SelectedItem);
                //izmeni.Show();

                this.NavigationService.Navigate(new IzmeniPacijentaPage((Pacijent)tabelaPacijenata.SelectedItem));
            }
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            if (tabelaPacijenata.SelectedItem == null)
            {
                MessageBox.Show("Morate izabrati pacijenta za brisanje.", "Pacijent nije izabran");
            }
            else
            {
                Pacijent toDelete = (Pacijent)tabelaPacijenata.SelectedItem;
                PacijentStorage.Instance.Delete(toDelete.Jmbg);
                refresh();
            }
        }

        public void refresh()
        {
            pacijenti.Clear();
            List<Pacijent> pacijentiAll = PacijentStorage.Instance.ReadList();
            foreach (Pacijent p in pacijentiAll)
                pacijenti.Add(p);

        }

        public void RemoveInstance()
        {
            instance = null;
        }
    }
}
