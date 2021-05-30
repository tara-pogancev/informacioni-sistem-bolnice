using LiveCharts;
using LiveCharts.Wpf;
using SIMS.Repositories.SecretaryRepo;
using SIMS.LekarGUI.Pages;
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
using SIMS.Model;
using SIMS.Controller;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for LekarDashboard.xaml
    /// </summary>
    public partial class DoctorDashboard : Page
    {
        private static Doctor doctorUser;
        private AppointmentController appointmentController = new AppointmentController();

        public DoctorDashboard(Doctor doctor)
        {
            doctorUser = doctor;
            InitializeComponent();

            this.DataContext = this;

            WelcomeMSG.Content = doctorUser.Name + ", dobro došli!";
            Refresh();

        }

        public void Refresh()
        {
            SetActiveAppointment();
            RefreshGraphs1();
            RefreshGraphs2();
        }

        public void SetActiveAppointment()
        {
            Appointment currentAppointment = appointmentController.CheckIfActiveAppointment(doctorUser);

            if (currentAppointment == null)
                AktivniTermin.Content = LDBNemaTermin.GetInstance();
            else AktivniTermin.Content = LDBActiveAppointment.GetInstance(currentAppointment);

        }

        private void ButtonAppointments(object sender, RoutedEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(1);
        }

        private void ButtonNotifications(object sender, RoutedEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(6);
        }

        private void RefreshGraphs1()
        {
            int recorded = appointmentController.GetRecordedAppointmentsByDoctor(doctorUser);
            int total = appointmentController.GetAppointmentsByDoctor(doctorUser).Count;

            GraphEvidentirani.To = total;
            GraphEvidentirani.Value = recorded;
        }

        private void RefreshGraphs2()
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Pregledi",
                    Values = new ChartValues<int>(appointmentController.GetAppointmentsCountForCurrentWeek(AppointmentType.examination, doctorUser)),
                    Stroke = new SolidColorBrush(Color.FromRgb(87,214,180))

        },
                new LineSeries
                {
                    Title = "Operacije",
                    Values = new ChartValues<int>(appointmentController.GetAppointmentsCountForCurrentWeek(AppointmentType.surgery, doctorUser)),
                    Stroke = new SolidColorBrush(Color.FromRgb(226,104,104))

                }
            };

            Labels = new[] { "Ponedeljak", "Utorak", "Sreda", "Četvrtak", "Petak", "Subota", "Nedelja"};

            DataContext = this;

        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

    }
}
