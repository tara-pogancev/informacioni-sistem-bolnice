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
using SIMS.Repositories.SecretaryRepo;
using SIMS.LekarGUI.Dialogues.Izvestaji;
using SIMS.Model;
using SIMS.Repositories.AnamnesisRepository;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for PacijentKartonView.xaml
    /// </summary>
    public partial class PacijentDokumentacijaView : Page
    {
        private Patient pacijentProfile;

        public ObservableCollection<Anamnesis> PregledView { get; set; }
        public ObservableCollection<SurgeryReport> OperacijaIzvestajView { get; set; }
        public ObservableCollection<Receipt> ReceptView { get; set; }


        public PacijentDokumentacijaView(Patient p)
        {
            InitializeComponent();

            pacijentProfile = p;

            this.DataContext = this;
            initializeData();

            Ime_Top.Content = pacijentProfile.FullName;
            Label_Ime.Content = pacijentProfile.FullName;

        }

        private void initializeData()
        {
            PregledView = new ObservableCollection<Anamnesis>(AnamnesisFileRepository.Instance.ReadByPatient(pacijentProfile));
            ReceptView = new ObservableCollection<Receipt>(ReceiptFileRepository.Instance.ReadByPatient(pacijentProfile));
            OperacijaIzvestajView = new ObservableCollection<SurgeryReport>(SurgeryReportFileRepository.Instance.ReadByPatient(pacijentProfile));

            foreach (Anamnesis anamneza in PregledView)
            {
                anamneza.InitData();
            }

            foreach (Receipt recept in ReceptView)
            {
                recept.InitData();
            }

            foreach (SurgeryReport operacijaIzvestaj in OperacijaIzvestajView)
            {
                operacijaIzvestaj.InitData();
            }

        }

        private void Button_Pacijenti(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(2);
        }

        private void Button_Home(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(0);
        }

        private void Button_PacijentKarton(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().SellectedTab.Content = PacijentKartonView.GetInstance(pacijentProfile);
        }

        private void Button_Pročitaj(object sender, RoutedEventArgs e)
        {
            if (TabbedPanel.SelectedIndex == 0)
            {
                ReadPregled();
            }
            else if (TabbedPanel.SelectedIndex == 1)
            {
                ReadOperacija();
            }
            else if (TabbedPanel.SelectedIndex == 2)
            {
                ReadRecept();
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
                Receipt sellectedRecept = (Receipt)dataGridRecepti.SelectedItem;
                PrikazRecepta window = new PrikazRecepta(sellectedRecept);
                window.Show();
            }
        }

        private void ReadPregled()
        {
            if (dataGridPregledi.SelectedItem != null)
            {
                Anamnesis sellectedAnamneza = (Anamnesis)dataGridPregledi.SelectedItem;
                AnamnesisRead window = new AnamnesisRead(sellectedAnamneza);
                window.Show();
            }
        }

        private void ReadOperacija()
        {
            if (dataGridOperacije.SelectedItem != null)
            {
                SurgeryReport sellectedIzvestaj = (SurgeryReport)dataGridOperacije.SelectedItem;
                SurgeryReportRead window = new SurgeryReportRead(sellectedIzvestaj);
                window.Show();
            }
        }

        private void Button_ViewOperacija(object sender, MouseButtonEventArgs e)
        {
            ReadOperacija();
        }
    }
}
