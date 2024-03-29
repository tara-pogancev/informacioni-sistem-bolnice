﻿
using SIMS.Repositories.SecretaryRepo;
using SIMS.LekarGUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

using System.Windows.Threading;
using SIMS.Controller;

namespace SIMS
{
    public partial class DoctorUI : Window
    {
        public static DoctorUI instance;
        private static Doctor doctorUser;
        private WindowBar bar = new WindowBar();

        private LastLoginController loginController = new LastLoginController();

        public static DoctorUI GetInstance(Doctor doctor)
        {
            if (instance == null)
            {
                doctorUser = doctor;
                instance = new DoctorUI();
            }
            return instance;
        }

        public static DoctorUI GetInstance()
        {
            return instance;
        }

        public DoctorUI()
        {
            InitializeComponent();

            SetStatusBarClock();

            SellectedTab.Content = new DoctorDashboard(doctorUser);
            UsernameLabel.Content = doctorUser.FullName;
            WindowBarFrame.Content = bar;
        }

        private void SetStatusBarClock()
        {
            //Tred za prikazivanje sata i datuma
            this.dateAndTime.Content = DateTime.Now.ToString("HH:mm │ dd.MM.yyyy.");

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                this.dateAndTime.Content = DateTime.Now.ToString("HH:mm │ dd.MM.yyyy.");
            }, this.Dispatcher);
        }

        private void ButtonDashboard(object sender, MouseButtonEventArgs e)
        {
            //Dashboard
            ChangeTab(0);
        }

        private void ButtonAppointments(object sender, RoutedEventArgs e)
        {
            //Button: Termini
            ChangeTab(1);
        }

        private void ButtonPatients(object sender, RoutedEventArgs e)
        {
            //Button: Pacijenti
            ChangeTab(2);
        }

        private void ButtonAppointmentHistory(object sender, RoutedEventArgs e)
        {
            //Button: Istorija pregleda
            ChangeTab(3);
        }

        private void ButtonMaterials(object sender, RoutedEventArgs e)
        {
            //Button: Evidentiranje materijala
            ChangeTab(4);
        }

        private void ButtonAccount(object sender, RoutedEventArgs e)
        {
            //Button: Nalog
            ChangeTab(5);
        }

        private void ButtonNotifications(object sender, MouseButtonEventArgs e)
        {
            //Button: Notifications
            this.ChangeTab(6);
        }

        private void ButtonHelp(object sender, MouseButtonEventArgs e)
        {
            //Button: Help
            this.ChangeTab(7);
        }

        public Doctor GetUser()
        {
            return doctorUser;
        }

        public void ChangeTab(int tabNum)
        {

            SolidColorBrush sellectedTab = new SolidColorBrush(Color.FromRgb(38, 46, 62));

            //Funkcija pomocu enumeracije menja tab
            switch (tabNum)
            {
                case 0:
                    {
                        SellectedTab.Content = new DoctorDashboard(doctorUser);
                        ResetActiveButtons();
                        break;
                    }
                case 1:
                    {
                        SellectedTab.Content = DoctorAppointmentsPage.GetInstance();
                        ResetActiveButtons();
                        B1.Fill = sellectedTab;
                        break;
                    }
                case 2:
                    {
                        SellectedTab.Content = DoctorPatientViewPage.GetInstance();
                        ResetActiveButtons();
                        B2.Fill = sellectedTab;
                        break;
                    }
                case 3:
                    {
                        SellectedTab.Content = new AppointmentHistoryView();
                        ResetActiveButtons();
                        B3.Fill = sellectedTab;
                        break;
                    }
                case 4:
                    {
                        SellectedTab.Content = new DoctorInverntoyPage();
                        ResetActiveButtons();
                        B4.Fill = sellectedTab;
                        break;
                    }
                case 5:
                    {
                        SellectedTab.Content = new DoctorAccountPage();
                        ResetActiveButtons();
                        B5.Fill = sellectedTab;
                        break;
                    }
                case 6:
                    {
                        SellectedTab.Content = new LekarNotificationPage();
                        ResetActiveButtons();
                        break;
                    }
                case 7:
                    {
                        SellectedTab.Content = new LekarHelpPage();
                        ResetActiveButtons();
                        break;
                    }

            }
        }

        public void ChangeWindowMinimize()
        {
            this.WindowState = WindowState.Minimized;
        }

        public void ChangeWindowClose()
        {
            this.Close();
        }

        public void ChangeWindowSize()
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }

        public WindowState GetWindowState()
        {
            return this.WindowState;
        }

        private void ButtonLogOut(object sender, MouseButtonEventArgs e)
        {
            LogOut();
        }

        public void LogOut()
        {
            if (MessageBox.Show("Da li ste sigurni da želite da se odjavite?",
                "Odjava", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                loginController.ClearAll();
                new MainWindow().Show();
                this.Close();
                RemoveAllInstances();
            }
        }

        private static void RemoveAllInstances()
        {
            if (DoctorPatientViewPage.GetInstance() != null)
                DoctorPatientViewPage.GetInstance().RemoveInstance();

            if (DoctorAppointmentsPage.GetInstance() != null)
                DoctorAppointmentsPage.GetInstance().RemoveInstance();

            instance = null;
        }

        private void OnDragMoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void MinimizeWindow(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Normal;

            Application.Current.MainWindow = this;
            Application.Current.MainWindow.Width = 1050;
            Application.Current.MainWindow.Height = 625;

            double height = SystemParameters.WorkArea.Height;
            double width = SystemParameters.WorkArea.Width;
            this.Top = (height - this.Height) / 2;
            this.Left = (width - this.Width) / 2;

            bar.ChangeMinimizeButton();
        }

        private void ResetActiveButtons()
        {
            B1.Fill = new SolidColorBrush(Colors.Transparent);
            B2.Fill = new SolidColorBrush(Colors.Transparent);
            B3.Fill = new SolidColorBrush(Colors.Transparent);
            B4.Fill = new SolidColorBrush(Colors.Transparent);
            B5.Fill = new SolidColorBrush(Colors.Transparent);
        }

        private void WindowKeyListener(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
                ChangeTab(7);
            else if (e.Key == Key.Escape)
                ChangeTab(0);
            else if (e.Key == Key.F2)
                ChangeTab(1);
            else if (e.Key == Key.F3)
                ChangeTab(2);
            else if (e.Key == Key.F4)
                ChangeTab(3);
            else if (e.Key == Key.F5)
                ChangeTab(4);
            else if (e.Key == Key.F6)
                ChangeTab(5);
            else if (e.Key == Key.F7)
                ChangeTab(6);
        }
    }
}
