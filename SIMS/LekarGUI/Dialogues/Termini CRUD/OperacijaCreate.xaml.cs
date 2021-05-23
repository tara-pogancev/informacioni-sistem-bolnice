using SIMS.Repositories.SecretaryRepo;
using SIMS.LekarGUI;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for TerminCreate.xaml
    /// </summary>
    public partial class OperacijaCreate : Window
    {
        private List<Doctor> lekari;
        private List<Patient> pacijenti;
        private List<Room> prostorije;
        private List<String> dostupniTermini;

        public OperacijaCreate(Patient patient)
        {
            InitializeComponents();

            foreach(Patient currentPatient in pacijenti)
            {
                if (patient.Jmbg == currentPatient.Jmbg)
                {
                    pacijentiCombo.SelectedItem = currentPatient;
                    break;
                }
            }
        }

        public OperacijaCreate()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            InitializeComponent();

            IDoctorRepository storageL = new DoctorFileRepository();
            lekari = storageL.GetAll();

            PatientFileRepository storageP = new PatientFileRepository();
            pacijenti = storageP.GetAll();

            prostorije = new List<Room>(RoomFileRepository.Instance.ReadAll().Values);

            doktoriCombo.ItemsSource = lekari;
            pacijentiCombo.ItemsSource = pacijenti;
            prostorijeCombo.ItemsSource = prostorije;

            List<String> trajanjeVrednosti = new List<String>() { "30 minuta", "60 minuta", "90 minuta" };
            trajanjeLista.ItemsSource = trajanjeVrednosti;

            int index = 0;
            foreach (Doctor l in lekari)
            {
                if (l.Jmbg.Equals(DoctorUI.GetInstance().GetUser().Jmbg))
                {
                    break;
                }
                index++;
            }
            doktoriCombo.SelectedIndex = index;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Odraditi sve provere

            if (doktoriCombo.SelectedItem == null || datePicker1.SelectedDate == null || terminiLista.SelectedItem == null)
                MessageBox.Show("Molimo popunite sva polja!");
            else
            {
                Appointment termin = new Appointment();

                String vrijemeIDatum = datePicker1.Text + " " + terminiLista.Text;
                DateTime vremenskaOdrednica = DateTime.Parse(vrijemeIDatum);
                termin.StartTime = vremenskaOdrednica;
                termin.InitialTime = vremenskaOdrednica;

                if (trajanjeLista.SelectedIndex == 0)
                    termin.Duration = 30;
                else if (trajanjeLista.SelectedIndex == 1)
                    termin.Duration = 60;
                else
                    termin.Duration = 90;

                termin.Room = prostorije[prostorijeCombo.SelectedIndex];
                termin.Patient = pacijenti[pacijentiCombo.SelectedIndex];
                termin.Doctor = lekari[doktoriCombo.SelectedIndex];
                termin.Type = AppointmentType.surgery;

                //PROVERA DOSTUPNOSTI LEKARA
                if (!lekari[doktoriCombo.SelectedIndex].IsFree(termin))
                    MessageBox.Show("Odabrani lekar nije dostupan u datom terminu. Molimo izaberite drugi termin.", "Upozorenje!");

                else
                {
                    termin.Doctor.Serialize = false;
                    termin.Patient.Serialize = false;
                    termin.Room.Serialize = false;

                    AppointmentFileRepository.Instance.Save(termin);
                    LekarTerminiPage.GetInstance().refresh();
                    this.Close();
                }
            }
            
        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doktoriCombo.SelectedItem != null)
            {
                Doctor lek = lekari[doktoriCombo.SelectedIndex];
                List<Appointment> doktoroviTermini = new List<Appointment>();
                dostupniTermini = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
                terminiLista.ItemsSource = dostupniTermini;
                /*
                foreach (Termin termin in new TerminStorage().ReadEntities())
                {
                    if (termin.LekarKey.Equals(lek.Jmbg) && OdabirDatuma.SelectedDate.Value.Date.ToShortDateString().Equals(termin.Datum))
                    {
                        doktoroviTermini.Add(termin);
                    }
                }

                foreach (Termin termin in doktoroviTermini)
                {
                    dostupniTermini.Remove(termin.Vrijeme);
                }
                */
            }
        }

    }

}
