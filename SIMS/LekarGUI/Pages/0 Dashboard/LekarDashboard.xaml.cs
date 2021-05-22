using LiveCharts;
using LiveCharts.Wpf;
using SIMS.Repositories.PatientRepo;
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

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for LekarDashboard.xaml
    /// </summary>
    public partial class LekarDashboard : Page
    {
        private static Doctor lekarUser;

        public LekarDashboard(Doctor l)
        {
            lekarUser = l;

            InitializeComponent();

            this.DataContext = this;

            WelcomeMSG.Content = lekarUser.Ime + ", dobro došli!";
            refresh();

        }

        public void refresh()
        {
            setAktivanTermin();
            RefreshGraphs1();
            RefreshGraphs2();
        }

        public void setAktivanTermin()
        {
            //TODO uraditi varijaciju
            List<Appointment> termini = AppointmentFileRepository.Instance.GetDoctorAppointments(lekarUser);

            foreach (Appointment t in termini)
            {
                if (t.IsCurrent && t.Evidentiran == false)
                {
                    AktivniTermin.Content = LDBAktivanTermin.GetInstance(t);
                    return;
                }
            }

            AktivniTermin.Content = LDBNemaTermin.GetInstance();
        }

        private void Button_Termini(object sender, RoutedEventArgs e)
        {
            LekarUI.GetInstance().ChangeTab(1);
        }

        private void Button_Hitno(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void RefreshGraphs1()
        {
            List<Appointment> termini = AppointmentFileRepository.Instance.GetDoctorAppointments(lekarUser);

            int evidentirani = 0;
            int ukupno = termini.Count;

            foreach (Appointment t in termini)
                if (t.Evidentiran)
                    evidentirani++;

            GraphEvidentirani.To = ukupno;
            GraphEvidentirani.Value = evidentirani;

        }

        private void RefreshGraphs2()
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Pregledi",
                    Values = new ChartValues<int>(AppointmentFileRepository.Instance.GetAppointmentsCountForCurrentWeek(AppointmentType.pregled, lekarUser)),
                    Stroke = new SolidColorBrush(Color.FromRgb(87,214,180))

        },
                new LineSeries
                {
                    Title = "Operacije",
                    Values = new ChartValues<int>(AppointmentFileRepository.Instance.GetAppointmentsCountForCurrentWeek(AppointmentType.operacija, lekarUser)),
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
