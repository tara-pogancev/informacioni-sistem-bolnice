using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for TerminCreate.xaml
    /// </summary>
    public partial class TerminUpdate : Window
    {
        private List<Doctor> lekari;
        private List<Patient> pacijenti;
        private List<Room> prostorije;
        private List<String> termini;
        Appointment termin;

        public TerminUpdate(Appointment t)
        {
            InitializeComponent();
            this.termin = t;

            IDoctorRepository storageL = new DoctorFileRepository();
            lekari = storageL.GetAll();

            PatientFileRepository storageP = new PatientFileRepository();
            pacijenti = storageP.GetAll();

            prostorije = new List<Room>(RoomFileRepository.Instance.ReadAll().Values);

            doktoriCombo.ItemsSource = lekari;
            pacijentiCombo.ItemsSource = pacijenti;
            prostorijeCombo.ItemsSource = prostorije;

            this.SetValues();

            List<String> trajanjeVrednosti = new List<String>() { "30 minuta", "60 minuta", "90 minuta" };
            trajanjeLista.ItemsSource = trajanjeVrednosti;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Izmena pregleda
            //TODO: Odraditi sve provere

            if (doktoriCombo.SelectedItem == null || datePicker1.SelectedDate == null || terminiLista.SelectedItem == null)
                MessageBox.Show("Molimo popunite sva polja!");
            else
            {
                String vrijemeIDatum = datePicker1.Text + " " + terminiLista.Text;
                DateTime vremenskaOdrednica = DateTime.Parse(vrijemeIDatum);
                termin.StartTime = vremenskaOdrednica;

                if (trajanjeLista.SelectedIndex == 0)
                    termin.Duration = 30;
                else if (trajanjeLista.SelectedIndex == 1)
                    termin.Duration = 60;
                else
                    termin.Duration = 90;

                termin.Room = prostorije[prostorijeCombo.SelectedIndex];
                termin.Patient = pacijenti[pacijentiCombo.SelectedIndex];
                termin.Doctor = lekari[doktoriCombo.SelectedIndex];

                if (!lekari[doktoriCombo.SelectedIndex].IsFreeUpdate(termin))
                    MessageBox.Show("Odabrani lekar nije dostupan u datom terminu. Molimo izaberite drugi termin.", "Upozorenje!");

                else
                {
                    termin.Doctor.Serialize = false;
                    termin.Patient.Serialize = false;
                    termin.Room.Serialize = false;

                    AppointmentFileRepository.Instance.Update(termin);
                    LekarTerminiPage.GetInstance().refresh();
                    this.Close();
                }

            }
        }

        private void SetValues()
        {
            termini = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            terminiLista.ItemsSource = termini;

            datePicker1.DisplayDate = termin.StartTime;
            datePicker1.Text = termin.StartTime.ToString("dd.MM.yyyy.");

            if (termin.Duration == 30)
                trajanjeLista.SelectedIndex = 0;
            else if (termin.Duration == 60)
                trajanjeLista.SelectedIndex = 1;
            else
                trajanjeLista.SelectedIndex = 2;

            int index = 0;
            foreach (Doctor l in lekari)
            {
                if (l.Jmbg.Equals(termin.Doctor.Jmbg))
                {
                    break;
                }
                index++;
            }
            doktoriCombo.SelectedIndex = index;

            index = 0;
            foreach (String str in termini)
            {
                if (str.Equals(termin.AppointmentTime))
                {
                    break;
                }
                index++;
            }
            terminiLista.SelectedIndex = index;

            index = 0;
            foreach (Patient p in pacijenti)
            {
                if (p.Jmbg.Equals(termin.Patient.Jmbg))
                {
                    break;
                }
                index++;
            }
            pacijentiCombo.SelectedIndex = index;

            index = 0;
            foreach (Room pr in prostorije)
            {
                if (pr.Number.Equals(termin.Room.Number))
                {
                    break;
                }
                index++;
            }
            prostorijeCombo.SelectedIndex = index;

        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            if (doktoriCombo.SelectedIndex != -1)
            {
                Lekar lek = lekari[doktoriCombo.SelectedIndex];
                termini = new List<string>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
                foreach (Termin ter in new TerminStorage().ReadEntities())
                {
                    if (termin.LekarKey.Equals(lek.Jmbg))
                    {
                        if (OdabirDatuma.SelectedDate.Value.Date.ToShortDateString().Equals(ter.Datum) && !termin.Vrijeme.Equals(ter.Vrijeme))
                            termini.Remove(ter.Vrijeme);
                    }
                }
            
                terminiLista.ItemsSource = termini;
            
            }
            */
        }

    }


}
