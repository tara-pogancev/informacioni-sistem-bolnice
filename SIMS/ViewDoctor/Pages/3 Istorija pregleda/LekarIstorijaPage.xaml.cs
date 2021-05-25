using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SIMS.LekarGUI.Dialogues.Izvestaji;
using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorSurveyRepo;
using SIMS.Repositories.DoctorRepo;
using SIMS.Repositories.AnamnesisRepository;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for LekarIstorijaPage.xaml
    /// </summary>
    public partial class AppointmentHistoryView : Page
    {
        private static Doctor doctorUser;

        public ObservableCollection<Appointment> AnamnesisViewModel { get; set; }
        public ObservableCollection<Appointment> AppointmentsViewModel { get; set; }

        public AppointmentHistoryView(Doctor doctor)
        {
            InitializeComponent();
            doctorUser = doctor;

            this.DataContext = this;
            AnamnesisViewModel = new ObservableCollection<Appointment>(AppointmentFileRepository.Instance.GetAll());
            AppointmentsViewModel = new ObservableCollection<Appointment>(AppointmentFileRepository.Instance.GetAll());
            refreshView();
        }

        public void refreshView()
        {
            AnamnesisViewModel.Clear();
            AppointmentsViewModel.Clear();

            List<Appointment> appointments = new List<Appointment>(AppointmentFileRepository.Instance.GetDoctorAppointments(doctorUser));
            foreach (Appointment appointment in appointments)
            {
                appointment.InitData();

                if (appointment.GetIfRecorded() == true)
                    AnamnesisViewModel.Add(appointment);

                else if (appointment.IsPast() == true)
                {
                    AppointmentsViewModel.Add(appointment);
                }
            }
        }

        private void ButtonReadReport(object sender, RoutedEventArgs e)
        {
            if (dataGridReports.SelectedItem != null)
            {
                Appointment appointnment = (Appointment)dataGridReports.SelectedItem;

                if (appointnment.Type == AppointmentType.examination)
                {
                    ShowAppointmentAnamnesis(appointnment);
                }
                else
                {
                    ShowSurgeryReport(appointnment);
                }
            }
        }

        private static void ShowSurgeryReport(Appointment appointnment)
        {
            SurgeryReport report = SurgeryReportFileRepository.Instance.FindById(appointnment.AppointmentID);
            if (report != null)
            {
                SurgeryReportRead a = new SurgeryReportRead(report);
                a.Show();
            }
        }

        private static void ShowAppointmentAnamnesis(Appointment appointnment)
        {
            Anamnesis anamnesis = AnamnesisFileRepository.Instance.FindById(appointnment.AppointmentID);
            if (anamnesis != null)
            {
                AnamnesisRead a = new AnamnesisRead(anamnesis);
                a.Show();
            }
        }

        private void ButtonWriteReport(object sender, RoutedEventArgs e)
        {
            if (dataGridAppointments.SelectedItem != null)
            {
                Appointment appointment = (Appointment)dataGridAppointments.SelectedItem;

                if (appointment.Type == AppointmentType.examination)
                {
                    WriteAnamnesis();
                }
                else
                {
                    WriteSurgeryReport();
                }
            }
        }

        private void WriteSurgeryReport()
        {
            OperacijaIzvestajCreate o = new OperacijaIzvestajCreate((Appointment)dataGridAppointments.SelectedItem);
            o.ShowDialog();
            refreshView();
        }

        private void WriteAnamnesis()
        {
            AnamnezaCreate a = new AnamnezaCreate((Appointment)dataGridAppointments.SelectedItem);
            a.ShowDialog();
            refreshView();
        }

        private void Button_Home(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(0);
        }

    }
}
