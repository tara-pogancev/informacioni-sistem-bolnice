using SIMS.Repositories.SecretaryRepo;
using SIMS.LekarGUI.Dialogues.Termini_CRUD;
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

namespace SIMS.LekarGUI.Dialogues.Izvestaji
{
    /// <summary>
    /// Interaction logic for OperacijaCreate.xaml
    /// </summary>
    public partial class SurgeryReportCreate : Window
    {
        private Appointment operation;

        public SurgeryReportCreate(Appointment operationPar)
        {
            InitializeComponent();
            operation = operationPar;

            LabelDoctor.Content = "Doktor: " + operation.GetDoctorName();
            LabelDate.Content = "Datum operacije: " + operation.GetAppointmentDate();

            LabelPacijent.Content = "Pacijent: " + operation.GetPatientName();
            LabelBirthDate.Content = "Datum rođenja: " + operation.Patient.GetDateOfBirthString();

            LabelRoom.Content = "Prostorija: " + operation.Room.Number;

        }

        private void Button_Accept(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                Patient patient = operation.Patient;

                SurgeryReport report = new SurgeryReport(operation, OperationName.Text, OperationDescription.Text);
                SurgeryReportFileRepository.Instance.Save(report);

                this.Close();
                DoctorUI.GetInstance().ChangeTab(3);
                new ActionsAfterReport(patient).ShowDialog();

            }
            else
            {
                MessageBox.Show("Molimo popunite sva polja!");
            }

        }

        private bool ValidateForm()
        {
            return (!OperationName.Text.Equals("") && !OperationDescription.Text.Equals(""));
        }
    }
}
