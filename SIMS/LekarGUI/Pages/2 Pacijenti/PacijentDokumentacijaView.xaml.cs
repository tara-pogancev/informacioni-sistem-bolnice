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
using SIMS.LekarGUI.Dialogues.Izvestaji;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for PacijentKartonView.xaml
    /// </summary>
    public partial class PacijentDokumentacijaView : Page
    {
        private Pacijent pacijentProfile;

        public ObservableCollection<Anamneza> PregledView { get; set; }
        public ObservableCollection<Recept> ReceptView { get; set; }

        public PacijentDokumentacijaView(Pacijent p)
        {
            InitializeComponent();

            pacijentProfile = p;

            this.DataContext = this;
            initializeData();

            Ime_Top.Content = pacijentProfile.ImePrezime;
            Label_Ime.Content = pacijentProfile.ImePrezime;

        }

        private void initializeData()
        {
            PregledView = new ObservableCollection<Anamneza>(AnamnezaStorage.Instance.ReadByPatient(pacijentProfile));
            ReceptView = new ObservableCollection<Recept>(ReceptStorage.Instance.ReadByPatient(pacijentProfile));

            foreach (Anamneza anamneza in PregledView)
            {
                anamneza.InitData();
            }

            foreach (Recept recept in ReceptView)
            {
                recept.InitData();
            }

        }

        private void Button_Pacijenti(object sender, MouseButtonEventArgs e)
        {
            LekarUI.GetInstance().ChangeTab(2);
        }

        private void Button_Home(object sender, MouseButtonEventArgs e)
        {
            LekarUI.GetInstance().ChangeTab(0);
        }

        private void Button_PacijentKarton(object sender, MouseButtonEventArgs e)
        {
            LekarUI.GetInstance().SellectedTab.Content = PacijentKartonView.GetInstance(pacijentProfile);
        }

        private void Button_Pročitaj(object sender, RoutedEventArgs e)
        {
            if (TabbedPanel.SelectedIndex == 0)
            {
                ReadPregled();
            }
            else if (TabbedPanel.SelectedIndex == 1)
            {
                //TODO
            }
            else if (TabbedPanel.SelectedIndex == 2)
            {
                ReadRecept();
            }
            else if (TabbedPanel.SelectedIndex == 3)
            {
                //TODO
            }
        }

        private void Button_GenerisanjeIzvestaja(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void Button_ViewRecept(object sender, MouseButtonEventArgs e)
        {
            ReadRecept();
        }

        private void Button_ViewPregled(object sender, MouseButtonEventArgs e)
        {
            ReadPregled();
        }

        private void ReadRecept()
        {
            if (dataGridRecepti.SelectedItem != null)
            {
                Recept sellectedRecept = (Recept)dataGridRecepti.SelectedItem;
                PrikazRecepta window = new PrikazRecepta(sellectedRecept);
                window.Show();
            }
        }

        private void ReadPregled()
        {
            if (dataGridPregledi.SelectedItem != null)
            {
                Anamneza sellectedAnamneza = (Anamneza)dataGridPregledi.SelectedItem;
                AnamnezaView window = new AnamnezaView(sellectedAnamneza);
                window.Show();
            }
        }
    }
}
