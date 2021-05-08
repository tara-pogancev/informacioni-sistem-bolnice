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
    /// Interaction logic for SekretarTerminiPage.xaml
    /// </summary>
    public partial class SekretarTerminiPage : Page
    {
        public static SekretarTerminiPage instance = null;

        private ObservableCollection<Termin> terminiView;
        public ObservableCollection<Termin> TerminiView { get => terminiView; set => terminiView = value; }

        public static SekretarTerminiPage GetInstance()
        {
            if (instance == null)
                instance = new SekretarTerminiPage();
            return instance;
        }

        public SekretarTerminiPage()
        {
            InitializeComponent();

            this.DataContext = this;
            terminiView = new ObservableCollection<Termin>(TerminStorage.Instance.ReadList());
            dobaviPodatkeOPacijentuILekaru(terminiView);
            tabelaTermina.ItemsSource = terminiView;
            refreshView();
        }

        private void refreshView()
        {
            terminiView.Clear();
            ObservableCollection<Termin> temp = new ObservableCollection<Termin>(TerminStorage.Instance.ReadList());
            dobaviPodatkeOPacijentuILekaru(temp);
            foreach (Termin t in temp)
            {
                terminiView.Add(t);
            }
        }

        private void Button_Pregled(object sender, RoutedEventArgs e)
        {
            //Button: Zakazi pregled
            /*TerminCreate terminCreate = new TerminCreate();
            terminCreate.Show();*/
            this.NavigationService.Navigate(new DodajPregledPage());
        }

        private void Button_Operacija(object sender, RoutedEventArgs e)
        {
            //Button: Zakazi operaciju
            /*OperacijaCreate operacijaCreate = new OperacijaCreate();
            operacijaCreate.Show();*/
            this.NavigationService.Navigate(new DodajOperacijuPage());
        }

        private void Button_Izmeni(object sender, RoutedEventArgs e)
        {
            //Button: Uredi termin
            if (tabelaTermina.SelectedItem != null)
            {
                /*TerminUpdate terminUpdate = new TerminUpdate((Termin)tabelaTermina.SelectedItem);
                terminUpdate.Show();*/
                this.NavigationService.Navigate(new IzmeniTerminPage((Termin)tabelaTermina.SelectedItem));
            }

        }

        private void Button_Obrisi(object sender, RoutedEventArgs e)
        {
            //Button: Otkaži pregled

            if (tabelaTermina.SelectedItem != null)
            {

                if (MessageBox.Show("Da li ste sigurni da želite da otkažete termin?",
                "Otkaži termin", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Termin toDelete = (Termin)tabelaTermina.SelectedItem;
                    TerminStorage.Instance.Delete(toDelete.TerminKey);
                    MessageBox.Show("Termin je uspešno otkazan!");
                    refreshView();
                }

            }
        }

        public void dodajTermin(Termin termin)
        {
            TerminStorage.Instance.Create(termin);
            refreshView();
            MessageBox.Show("Termin uspešno zakazan.");
        }

        public void refresh()
        {
            refreshView();
        }

        public void RemoveInstance()
        {
            instance = null;
        }

        private void dobaviPodatkeOPacijentuILekaru(ObservableCollection<Termin> termini)
        {
            foreach (Termin termin in termini)
            {
                termin.Pacijent = PacijentStorage.Instance.Read(termin.Pacijent.Jmbg);
                termin.Lekar = LekarStorage.Instance.Read(termin.Lekar.Jmbg);
            }
        }
    }
}
