using Model;
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
    /// Interaction logic for DetaljiPregleda.xaml
    /// </summary>
    public partial class DetaljiPregleda : Page
    {
        private Anamnesis anamneza;
        public DetaljiPregleda(Anamnesis anamneza)
        {
            InitializeComponent();

            this.anamneza = anamneza;
            this.DataContext = this;

            ucitajPodatke();
            if (anamneza.GlavneTegobe != null)
            {
                GlavneTegobe.Inlines.Add(new Run("Glavne tegobe:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
                GlavneTegobe.Inlines.Add("   ");
                GlavneTegobe.Inlines.Add(anamneza.GlavneTegobe);
                GlavneTegobe.TextWrapping = TextWrapping.Wrap;
                SadasnjaAnamneza.Inlines.Add(new Run("Sadašnja anamneza:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
                SadasnjaAnamneza.Inlines.Add("   ");
                SadasnjaAnamneza.Inlines.Add(anamneza.SadasnjaAnamneza);
                SadasnjaAnamneza.TextWrapping = TextWrapping.Wrap;

                OpstePojave.Inlines.Add(new Run("Opšte pojave:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
                OpstePojave.Inlines.Add("   ");
                OpstePojave.Inlines.Add(anamneza.OpstePojave);
                OpstePojave.TextWrapping = TextWrapping.Wrap;

                RespiratorniSistem.Inlines.Add(new Run("Respiratorni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
                RespiratorniSistem.Inlines.Add("   ");
                RespiratorniSistem.Inlines.Add(anamneza.RespiratorniSistem);
                RespiratorniSistem.TextWrapping = TextWrapping.Wrap;

                KardiovaskularniSistem.Inlines.Add(new Run("Kardiovaskularni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
                KardiovaskularniSistem.Inlines.Add("   ");
                KardiovaskularniSistem.Inlines.Add(anamneza.KardiovaskularniSistem);
                KardiovaskularniSistem.TextWrapping = TextWrapping.Wrap;

                DigestivniSistem.Inlines.Add(new Run("Digestivni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
                DigestivniSistem.Inlines.Add("   ");
                DigestivniSistem.Inlines.Add(anamneza.DigestivniSistem);
                DigestivniSistem.TextWrapping = TextWrapping.Wrap;

                UrogenitalniSistem.Inlines.Add(new Run("Urogenitalni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
                UrogenitalniSistem.Inlines.Add("   ");
                UrogenitalniSistem.Inlines.Add(anamneza.UrogenitalniSistem);
                UrogenitalniSistem.TextWrapping = TextWrapping.Wrap;

                LokomotorniSistem.Inlines.Add(new Run("Lokomotorni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
                LokomotorniSistem.Inlines.Add("   ");
                LokomotorniSistem.Inlines.Add(anamneza.LokomotorniSistem);
                LokomotorniSistem.TextWrapping = TextWrapping.Wrap;

                NervniSistem.Inlines.Add(new Run("Nervni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
                NervniSistem.Inlines.Add("   ");
                NervniSistem.Inlines.Add(anamneza.NervniSistem);
                NervniSistem.TextWrapping = TextWrapping.Wrap;

                RanijaOboljenja.Inlines.Add(new Run("Ranija oboljenja:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
                RanijaOboljenja.Inlines.Add("   ");
                RanijaOboljenja.Inlines.Add(anamneza.RanijaOboljenja);
                RanijaOboljenja.TextWrapping = TextWrapping.Wrap;

                PorodicniPodaci.Inlines.Add(new Run("Porodični podaci:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
                PorodicniPodaci.Inlines.Add("   ");
                PorodicniPodaci.Inlines.Add(anamneza.PorodicniPodaci);
                PorodicniPodaci.TextWrapping = TextWrapping.Wrap;

                SocijalnoEpidemioloskiPodaci.Inlines.Add(new Run("Socijalno-epidemiološki podaci:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
                SocijalnoEpidemioloskiPodaci.Inlines.Add("   ");
                SocijalnoEpidemioloskiPodaci.Inlines.Add(anamneza.SocioEpiPodaci);
                SocijalnoEpidemioloskiPodaci.TextWrapping = TextWrapping.Wrap;
            }
            else
            {
                GlavneTegobe.Inlines.Add(new Run("Poštovani, anamneza za ovaj pregled nije napisana!") { FontWeight = FontWeights.Bold });
            }

        }

        private void ucitajPodatke()
        {
            anamneza.Termin = new AppointmentRepository().FindById(anamneza.Termin.TerminKey);
            anamneza.Termin.Lekar = new DoctorRepository().FindById(anamneza.Termin.Lekar.Jmbg);
            anamneza.Termin.Pacijent = PocetnaStranica.getInstance().Pacijent;
        }

        public Anamnesis Anamneza { get => anamneza; set => anamneza = value; }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            PocetnaStranica.getInstance().Tabovi.Content = new IstorijaPregleda();
        }
    }
 }

