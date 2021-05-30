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
using SIMS.Repositories.SecretaryRepo;
using SIMS.LekarGUI.Dialogues.Izvestaji;
using SIMS.Model;
using SIMS.Controller;

namespace SIMS.LekarGUI.Pages
{
    /// <summary>
    /// Interaction logic for LDBAktivanTermin.xaml
    /// </summary>
    public partial class LDBActiveAppointment : Page
    {
        public static LDBActiveAppointment instance;
        private static Appointment activeAppointment;

        public static LDBActiveAppointment GetInstance(Appointment appointment)
        {
            if (instance == null)
            {
                instance = new LDBActiveAppointment();
                activeAppointment = appointment;
            }
            return instance;
        }

        public LDBActiveAppointment()
        {
            InitializeComponent();
        }

        private void ButtonRecord(object sender, RoutedEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(3);
            ShowRecordDialog();
        }

        private static void ShowRecordDialog()
        {
            if (activeAppointment.Type == AppointmentType.examination)
                new AnamnesisCreate(activeAppointment).ShowDialog();
            else
                new SurgeryReportCreate(activeAppointment).ShowDialog();
        }
    }
}
