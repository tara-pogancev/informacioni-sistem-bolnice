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
using SIMS.DTO;
using SIMS.Controller;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for LekarIstorijaPage.xaml
    /// </summary>
    public partial class AppointmentHistoryView : Page
    {
        private static Doctor doctorUser;

        public ObservableCollection<AppointmentDTO> AnamnesisViewModel { get; set; }
        public ObservableCollection<AppointmentDTO> AppointmentsViewModel { get; set; }

        private AppointmentController appointmentController = new AppointmentController();
        private SurgeryReportController reportController = new SurgeryReportController();
        private AnamnesisController anamnesisController = new AnamnesisController();
        private DoctorAppointmentController doctorAppointmentController = new DoctorAppointmentController();

        public AppointmentHistoryView(Doctor doctor)
        {
            InitializeComponent();
            doctorUser = doctor;

            this.DataContext = this;
            AnamnesisViewModel = new ObservableCollection<AppointmentDTO>();
            AppointmentsViewModel = new ObservableCollection<AppointmentDTO>();
            RefreshView();
        }

        public void RefreshView()
        {
            AnamnesisViewModel.Clear();
            AppointmentsViewModel.Clear();

            foreach (AppointmentDTO dto in GetRecotdedDTO())
                AnamnesisViewModel.Add(dto);

            foreach (AppointmentDTO dto in GetUnrecordedDTO())
                AppointmentsViewModel.Add(dto);
        }

        private List<AppointmentDTO> GetRecotdedDTO()
        {
            return doctorAppointmentController.GetDTOFromList(doctorAppointmentController.GetRecordedAppointmentsByDoctorList(doctorUser));
        }

        private List<AppointmentDTO> GetUnrecordedDTO()
        {
            return doctorAppointmentController.GetDTOFromList(doctorAppointmentController.GetUnrecordedAppointmentsByDoctorList(doctorUser));
        }

        private void ButtonReadReport(object sender, RoutedEventArgs e)
        {
            if (dataGridReports.SelectedItem != null)
            {
                Appointment appointnment = GetSelectedReport();

                if (appointnment.Type == AppointmentType.examination)
                    ShowAppointmentAnamnesis(appointnment);

                else
                    ShowSurgeryReport(appointnment);
            }
        }

        private Appointment GetSelectedReport()
        {
            AppointmentDTO dto = (AppointmentDTO)dataGridReports.SelectedItem;
            return appointmentController.GetAppointment(dto.AppointmentID);
        }

        private Appointment GetSelectedAppointment()
        {
            AppointmentDTO dto = (AppointmentDTO)dataGridAppointments.SelectedItem;
            return appointmentController.GetAppointment(dto.AppointmentID);
        }

        private void ShowSurgeryReport(Appointment appointnment)
        {
            SurgeryReport report = reportController.GetReport(appointnment.AppointmentID);
            if (report != null)
                new SurgeryReportRead(report).Show();
        }

        private void ShowAppointmentAnamnesis(Appointment appointnment)
        {
            Anamnesis anamnesis = anamnesisController.GetAnamnesis(appointnment.AppointmentID);
            if (anamnesis != null)
                new AnamnesisRead(anamnesis).Show();
        }

        private void ButtonWriteReport(object sender, RoutedEventArgs e)
        {
            if (dataGridAppointments.SelectedItem != null)
            {
                Appointment appointment = GetSelectedAppointment();

                if (appointment.Type == AppointmentType.examination)
                    WriteAnamnesis();
                
                else
                    WriteSurgeryReport();
                
            }
        }

        private void WriteSurgeryReport()
        {
            new SurgeryReportCreate(GetSelectedAppointment()).ShowDialog();
            RefreshView();
        }

        private void WriteAnamnesis()
        {
            new AnamnesisCreate(GetSelectedAppointment()).ShowDialog();
            RefreshView();
        }

        private void ButtonHome(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(0);
        }

    }
}
