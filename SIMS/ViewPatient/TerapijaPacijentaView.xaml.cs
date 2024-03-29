﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIMS.PacijentGUI.ViewModel;
using Syncfusion.SfSkinManager;
using Syncfusion.UI.Xaml.Schedule;
using SIMS.PacijentGUI.ViewModel;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for TerapijaPacijentaView.xaml
    /// </summary>
    public partial class TerapijaPacijentaView : Page
    {
        public TerapijaPacijentaView()
        {
            InitializeComponent();
            this.schedule.AllowEditing = false;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("sr-SR");
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("sr-SR");
            //SfSkinManager.SetTheme(schedule, new Theme(""));
            schedule.AppointmentEditorOpening += Schedule_AppointmentEditorOpening;
            this.DataContext =PatientTherapyViewModel.Instance;



        }

        private void Schedule_AppointmentEditorOpening(object sender, AppointmentEditorOpeningEventArgs e)
        {
            //To handle the default appointment editior window by setting the e.Cancel value as true.
            e.Cancel = true;
            if (e.Appointment != null)
            {
                //Display the custom appointment editor window to edit the appointment
            }
            else
            {
                //Display the custom appointment editor window to add new appointment
            }
        }

       
    }
    
}
