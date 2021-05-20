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
using Model;

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

            LabelDoktor.Content = "Doktor: " + a.ImeLekara;
            LabelDatum.Content = "Datum pregleda: " + a.Date;

            LabelPacijent.Content = "Pacijent: " + a.ImePacijenta;
            Appointment t = AppointmentRepository.Instance.ReadEntity(a.IdAnamneze);
            LabelDatumRodjenja.Content = "Datum rođenja: " + PatientRepository.Instance.ReadEntity(t.Pacijent.Jmbg).Datum_Rodjenja.ToString("dd.MM.yyyy.");

            GlavneTegobe.Inlines.Add(new Run("Glavne tegobe:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            GlavneTegobe.Inlines.Add("   ");
            GlavneTegobe.Inlines.Add(a.GlavneTegobe);

            SadasnjaAnamneza.Inlines.Add(new Run("Sadašnja anamneza:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            SadasnjaAnamneza.Inlines.Add("   ");
            SadasnjaAnamneza.Inlines.Add(a.SadasnjaAnamneza);

            OpstePojave.Inlines.Add(new Run("Opšte pojave:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            OpstePojave.Inlines.Add("   ");
            OpstePojave.Inlines.Add(a.OpstePojave);

            RespiratorniSistem.Inlines.Add(new Run("Respiratorni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            RespiratorniSistem.Inlines.Add("   ");
            RespiratorniSistem.Inlines.Add(a.RespiratorniSistem);

            KardiovaskularniSistem.Inlines.Add(new Run("Kardiovaskularni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            KardiovaskularniSistem.Inlines.Add("   ");
            KardiovaskularniSistem.Inlines.Add(a.KardiovaskularniSistem);

            DigestivniSistem.Inlines.Add(new Run("Digestivni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            DigestivniSistem.Inlines.Add("   ");
            DigestivniSistem.Inlines.Add(a.DigestivniSistem);

            UrogenitalniSistem.Inlines.Add(new Run("Urogenitalni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            UrogenitalniSistem.Inlines.Add("   ");
            UrogenitalniSistem.Inlines.Add(a.UrogenitalniSistem);

            LokomotorniSistem.Inlines.Add(new Run("Lokomotorni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            LokomotorniSistem.Inlines.Add("   ");
            LokomotorniSistem.Inlines.Add(a.LokomotorniSistem);

            NervniSistem.Inlines.Add(new Run("Nervni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            NervniSistem.Inlines.Add("   ");
            NervniSistem.Inlines.Add(a.NervniSistem);

            RanijaOboljenja.Inlines.Add(new Run("Ranija oboljenja:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            RanijaOboljenja.Inlines.Add("   ");
            RanijaOboljenja.Inlines.Add(a.RanijaOboljenja);

            PorodicniPodaci.Inlines.Add(new Run("Porodični podaci:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            PorodicniPodaci.Inlines.Add("   ");
            PorodicniPodaci.Inlines.Add(a.PorodicniPodaci);

            SocijalnoEpidemioloskiPodaci.Inlines.Add(new Run("Socijalno-epidemiološki podaci:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            SocijalnoEpidemioloskiPodaci.Inlines.Add("   ");
            SocijalnoEpidemioloskiPodaci.Inlines.Add(a.SocioEpiPodaci);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
