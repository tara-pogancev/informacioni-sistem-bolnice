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

namespace SIMS.PacijentGUI
{
    
    public partial class IstorijaPregleda : Page
    {
       
        

        public IstorijaPregleda()
        {
            InitializeComponent();
            
            this.DataContext = new PastAppointmentsViewModel(PocetnaStranica.getInstance().frame.NavigationService, PocetnaStranica.getInstance().Pacijent);
        }
    }
}
