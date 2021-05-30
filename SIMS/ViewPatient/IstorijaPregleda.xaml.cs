using SIMS.Repositories.SecretaryRepo;
using SIMS.Model;
using SIMS.Repositories.AnamnesisRepository;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
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
using SIMS.Service;
using SIMS.ViewPatient.ViewModel;
using SIMS.Filters;
using SIMS.Controller;

namespace SIMS.PacijentGUI
{
    
    public partial class IstorijaPregleda : Page
    {

        PastAppointmentsViewModel pst;
        public IstorijaPregleda()
        {
            pst = new PastAppointmentsViewModel(PocetnaStranica.getInstance().frame.NavigationService, PocetnaStranica.getInstance().Pacijent);
            InitializeComponent();
            this.DataContext = pst;
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            
            terminiTabela.ItemsSource = AppointmentHistoryFilter.Instance.ApplyFilters(pst.PastAppointments, SearchBox.Text, false);
            this.DataContext = pst;
        }
    }
}
