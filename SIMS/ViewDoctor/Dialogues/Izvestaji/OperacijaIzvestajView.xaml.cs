using SIMS.Model;
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
using SIMS.Controller;

namespace SIMS.LekarGUI.Dialogues.Izvestaji
{
    /// <summary>
    /// Interaction logic for OperacijaIzvestajView.xaml
    /// </summary>
    public partial class SurgeryReportRead : Window
    {
        private PatientController patientController = new PatientController();

        public SurgeryReportRead(SurgeryReport report)
        {
            InitializeComponent();
            Appointment appointment = report.GetSurgery();
            appointment.InitData();
            Patient patient = patientController.GetPatient(appointment.Patient.Jmbg);

            LabelDoctor.Content = "Doktor: " + appointment.GetDoctorName();
            LabelDate.Content = "Datum operacije: " + appointment.GetAppointmentDate();

            LabelPacijent.Content = "Pacijent: " + patient.FullName;
            LabelBirthDate.Content = "Datum rođenja: " + patient.GetDateOfBirthString();

            LabelRoom.Content = "Prostorija: " + appointment.Room.Number;

            OperationName.Content = report.SurgeryName;
            OperationDescription.Text = report.SurgeryDescription;

        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
