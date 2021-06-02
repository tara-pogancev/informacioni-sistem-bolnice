using SIMS.Repositories.SecretaryRepo;
using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;
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
using SIMS.PacijentGUI.ViewModel;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for AnketaBolnice.xaml
    /// </summary>
    public partial class AnketaBolnicePage : Page
    {
        public AnketaBolnicePage()
        {
            InitializeComponent();
            this.DataContext = new HospitalSurveyViewModel();
        }

       
    }
}
