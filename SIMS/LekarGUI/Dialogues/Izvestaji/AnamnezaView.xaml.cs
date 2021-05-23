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

namespace SIMS.LekarGUI.Dialogues.Izvestaji
{
    /// <summary>
    /// Interaction logic for AnamnezaView.xaml
    /// </summary>
    public partial class AnamnezaView : Window
    {
        public AnamnezaView(Anamnesis a)
        {
            InitializeComponent();

            a.InitData();

            LabelDoktor.Content = "Doktor: " + a.DoctorName;
            LabelDatum.Content = "Datum pregleda: " + a.Date;

            LabelPacijent.Content = "Pacijent: " + a.PatientName;
            Appointment t = AppointmentFileRepository.Instance.FindById(a.AnamnesisID);
            LabelDatumRodjenja.Content = "Datum rođenja: " + PatientFileRepository.Instance.FindById(t.Pacijent.Jmbg).Datum_Rodjenja.ToString("dd.MM.yyyy.");

            GlavneTegobe.Inlines.Add(new Run("Glavne tegobe:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            GlavneTegobe.Inlines.Add("   ");
            GlavneTegobe.Inlines.Add(a.MainIssues);

            SadasnjaAnamneza.Inlines.Add(new Run("Sadašnja anamneza:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            SadasnjaAnamneza.Inlines.Add("   ");
            SadasnjaAnamneza.Inlines.Add(a.CurrentAnamnesis);

            OpstePojave.Inlines.Add(new Run("Opšte pojave:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            OpstePojave.Inlines.Add("   ");
            OpstePojave.Inlines.Add(a.GeneralOccurrences);

            RespiratorniSistem.Inlines.Add(new Run("Respiratorni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            RespiratorniSistem.Inlines.Add("   ");
            RespiratorniSistem.Inlines.Add(a.RespiratorySystem);

            KardiovaskularniSistem.Inlines.Add(new Run("Kardiovaskularni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            KardiovaskularniSistem.Inlines.Add("   ");
            KardiovaskularniSistem.Inlines.Add(a.CardioSystem);

            DigestivniSistem.Inlines.Add(new Run("Digestivni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            DigestivniSistem.Inlines.Add("   ");
            DigestivniSistem.Inlines.Add(a.DigestiveSystem);

            UrogenitalniSistem.Inlines.Add(new Run("Urogenitalni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            UrogenitalniSistem.Inlines.Add("   ");
            UrogenitalniSistem.Inlines.Add(a.UroGenitalSystem);

            LokomotorniSistem.Inlines.Add(new Run("Lokomotorni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            LokomotorniSistem.Inlines.Add("   ");
            LokomotorniSistem.Inlines.Add(a.LocomotorSystem);

            NervniSistem.Inlines.Add(new Run("Nervni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            NervniSistem.Inlines.Add("   ");
            NervniSistem.Inlines.Add(a.NervousSystem);

            RanijaOboljenja.Inlines.Add(new Run("Ranija oboljenja:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            RanijaOboljenja.Inlines.Add("   ");
            RanijaOboljenja.Inlines.Add(a.PastDiseases);

            PorodicniPodaci.Inlines.Add(new Run("Porodični podaci:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            PorodicniPodaci.Inlines.Add("   ");
            PorodicniPodaci.Inlines.Add(a.FamilyData);

            SocijalnoEpidemioloskiPodaci.Inlines.Add(new Run("Socijalno-epidemiološki podaci:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            SocijalnoEpidemioloskiPodaci.Inlines.Add("   ");
            SocijalnoEpidemioloskiPodaci.Inlines.Add(a.SocioEpiData);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
