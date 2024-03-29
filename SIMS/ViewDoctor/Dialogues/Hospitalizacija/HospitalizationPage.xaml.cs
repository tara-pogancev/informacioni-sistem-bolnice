﻿using SIMS.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS.ViewDoctor.Dialogues.Hospitalizacija
{
    /// <summary>
    /// Interaction logic for HospitalizationPage.xaml
    /// </summary>
    public partial class HospitalizationPage : Page
    {
        public HospitalizationPage(Hospitalization hospitalization)
        {
            InitializeComponent();
            HospitalRoomLabel.Content = "Prostorija: " + hospitalization.Room.Number;
        }
    }
}
