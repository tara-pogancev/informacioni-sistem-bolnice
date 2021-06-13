using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
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
using SIMS.Model;
using SIMS.Controller;

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
            ucitajPodatke();
            this.DataContext = this;

            
            if (anamneza.MainIssues != null)
            {
                GlavneTegobe.Inlines.Add(new Run("Glavne tegobe:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline, Foreground = Brushes.White });
                GlavneTegobe.Inlines.Add("   ");
                GlavneTegobe.Inlines.Add(anamneza.MainIssues);
                GlavneTegobe.TextWrapping = TextWrapping.Wrap;
                SadasnjaAnamneza.Inlines.Add(new Run("Sadašnja anamneza:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline, Foreground = Brushes.White });
                SadasnjaAnamneza.Inlines.Add("   ");
                SadasnjaAnamneza.Inlines.Add(anamneza.CurrentAnamnesis);
                SadasnjaAnamneza.TextWrapping = TextWrapping.Wrap;

                OpstePojave.Inlines.Add(new Run("Opšte pojave:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline, Foreground = Brushes.White });
                OpstePojave.Inlines.Add("   ");
                OpstePojave.Inlines.Add(anamneza.GeneralOccurrences);
                OpstePojave.TextWrapping = TextWrapping.Wrap;

                RespiratorniSistem.Inlines.Add(new Run("Respiratorni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline, Foreground = Brushes.White });
                RespiratorniSistem.Inlines.Add("   ");
                RespiratorniSistem.Inlines.Add(anamneza.RespiratorySystem);
                RespiratorniSistem.TextWrapping = TextWrapping.Wrap;

                KardiovaskularniSistem.Inlines.Add(new Run("Kardiovaskularni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
                KardiovaskularniSistem.Inlines.Add("   ");
                KardiovaskularniSistem.Inlines.Add(anamneza.CardioSystem);
                KardiovaskularniSistem.TextWrapping = TextWrapping.Wrap;

                DigestivniSistem.Inlines.Add(new Run("Digestivni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline , Foreground = Brushes.White });
                DigestivniSistem.Inlines.Add("   ");
                DigestivniSistem.Inlines.Add(anamneza.DigestiveSystem);
                DigestivniSistem.TextWrapping = TextWrapping.Wrap;

                UrogenitalniSistem.Inlines.Add(new Run("Urogenitalni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
                UrogenitalniSistem.Inlines.Add("   ");
                UrogenitalniSistem.Inlines.Add(anamneza.UroGenitalSystem);
                UrogenitalniSistem.TextWrapping = TextWrapping.Wrap;

                LokomotorniSistem.Inlines.Add(new Run("Lokomotorni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline, Foreground = Brushes.White });
                LokomotorniSistem.Inlines.Add("   ");
                LokomotorniSistem.Inlines.Add(anamneza.LocomotorSystem);
                LokomotorniSistem.TextWrapping = TextWrapping.Wrap;

                NervniSistem.Inlines.Add(new Run("Nervni sistem:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline, Foreground = Brushes.White });
                NervniSistem.Inlines.Add("   ");
                NervniSistem.Inlines.Add(anamneza.NervousSystem);
                NervniSistem.TextWrapping = TextWrapping.Wrap;

                RanijaOboljenja.Inlines.Add(new Run("Ranija oboljenja:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline , Foreground = Brushes.White });
                RanijaOboljenja.Inlines.Add("   ");
                RanijaOboljenja.Inlines.Add(anamneza.PastDiseases);
                RanijaOboljenja.TextWrapping = TextWrapping.Wrap;

                PorodicniPodaci.Inlines.Add(new Run("Porodični podaci:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline, Foreground = Brushes.White });
                PorodicniPodaci.Inlines.Add("   ");
                PorodicniPodaci.Inlines.Add(anamneza.FamilyData);
                PorodicniPodaci.TextWrapping = TextWrapping.Wrap;

                SocijalnoEpidemioloskiPodaci.Inlines.Add(new Run("Socijalno-epidemiološki podaci:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline,Foreground = Brushes.White });
                SocijalnoEpidemioloskiPodaci.Inlines.Add("   ");
                SocijalnoEpidemioloskiPodaci.Inlines.Add(anamneza.SocioEpiData);
                SocijalnoEpidemioloskiPodaci.TextWrapping = TextWrapping.Wrap;
            }
            else
            {
                GlavneTegobe.Inlines.Add(new Run("Poštovani, anamneza za ovaj pregled nije napisana!") { FontWeight = FontWeights.Bold, Foreground=Brushes.White});
            }

        }

        private void ucitajPodatke()
        {
            if (anamneza.AnamnesisAppointment != null)
            {
                anamneza.AnamnesisAppointment = new AppointmentController().GetAppointment(anamneza.AnamnesisAppointment.AppointmentID);
                anamneza.AnamnesisAppointment.Doctor = new DoctorController().GetDoctor(anamneza.AnamnesisAppointment.Doctor.Jmbg);
                anamneza.AnamnesisAppointment.Patient = PocetnaStranica.getInstance().Pacijent;
            }
            
        }

        public Anamnesis Anamneza { get => anamneza; set => anamneza = value; }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            PocetnaStranica.getInstance().frame.Content = new IstorijaPregleda();
        }
    }
 }

