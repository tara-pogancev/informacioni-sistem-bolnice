﻿using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Service
{
    public class AppointmentService
    {
        private IAppointmentRepository appointmentRepository;

        public AppointmentService()
        {
            appointmentRepository = new AppointmentFileRepository();
            
        }

        public List<Appointment> GetAllAppointments() => appointmentRepository.GetAll();

        public void UpdateAppointment(Appointment appointment) => appointmentRepository.Update(appointment);

        public void DeleteAppointment(Appointment appointment) => appointmentRepository.Delete(appointment.AppointmentID);

        public void SaveAppointment(Appointment appointment) => appointmentRepository.Save(appointment);

        public Appointment GetAppointment(String key) => appointmentRepository.FindById(key);

        //Staviti DTO object
        public List<String> GetAvailableTimeOfAppointment(Doctor doctor,String date,Patient patient)
        {
            List<String> timeOfAppointment = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            foreach (Appointment appointment in GetScheduledAppointments(date))
            {
                if (doctor.Unavailable(appointment) || patient.Unvailable(appointment)) 
                {
                    timeOfAppointment.Remove(appointment.AppointmentTime);
                }
            }
            return timeOfAppointment;
        }

        //TODO staviti da baca excetion
        public bool ScheduleAppointment(Doctor doctor,DateTime date,Patient patient)
        {
            RoomAvailabilityService roomAvailabilityService = new RoomAvailabilityService();
            List<Room> availableRooms = roomAvailabilityService.GetAvailableRooms(date);
            if (availableRooms.Count == 0)
            {
                return false;
            }

            appointmentRepository.Save(new Appointment(date, 30, AppointmentType.examination, doctor, patient, availableRooms[0]));
            return true;
        }

        public int GetNumberOfFinishedAppointments(Patient patient)
        {
            List<Appointment> scheduledAppointments = appointmentRepository.GetPatientAppointments(patient);
            int finishedAppointmentCounter = 0;
            foreach (Appointment appointment in scheduledAppointments)
            {
                if (appointment.IsPast)
                {
                    finishedAppointmentCounter++;
                }
            }
            return finishedAppointmentCounter;
        }


        private List<Appointment> GetScheduledAppointments(String date)
        {
            List<Appointment> scheduledAppointments = appointmentRepository.GetAll();
            for (int i = 0; i < scheduledAppointments.Count;i++)
            {
                    if (scheduledAppointments[i].AppointmentDate != date)
                    {
                        scheduledAppointments.RemoveAt(i);
                        i--;
                    }
            }
            return scheduledAppointments;
        }

        public List<Appointment> GetPastAppointments()
        {
            List<Appointment> scheduledAppointments = appointmentRepository.GetAll();
            for (int i = 0; i < scheduledAppointments.Count; i++)
            {
                if (!scheduledAppointments[i].IsPast)
                {
                    scheduledAppointments.RemoveAt(i);
                    i--;
                }
            }
            return scheduledAppointments;
        }
        public List<Appointment> GetFutureAppointments(Patient patient)
        {
            List<Appointment> scheduledAppointments = appointmentRepository.GetPatientAppointments(patient);
            for (int i = 0; i < scheduledAppointments.Count; i++)
            {
                if (scheduledAppointments[i].IsPast)
                {
                    scheduledAppointments.RemoveAt(i);
                    i--;
                }
            }
            return scheduledAppointments;
        }

        public void CancelAppointment(Appointment appointment)
        {

            DeleteAppointment(appointment);

            AppointmentLog terminLog = new AppointmentLog(appointment.AppointmentID + appointment.Patient.Jmbg + DateTime.Now.ToString("hhmmss"), appointment.AppointmentID, appointment.Patient.Jmbg, DateTime.Now, SurgeryType.Brisanje);
            new AppointmentLogFileRepository().Save(terminLog);
        }

        public void ChangeAppointment(Appointment appointment)
        {
            UpdateAppointment(appointment);
            AppointmentLog terminLog = new AppointmentLog(appointment.AppointmentID + appointment.Patient.Jmbg + DateTime.Now.ToString("hhmmss"), appointment.AppointmentID, appointment.Patient.Jmbg, DateTime.Now, SurgeryType.Brisanje);
            new AppointmentLogFileRepository().Save(terminLog);

        }
       
    }
}
