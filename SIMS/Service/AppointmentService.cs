﻿using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    public class AppointmentService
    {
        private IAppointmentRepository appointmentRepository;

        public AppointmentService()
        {
            appointmentRepository = new AppointmentFileRepository();            
        }

        public List<String> GetAvailableTimeOfAppointment(Doctor doctor,String date,Patient patient)
        {
            List<String> timeOfAppointment = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            foreach (Appointment appointment in GetScheduledAppointments(date))
            {
                if (!doctor.Available(appointment) || !patient.Available(appointment)) //|| //!RoomAvailable(appointment))
                {
                    timeOfAppointment.Remove(appointment.AppointmentTime);
                }
            }
            return timeOfAppointment;
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
    }
}
