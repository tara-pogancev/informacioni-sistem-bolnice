﻿using SIMS.Repositories.SecretaryRepo;
using SIMS.LekarGUI.Dialogues.Materijali_i_lekovi;
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
using SIMS.Model;
using SIMS.Controller;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Stranica u glavnom prozoru koja se odnosi na nalog lekara, uz mogucnosti podesavanja profila.
    /// </summary>
    /// 

    public partial class LekarNotificationPage : Page
    {
        private Doctor doctorUser;
        private ObservableCollection<Notification> notificationViewModel;

        private NotificationController notificationController = new NotificationController();

        public LekarNotificationPage()
        {
            InitializeComponent();

            doctorUser = DoctorUI.GetInstance().GetUser();

            InitializeNotificationsList();

            notificationViewer.ItemsSource = notificationViewModel;
        }

        private void InitializeNotificationsList()
        {
            List<Notification> notificationList = notificationController.ReadPastNotificationsByUser(doctorUser.Jmbg);
            notificationList.Reverse();
            notificationViewModel = new ObservableCollection<Notification>(notificationList);
        }

        private void ButtonMedicineApproval(object sender, RoutedEventArgs e)
        {
            new MedicineApproval().Show();
        }

        private void ButtonHome(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(0);
        }

        private void ButtonViewMedicineList(object sender, RoutedEventArgs e)
        {
            new AvailableMedicineView().Show();
        }
    }
}
