﻿using SIMS.Commands;
using SIMS.Controller;
using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.HospitalSurveyRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SIMS.PacijentGUI.ViewModel
{
    class HospitalSurveyViewModel:ViewModelPatient
    {

        #region fields
        private int questionOneAnswer;
        private int questionTwoAnswer;
        private int questionThreeAnswer;
        private int questionFourAnswer;
        private int questionFiveAnswer;
        private string comment;
        


        public int QuestionOneAnswer
        {
            get { return questionOneAnswer; }
            set
            {
                questionOneAnswer = value;
                OnPropertyChanged();
            }
        }

        public int QuestionTwoAnswer
        {
            get { return questionTwoAnswer; }
            set
            {
                questionTwoAnswer = value;
                OnPropertyChanged();
            }
        }

        public int QuestionThreeAnswer
        {
            get { return questionThreeAnswer; }
            set
            {
                questionThreeAnswer = value;
                OnPropertyChanged();
            }
        }

        public int QuestionFourAnswer
        {
            get { return questionFourAnswer; }
            set
            {
                questionFourAnswer = value;
                OnPropertyChanged();
            }
        }

        public int QuestionFiveAnswer
        {
            get { return questionFiveAnswer; }
            set
            {
                questionFiveAnswer = value;
                OnPropertyChanged();
            }
        }
        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged();
            }
        }
        #endregion


        #region commands
        public RelayCommand SendSurveyCommand { get; set; }

        #endregion

        #region actions
        public void Execute_SendSurveyCommand(object obj)
        {
           
            HospitalSurvey hospitalSurvey = new HospitalSurvey();
            hospitalSurvey.IdVlasnika = PocetnaStranica.getInstance().Pacijent.Jmbg;
            hospitalSurvey.SurveyID = hospitalSurvey.SubmissionDate + hospitalSurvey.IdVlasnika;
            hospitalSurvey.Comment = comment;
            hospitalSurvey.Answers.Add("pitanje1", questionOneAnswer);
            hospitalSurvey.Answers.Add("pitanje2", questionTwoAnswer);
            hospitalSurvey.Answers.Add("pitanje3", QuestionThreeAnswer);
            hospitalSurvey.Answers.Add("pitanje4", questionFourAnswer);
            hospitalSurvey.Answers.Add("pitanje5", questionFiveAnswer);
            hospitalSurvey.NumberOfCheckups = brojZavrsenihPRegleda();
            new HospitalSurveyController().SaveHospitalSurvey(hospitalSurvey);
            PocetnaStranica.getInstance().Anketa.Visibility = Visibility.Collapsed;
            PocetnaStranica.getInstance().frame.Content = new PocetniEkran(PocetnaStranica.getInstance().Pacijent);
        }

        public bool CanExecute_SendSurveyCommand(object obj)
        {
            return true;
        }

        #endregion

        #region helperFunctions
        private int brojZavrsenihPRegleda()
        {
            List<Appointment> zakazaniTermini = new AppointmentFileRepository().GetPatientAppointments(PocetnaStranica.getInstance().Pacijent);
            int brojacZavrsenihPregleda = 0;
            foreach (Appointment termin in zakazaniTermini)
            {
                if (termin.GetIfPast())
                {
                    brojacZavrsenihPregleda++;
                }
            }
            return brojacZavrsenihPregleda;
        }

        #endregion

        #region constructors
        public HospitalSurveyViewModel(){
            questionOneAnswer = 1;
            questionTwoAnswer = 1;
            questionThreeAnswer = 1;
            questionFourAnswer = 1;
            questionFiveAnswer = 1;
            comment = "";
            SendSurveyCommand = new RelayCommand(Execute_SendSurveyCommand, CanExecute_SendSurveyCommand);
        }

        #endregion

    }
}

