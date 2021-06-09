using SIMS.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SIMS.ViewSecretary.Appointments
{
    public partial class AppointmentDetails : Page
    {
        public AppointmentDetails(Appointment appointment)
        {
            InitializeComponent();

            SetValuesForSelectedAppointment(appointment);
        }

        private void SetValuesForSelectedAppointment(Appointment appointment)
        {
            if (appointment.Type == AppointmentType.examination)
                typeTextBox.Text = TranslationSource.Instance["Examination"];
            else
                typeTextBox.Text = TranslationSource.Instance["Surgery"];
            doctorTextBox.Text = appointment.Doctor.FullName;
            patientTextBox.Text = appointment.Patient.FullName;
            roomTextBox.Text = appointment.Room.Number;
            dateTextBox.Text = appointment.StartTime.ToString("dd.MM.yyyy.");
            appointmentTextBox.Text = appointment.StartTime.ToString("HH:mm");
            durationTextBox.Text = appointment.Duration.ToString();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(ViewAppointments.GetInstance());
        }
    }
}
