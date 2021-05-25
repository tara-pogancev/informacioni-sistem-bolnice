using SIMS.Repositories.SecretaryRepo;
using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using SIMS.Service;
using SIMS.PacijentGUI.ViewModel;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for MojiTermini.xaml
    /// </summary>
    public partial class MojiTermini : Page
    {
        Patient patient;
        public MojiTermini(Patient p)
        {
            patient = p;
            this.DataContext = new ScheduledAppointmentsViewModel(patient);
            InitializeComponent();
            
        }

        

       
        
    }
}
