using SIMS.Repositories.SecretaryRepo;
using SIMS.Model;
using SIMS.Repositories.DoctorSurveyRepo;
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
using SIMS.Controller;

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
            anketaLekara.Appointment = termin;
            lekar = termin.Doctor.FullName;
            this.DataContext = this;
            
        }

        public DoctorSurvey AnketaLekara { get => anketaLekara; set => anketaLekara = value; }
        public string Lekar { get => lekar; set => lekar = value; }

        private void Posalji_Click(object sender, RoutedEventArgs e)
        {
            anketaLekara.Comment = KomentarPregleda.Text;
            anketaLekara.Grade = BasicRatingBar.Value;
            anketaLekara.Appointment.Serialize = false;
            anketaLekara.SurveyID = anketaLekara.Appointment.AppointmentID;
            anketaLekara.IdVlasnika = anketaLekara.Appointment.Patient.Jmbg;
            anketaLekara.DoctorId = anketaLekara.Appointment.Doctor.Jmbg;
            new DoctorSurveyController().SaveDoctorSurvey(anketaLekara);
            NavigationService.Navigate(PocetnaStranica.getInstance().frame.Content=new IstorijaPregleda());
        }
    }
}
