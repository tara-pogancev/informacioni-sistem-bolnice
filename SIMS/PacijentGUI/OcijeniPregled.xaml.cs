using Model;
using SIMS.Model;
using System;
using System.Collections.Generic;
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

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for OcijeniPregled.xaml
    /// </summary>
    public partial class OcijeniPregled : Page
    {
        private DoctorSurvey anketaLekara;
        private String lekar;
        public OcijeniPregled(Appointment termin)
        {
            InitializeComponent();

            anketaLekara = new DoctorSurvey();
            anketaLekara.Termin = termin;
            lekar = termin.Lekar.ImePrezime;
            this.DataContext = this;
            
        }

        public DoctorSurvey AnketaLekara { get => anketaLekara; set => anketaLekara = value; }
        public string Lekar { get => lekar; set => lekar = value; }

        private void Posalji_Click(object sender, RoutedEventArgs e)
        {
            anketaLekara.Komentar = KomentarPregleda.Text;
            anketaLekara.Ocjena = BasicRatingBar.Value;
            anketaLekara.Termin.Serijalizuj = false;
            anketaLekara.IdAnkete = anketaLekara.Termin.TerminKey;
            anketaLekara.IdVlasnika = anketaLekara.Termin.Pacijent.Jmbg;
            new DoctorSurveyRepository().CreateEntity(anketaLekara);
            NavigationService.Navigate(PocetnaStranica.getInstance().Tabovi.Content=new IstorijaPregleda());
        }
    }
}
