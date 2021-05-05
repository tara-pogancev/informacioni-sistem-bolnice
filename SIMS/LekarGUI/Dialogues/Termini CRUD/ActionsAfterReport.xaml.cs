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
using SIMS.LekarGUI.Dialogues.Recepti_i_terapije;

namespace SIMS.LekarGUI.Dialogues.Termini_CRUD
{
    /// <summary>
    /// Interaction logic for ActionsAfterReport.xaml
    /// </summary>
    public partial class ActionsAfterReport : Window
    {
        private Pacijent patient;

        public ActionsAfterReport(Pacijent patient)
        {
            this.patient = patient;
            InitializeComponent();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PrikazProfila(object sender, RoutedEventArgs e)
        {
            LekarUI.GetInstance().SellectedTab.Content = PacijentKartonView.GetInstance(patient);
            this.Close();
        }

        private void IzdavanjeRecepta(object sender, RoutedEventArgs e)
        {
            var window = new LekarIzdavanjeRecepta(patient);
            window.ShowDialog();
        }

        private void PisajneUputa(object sender, RoutedEventArgs e)
        {
            var window = new UputCreate(patient);
            window.ShowDialog();
        }

        private void ZakazivajneOperacije(object sender, RoutedEventArgs e)
        {
            var window = new OperacijaCreate(patient);
            window.ShowDialog();
        }

        private void PisanjeTerapije(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void ZakazivanjePregleda(object sender, RoutedEventArgs e)
        {
            var window = new TerminCreate(patient);
            window.ShowDialog();
        }

        private void HitnaOperacija(object sender, RoutedEventArgs e)
        {
            var window = new HitnaOperacijaCreate(patient);
            window.ShowDialog();
        }
    }
}
