﻿using SIMS.ViewPatient.ViewModel;
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

namespace SIMS.ViewPatient
{
    /// <summary>
    /// Interaction logic for BeleskaPage.xaml
    /// </summary>
    public partial class BeleskaPage : Page
    {
        NoteViewModel noteViewModel;
        private int _noOfErrorsOnScreen;

        public BeleskaPage(NoteViewModel noteViewModel)
        {
            this.noteViewModel = noteViewModel;
            InitializeComponent();
            this.DataContext = noteViewModel;
            DatePicker.DisplayDateStart = DateTime.Now;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (Radio.IsChecked==true)
            {
                DatePicker.Visibility = Visibility.Visible;
                TimePicker.Visibility = Visibility.Visible;
            }
            else
            {
                DatePicker.Visibility = Visibility.Collapsed;
                TimePicker.Visibility = Visibility.Collapsed;
            }
        }

        private void Save_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                _noOfErrorsOnScreen++;
            else
                _noOfErrorsOnScreen--;

            Potvrdi.IsEnabled = _noOfErrorsOnScreen > 0 ? false : true;

        }
    }
}
