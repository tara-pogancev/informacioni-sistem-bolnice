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
using SIMS.Model;
using SIMS.Controller;
using SIMS.ViewDoctor.Pages;

namespace SIMS.LekarGUI.Dialogues.Hospitalizacija
{
    /// <summary>
    /// Interaction logic for HospitalizeCreate.xaml
    /// </summary>
    public partial class HospitalizeCreate : Window
    {
        private Patient patient;
        private Doctor doctor = DoctorUI.GetInstance().GetUser();
        private HospitalizationController hospitalizationController = new HospitalizationController();
        private RoomController roomController = new RoomController();
        private RoomInventoryController RoomInventoryController = new RoomInventoryController(); 
        private List<Room> rooms;

        public HospitalizeCreate(Patient patientPar)
        {
            InitializeComponent();
            patient = patientPar;

            LabelDoctor.Content = "Doktor: " + doctor.FullName;
            LabelPatient.Content = "Pacijent: " + patient.FullName;

            rooms = roomController.GetAllHospitalizationRooms();
            roomCombo.ItemsSource = rooms;

        }

        private void ButtonAccept(object sender, RoutedEventArgs e)
        {
            DoCreateHospitalization();
        }

        private void DoCreateHospitalization()
        {
            if (ValidateForm())
            {
                if (StartDate.SelectedDate > EndDate.SelectedDate)
                    MessageBox.Show("Početni datum ne sme biti nakon krajnjeg!");
                else if (!RoomInventoryController.GetIfAvailableBeds(GetSelectedRoom()))
                    MessageBox.Show("Odabrana soba nema dostupnih kreveta!");
                else
                {
                    CreateHospitalization();
                    this.Close();
                    DoctorUI.GetInstance().SellectedTab.Content = new PatientHospitalizationPage(patient);
                    MessageBox.Show("Pacijent uspešno hospitalizovan!");
                }
            }
            else
                MessageBox.Show("Molimo popunite sva polja!");
        }

        private bool ValidateForm()
        {
            return StartDate.SelectedDate != null && EndDate.SelectedDate != null && roomCombo.SelectedItem != null;
        }

        private void CreateHospitalization()
        {
            Room room = GetSelectedRoom();
            Hospitalization hospitalization = new Hospitalization(patient, doctor,
            (DateTime)StartDate.SelectedDate, (DateTime)EndDate.SelectedDate, room);
            hospitalizationController.SaveHospitalization(hospitalization);
        }

        private Room GetSelectedRoom()
        {
            return (Room)roomCombo.SelectedItem;
        }

        private void WindowKeyListener(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
            else if (e.Key == Key.Return)
                DoCreateHospitalization();
        }
    }
}
