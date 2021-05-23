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
    public partial class OperacijaIzvestajCreate : Window
    {
        private Appointment operation;

        public OperacijaIzvestajCreate(Appointment operationPar)
        {
            InitializeComponent();
            operation = operationPar;

            LabelDoctor.Content = "Doktor: " + operation.DoctorName;
            LabelDate.Content = "Datum operacije: " + operation.AppointmentDate;

            LabelPacijent.Content = "Pacijent: " + operation.PatientName;
            LabelBirthDate.Content = "Datum rođenja: " + operation.Patient.DateOfBirth;

            LabelRoom.Content = "Prostorija: " + operation.Room.Number;

        }

        private void Button_Accept(object sender, RoutedEventArgs e)
        {
            if (!OperationName.Text.Equals("") && !OperationDescription.Text.Equals(""))
            {
                Patient patient = operation.Patient;

                SurgeryReport o = new SurgeryReport(operation, OperationName.Text, OperationDescription.Text);
                o.Operacija.Doctor.Serialize = false;
                o.Operacija.Patient.Serialize = false;
                o.Operacija.Serialize = false;
                SurgeryReportFileRepository.Instance.Save(o);

                this.Close();
                DoctorUI.GetInstance().ChangeTab(3);
                var window = new ActionsAfterReport(patient);
                window.Show();


            } else
            {
                MessageBox.Show("Molimo popunite sva polja!");
            }       

        }
    }
}
