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
using System.Windows.Shapes;
using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Model;
using SIMS.DTO;
using SIMS.Controller;

namespace SIMS.LekarGUI.Dialogues.Izvestaji
{
    /// <summary>
    /// Interaction logic for AnamnezaView.xaml
    /// </summary>
    public partial class AnamnesisRead : Window
    {
        private Anamnesis anamnesis;
        private PatientController patientController = new PatientController();

        public AnamnesisRead(Anamnesis anamnesisPar)
        {
            InitializeComponent();
            anamnesis = anamnesisPar;
            Appointment appointment = anamnesis.GetAppointment();
            appointment.InitData();

            Patient patient = patientController.GetPatient(appointment.Patient.Jmbg);

            LabelDoctor.Content = "Doktor: " + appointment.GetDoctorName();
            LabelDate.Content = "Datum pregleda: " + appointment.GetAppointmentDate();

            LabelPatient.Content = "Pacijent: " + patient.FullName;
            LabelPatientDateOfBirth.Content = "Datum rođenja: " + patient.GetDateOfBirthString();

            GenerateText(anamnesis);
        }

        private void GenerateText(Anamnesis anamnesis)
        {
            GlavneTegobe.Inlines.Add(new Run("Glavne tegobe:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            GlavneTegobe.Inlines.Add("   ");
            GlavneTegobe.Inlines.Add(anamnesis.MainIssues);

            SadasnjaAnamneza.Inlines.Add(new Run("Sadašnja anamneza:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            SadasnjaAnamneza.Inlines.Add("   ");
            SadasnjaAnamneza.Inlines.Add(anamnesis.CurrentAnamnesis);

            OpstePojave.Inlines.Add(new Run("Opšte pojave:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            OpstePojave.Inlines.Add("   ");
            OpstePojave.Inlines.Add(anamnesis.GeneralOccurrences);

            RespiratorniSistem.Inlines.Add(new Run("Respiratorni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            RespiratorniSistem.Inlines.Add("   ");
            RespiratorniSistem.Inlines.Add(anamnesis.RespiratorySystem);

            KardiovaskularniSistem.Inlines.Add(new Run("Kardiovaskularni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            KardiovaskularniSistem.Inlines.Add("   ");
            KardiovaskularniSistem.Inlines.Add(anamnesis.CardioSystem);

            DigestivniSistem.Inlines.Add(new Run("Digestivni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            DigestivniSistem.Inlines.Add("   ");
            DigestivniSistem.Inlines.Add(anamnesis.DigestiveSystem);

            UrogenitalniSistem.Inlines.Add(new Run("Urogenitalni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            UrogenitalniSistem.Inlines.Add("   ");
            UrogenitalniSistem.Inlines.Add(anamnesis.UroGenitalSystem);

            LokomotorniSistem.Inlines.Add(new Run("Lokomotorni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            LokomotorniSistem.Inlines.Add("   ");
            LokomotorniSistem.Inlines.Add(anamnesis.LocomotorSystem);

            NervniSistem.Inlines.Add(new Run("Nervni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            NervniSistem.Inlines.Add("   ");
            NervniSistem.Inlines.Add(anamnesis.NervousSystem);

            RanijaOboljenja.Inlines.Add(new Run("Ranija oboljenja:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            RanijaOboljenja.Inlines.Add("   ");
            RanijaOboljenja.Inlines.Add(anamnesis.PastDiseases);

            PorodicniPodaci.Inlines.Add(new Run("Porodični podaci:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            PorodicniPodaci.Inlines.Add("   ");
            PorodicniPodaci.Inlines.Add(anamnesis.FamilyData);

            SocijalnoEpidemioloskiPodaci.Inlines.Add(new Run("Socijalno-epidemiološki podaci:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            SocijalnoEpidemioloskiPodaci.Inlines.Add("   ");
            SocijalnoEpidemioloskiPodaci.Inlines.Add(anamnesis.SocioEpiData);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WindowKeyListener(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
