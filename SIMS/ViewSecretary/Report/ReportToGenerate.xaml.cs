using SIMS.Controller;
using SIMS.Model;
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

namespace SIMS.ViewSecretary.Report
{
    /// <summary>
    /// Interaction logic for ReportToGenerate.xaml
    /// </summary>
    public partial class ReportToGenerate : Page
    {
        private AppointmentController appointmentController = new AppointmentController();
        private RoomController roomController = new RoomController();

        public ObservableCollection<OccupiedRoom> occupiedRooms;
        public ReportToGenerate(DateTime startDate, DateTime endDate)
        {
            InitializeComponent();

            occupiedRooms = new ObservableCollection<OccupiedRoom>();
            foreach (Room r in roomController.GetAllRooms())
            {
                occupiedRooms.Add(new OccupiedRoom(r.Number));
            }
            SetOccupiedTimeForEachRoom(startDate, endDate);
            Lista.ItemsSource = occupiedRooms;

            SecretaryText.Text = SecretaryUI.GetInstance().GetSecretary().FullName;
            DateText.Text = DateTime.Now.ToString("dd.MM.yyyy.");
        }

        private void SetOccupiedTimeForEachRoom(DateTime startTime, DateTime endTime)
        {
            List<Appointment> appointments = appointmentController.GetAllAppointments();
            SortAppointments(appointments);
            foreach (OccupiedRoom occupiedRoom in occupiedRooms)
            {
                foreach(Appointment appointment in appointments)
                {
                    if (occupiedRoom.RoomNumber.Equals(appointment.Room.Number) && 
                        appointment.StartTime.Date >= startTime && appointment.StartTime.Date <= endTime)
                    {
                        occupiedRoom.OccupationTime += appointment.StartTime.Date.ToString("dd.MM.yyyy.") + " " + appointment.StartTime.TimeOfDay.ToString() + " - " + appointment.GetEndTime().TimeOfDay.ToString() + "\n";
                    }
                }
                if (occupiedRoom.OccupationTime == null)
                {
                    occupiedRoom.OccupationTime = TranslationSource.Instance["RoomNotOccupied"];
                }
            }
        }

        private void SortAppointments(List<Appointment> appointments)
        {
            for (int i = 0; i < appointments.Count - 1; ++i)
            {
                for (int j = 0; j < appointments.Count - i - 1; ++j)
                {
                    if (appointments[j].StartTime > appointments[j + 1].StartTime)
                    {
                        Appointment temp = appointments[j];
                        appointments[j] = appointments[j + 1];
                        appointments[j + 1] = temp;
                    }
                }
            }
        }
    }
}
