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

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for AnamnezaTrening.xaml
    /// </summary>
    public partial class AnamnezaCreate : Window
    {
        private Appointment termin;

        public AnamnezaCreate(Appointment t)
        {
            InitializeComponent();

            termin = t;

            LabelDoktor.Content = "Doktor: " + termin.DoctorName;
            if (termin.Type == AppointmentType.examination)
                LabelDatum.Content = "Datum pregleda: " + termin.AppointmentDate;
            else LabelDatum.Content = "Datum operacije: " + termin.AppointmentDate;

            LabelPacijent.Content = "Pacijent: " + termin.PatientName;
            LabelDatumRodjenja.Content = "Datum rođenja: " + PatientFileRepository.Instance.FindById(termin.Patient.Jmbg).DateOfBirth.ToString("dd.MM.yyyy.");

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txt1.Text.Equals("") || txt2.Text.Equals("") || txt3.Text.Equals(""))
                MessageBox.Show("Molimo popunite sva obavezna polja!");
            else
            {
                Patient patient = termin.Patient;

                Anamnesis a = new Anamnesis(termin, txt1.Text, txt2.Text, txt3.Text, txt4.Text, txt5.Text, txt6.Text,
                    txt7.Text, txt8.Text, txt9.Text, txt10.Text, txt11.Text, txt12.Text);
                a.AnamnesisAppointment.Serialize = false;
                AnamnesisFileRepository.Instance.Save(a);
                this.Close();

                DoctorUI.GetInstance().ChangeTab(3);
                var window = new ActionsAfterReport(patient);
                window.Show();

            }
        }
    }
}
