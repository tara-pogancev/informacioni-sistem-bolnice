using SIMS.DTO;
using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;
using System;
using System.Collections.Generic;

namespace SIMS.Service
{
    
    public class AppointmentService 
    {
        private IAppointmentRepository appointmentRepository = new AppointmentFileRepository();
        private DoctorService doctorService = new DoctorService();
        private RoomService roomService = new RoomService();
        

        public AppointmentService()
        {
            
        }

        

        public List<Appointment> GetAllAppointments() => appointmentRepository.GetAll();

        public void UpdateAppointment(Appointment appointment) => appointmentRepository.Update(appointment);

        public void DeleteAppointment(Appointment appointment) => appointmentRepository.Delete(appointment.AppointmentID);

        public void SaveAppointment(Appointment appointment) => appointmentRepository.Save(appointment);

        public Appointment GetAppointment(String key) => appointmentRepository.FindById(key);

        public List<Appointment> GetPatientAppointments(Patient pacijent)=> appointmentRepository.GetPatientAppointments(pacijent);

        public List<Appointment> GetDoctorAppointments(Doctor doctor)=> appointmentRepository.GetDoctorAppointments(doctor);
        public List<Appointment> GetAppointmentsByDoctor(Doctor doctor)
        {
            return appointmentRepository.GetDoctorAppointments(doctor);
        }

        

        public Appointment CheckIfActiveAppointment(Doctor doctor)
        {
            foreach (Appointment appointment in GetAppointmentsByDoctor(doctor))
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

            foreach (Appointment t in GetAppointmentsByDoctor(doctor))
                if (t.GetIfRecorded())
                    recorded++;

            return recorded;
        }

        public int GetNumberOfFinishedAppointments(Patient patient)
        {
            List<Appointment> scheduledAppointments = appointmentRepository.GetPatientAppointments(patient);
            int finishedAppointmentCounter = 0;
            foreach (Appointment appointment in scheduledAppointments)
            {
                if (appointment.GetIfPast())
                {
                    finishedAppointmentCounter++;
                }
            }
            return finishedAppointmentCounter;
        }

        

        public List<Appointment> GetPastAppointmentsForPatient(Patient patient){
            List<Appointment> scheduledAppointments = appointmentRepository.GetPatientAppointments(patient);
             for (int i = 0; i < scheduledAppointments.Count; i++)
            {
                if (!scheduledAppointments[i].GetIfPast() || scheduledAppointments[i].Patient.Jmbg!=patient.Jmbg)
                {
                    scheduledAppointments.RemoveAt(i);
                    i--;
                }
            }
            return scheduledAppointments;

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
            foreach (Appointment appointment in GetAppointmentsByDoctor(doctor))
            {
                if (!appointment.GetIfPast() && !appointment.GetIfRecorded())
                    retVal.Add(appointment);
            }

            return retVal;
        }


        public List<Appointment> GetFutureAppointments(Patient patient)
        {
            List<Appointment> scheduledAppointments = appointmentRepository.GetPatientAppointments(patient);
            for (int i = 0; i < scheduledAppointments.Count; i++)
            {
                if (scheduledAppointments[i].GetIfPast())
                {
                    scheduledAppointments.RemoveAt(i);
                    i--;
                }
            }
            return scheduledAppointments;
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

            foreach (Appointment appointment in GetAppointmentsByDoctor(doctor))
            {
                if (appointment.GetIfRecorded())
                    retVal.Add(appointment);
            }

            return retVal;
        }

        public List<Appointment> GetUnrecordedAppointmentsByDoctorList(Doctor doctor)
        {
            List<Appointment> retVal = new List<Appointment>();

            foreach (Appointment appointment in GetAppointmentsByDoctor(doctor))
            {
                if (!appointment.GetIfRecorded() && appointment.GetIfPast())
                    retVal.Add(appointment);
            }

            return retVal;
        }

        public List<Appointment> GetFutureAppointmentsByDoctor(Doctor doctor)
        {
            List<Appointment> retVal = new List<Appointment>();

            foreach(Appointment appointment in GetAllAppointments())
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
                    DateTime appointmentTime = DateTime.Parse(dateAndTime);
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
                    //TODO: Promeniti prostoriju!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    Room room = roomService.GetAllRooms()[0];

                    Appointment appointment = new Appointment(appTime, duration, AppointmentType.surgery, doctor, patient, room);
                    if (doctorService.CheckIfFree(doctor, appointment))
                    {
                        counterByDoctor++;
                        retVal.Add(appointment);
                    }

                    if (counterByDoctor >= 5)
                        break;
                }

            }

            return retVal;
        }

    }
}
