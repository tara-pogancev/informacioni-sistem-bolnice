using System;
using System.Collections.Generic;
using System.Globalization;
using SIMS.DTO;
using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;

namespace SIMS.Service.AppointmentServices
{
    public class DoctorAppointmentService
    {
        private IAppointmentRepository appointmentRepository = new AppointmentFileRepository();
        private RoomService roomService = new RoomService();
        private DoctorService doctorService = new DoctorService();

        public List<Appointment> GetAppointmentsByDoctor(Doctor doctor)
        {
            return appointmentRepository.GetDoctorAppointments(doctor);
        }

        public Appointment CheckIfActiveAppointment(Doctor doctor)
        {
            foreach (Appointment appointment in appointmentRepository.GetDoctorAppointments(doctor))
            {
                if (appointment.GetIfCurrent() && appointment.GetIfRecorded() == false)
                {
                    return appointment;
                }
            }

            return null;
        }

        public int GetRecordedAppointmentsByDoctor(Doctor doctor)
        {
            int recorded = 0;

            foreach (Appointment t in appointmentRepository.GetDoctorAppointments(doctor))
                if (t.GetIfRecorded())
                    recorded++;

            return recorded;
        }

        public List<AppointmentDTO> GetDTOFromList(List<Appointment> list)
        {
            List<AppointmentDTO> retVal = new List<AppointmentDTO>();
            foreach(Appointment appointment in list)
            {
                AppointmentDTO dto = new AppointmentDTO(appointment);
                retVal.Add(dto);
            }

            return retVal;
            
        }

        public List<Appointment> GetUpcommingAppointmentsByDoctor(Doctor doctor)
        {
            List<Appointment> retVal = new List<Appointment>();
            foreach (Appointment appointment in appointmentRepository.GetDoctorAppointments(doctor))
            {
                if (!appointment.GetIfPast() && !appointment.GetIfRecorded())
                    retVal.Add(appointment);
            }

            return retVal;
        }

        public List<Appointment> GetUpcommingAppointmentsByRoom(Room room)
        {
            List<Appointment> retVal = new List<Appointment>();
            foreach (Appointment appointment in appointmentRepository.GetAll())
            {
                if (!appointment.GetIfPast() && !appointment.GetIfRecorded())
                    if (appointment.Room.Number.Equals(room.Number))
                        retVal.Add(appointment);
            }

            return retVal;
        }


        public List<Appointment> GetUpcommingAppointments()
        {
            List<Appointment> retVal = new List<Appointment>();
            foreach (Appointment appointment in appointmentRepository.GetAll())
            {
                if (!appointment.GetIfPast() && !appointment.GetIfRecorded())
                    retVal.Add(appointment);
            }

            return retVal;
        }

        public AppointmentDTO GetDTO(Appointment appointment)
        {
            return new AppointmentDTO(appointment);
        }

        public List<int> GetAppointmentsCountForCurrentWeek(AppointmentType type, Doctor doctor)
        {
            return appointmentRepository.GetAppointmentsCountForCurrentWeek(type, doctor);
        }

        public List<Appointment> GetRecordedAppointmentsByDoctorList(Doctor doctor)
        {
            List<Appointment> retVal = new List<Appointment>();

            foreach (Appointment appointment in appointmentRepository.GetDoctorAppointments(doctor))
            {
                if (appointment.GetIfRecorded())
                    retVal.Add(appointment);
            }

            return retVal;
        }

        public List<Appointment> GetUnrecordedAppointmentsByDoctorList(Doctor doctor)
        {
            List<Appointment> retVal = new List<Appointment>();

            foreach (Appointment appointment in appointmentRepository.GetDoctorAppointments(doctor))
            {
                if (!appointment.GetIfRecorded() && appointment.GetIfPast())
                    retVal.Add(appointment);
            }

            return retVal;
        }

        public List<Appointment> GetFutureAppointmentsByDoctor(Doctor doctor)
        {
            List<Appointment> retVal = new List<Appointment>();

            foreach(Appointment appointment in appointmentRepository.GetDoctorAppointments(doctor))
            {
                if (!appointment.GetIfPast() && appointment.Doctor.Jmbg == doctor.Jmbg)
                    retVal.Add(appointment);
            }

            return retVal;
        }

        public List<DateTime> GetNearPotentialAppointments()
        {
            DateTime currentTime = DateTime.Now;

            List<String> availableTimes = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            List<String> availableDates = new List<String>();
            for (int i = 0; i < 10; i++)
            {
                DateTime currentDate = DateTime.Today.AddDays(i);
                availableDates.Add(currentDate.ToString("dd.MM.yyyy."));
            }

            List<DateTime> potentialAppointmentTimeList = new List<DateTime>();
            foreach (String date in availableDates)
            foreach (String time in availableTimes)
            {
                String dateAndTime = date + " " + time;
                DateTime appointmentTime = DateTime.ParseExact(dateAndTime, "dd.MM.yyyy. HH:mm", CultureInfo.InvariantCulture);
                if (appointmentTime >= currentTime)
                    potentialAppointmentTimeList.Add(appointmentTime);
            }

            return potentialAppointmentTimeList;
        }

        public List<Appointment> SortAppointmentsByTimeA(List<Appointment> appointments)
        {
            for (int i = 0; i < appointments.Count; i++)
            for (int j = 0; j < appointments.Count; j++)
                if (appointments[i].StartTime < appointments[j].StartTime)
                {
                    var temp = appointments[i];
                    appointments[i] = appointments[j];
                    appointments[j] = temp;
                }

            return appointments;
        }

        public List<Appointment> GetAvailableAppointmentsForAllDoctors(Specialization specialization, int duration, Patient patient)
        {
            List<Appointment> retVal = new List<Appointment>();

            foreach (Doctor doctor in doctorService.ReadBySpecialization(specialization))
            {
                List<DateTime> potentialAppointmentTimeList = GetNearPotentialAppointments();
                int counterByDoctor = 0;

                foreach (DateTime appTime in potentialAppointmentTimeList)
                {
                    Appointment appointment = new Appointment(appTime, duration, AppointmentType.surgery, doctor, patient, null);

                   foreach (Room roomNew in roomService.GetAllRooms())
                   {
                       if (roomNew.GetIfFreeForAppointment(appointment))
                       {
                            appointment.Room = roomNew;
                            break;
                       }
                   }

                    if (appointment.Room != null) {

                        if (doctorService.CheckIfFree(doctor, appointment))
                        {
                            counterByDoctor++;
                            retVal.Add(appointment);
                        }

                        if (counterByDoctor >= 5)
                            break;
                    }

                }

            }

            return retVal;
        }
    }
}