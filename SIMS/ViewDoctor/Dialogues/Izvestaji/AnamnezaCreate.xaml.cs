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
using SIMS.Repositories.SecretaryRepo;
using SIMS.LekarGUI.Dialogues.Termini_CRUD;
using SIMS.Repositories.AnamnesisRepository;
using SIMS.Model;
using SIMS.DTO;
using SIMS.Controller;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for AnamnezaTrening.xaml
    /// </summary>
    public partial class AnamnesisCreate : Window
    {
        private Appointment appointment;
        private AppointmentController appointmentController = new AppointmentController();
        private AnamnesisController anamnesisController = new AnamnesisController();

        public AnamnesisCreate(Appointment appointment)
        {
            InitializeComponent();

            this.appointment = appointment;

            LabelDoctor.Content = "Doktor: " + appointment.GetDoctorName();
            LabelDate.Content = "Datum pregleda: " + appointment.GetAppointmentDate();

            LabelPatient.Content = "Pacijent: " + appointment.GetPatientName();
            LabelPatientDateOfBirth.Content = "Datum rođenja: " + appointment.Patient.GetDateOfBirthString();
        }

        private void ButtonAccept(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
                MessageBox.Show("Molimo popunite sva obavezna polja!");
            else
            {
                Patient patient = appointment.Patient;

                Anamnesis a = new Anamnesis(appointment, txt1.Text, txt2.Text, txt3.Text, txt4.Text, txt5.Text, txt6.Text,
                    txt7.Text, txt8.Text, txt9.Text, txt10.Text, txt11.Text, txt12.Text);

                anamnesisController.SaveAnamnesis(a);

                this.Close();
                DoctorUI.GetInstance().ChangeTab(3);
                new ActionsAfterReport(patient).ShowDialog();

            }
        }

        private bool ValidateForm()
        {
            return txt1.Text.Equals("") || txt2.Text.Equals("") || txt3.Text.Equals("");
        }
    }
}
