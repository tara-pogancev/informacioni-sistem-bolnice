﻿using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AllergenRepo;
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
using SIMS.Repositories.MedicationRepo;

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for LekoviDetailPage.xaml
    /// </summary>
    public partial class LekoviDetailPage : Page
    {
        class Sastojak
        {
            public Component Alergen { get; set; }
            public bool IsChecked { get; set; }

            public Sastojak(Component alergen)
            {
                Alergen = alergen;
                IsChecked = false;
            }

            public Sastojak(Component alergen, bool isChecked)
            {
                Alergen = alergen;
                IsChecked = isChecked;
                
            }
        }

        Medication lek;
        ObservableCollection<Sastojak> Sastojci = new ObservableCollection<Sastojak>();

        public LekoviDetailPage(string ID) //izmena postojece prostorije
        {
            lek = MedicationFileRepository.Instance.FindById(ID);
            InitializeComponent();

            IDText.Text = lek.MedicineID;
            NazivText.Text = lek.MedicineName;
            ZamenaText.Text = lek.IDSubstitution;
            ApprovalText.Text = lek.ApprovalStatusString;

            IDText.IsEnabled = false;

            foreach (Component alergen in ComponentFileRepository.Instance.ReadAll().Values)
            {
                Sastojci.Add(new Sastojak(alergen, lek.Components.Contains(alergen)));
            }

            tabelaSastojaka.ItemsSource = Sastojci;
        }

        public LekoviDetailPage() //nov lek
        {
            lek = new Medication();
            InitializeComponent();

            foreach (Component alergen in ComponentFileRepository.Instance.ReadAll().Values)
            {
                Sastojci.Add(new Sastojak(alergen));
            }

            ApprovalText.Text = "Na čekanju";

            tabelaSastojaka.ItemsSource = Sastojci;
        }


        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(new LekoviPage());
            UpravnikWindow.Instance.SetLabel("Lekovi");
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            lek.MedicineID = IDText.Text;
            lek.MedicineName = NazivText.Text;
            lek.IDSubstitution = ZamenaText.Text;
            lek.Components.Clear();
            foreach(Sastojak sastojak in Sastojci)
            {
                if (sastojak.IsChecked)
                {
                    lek.Components.Add(sastojak.Alergen);
                }
            }

            lek.ApprovalStatus = MedicineApprovalStatus.Waiting;

            MedicationFileRepository.Instance.CreateOrUpdate(lek);
            UpravnikWindow.Instance.SetContent(new LekoviPage());
            UpravnikWindow.Instance.SetLabel("Lekovi");
        }

        private void IDText_KeyUp(object sender, KeyEventArgs e)
        {
            foreach(Medication lek in MedicationFileRepository.Instance.ReadAll().Values)
            {
                if (lek.IDSubstitution == IDText.Text)
                {
                    ZamenaText.Text = lek.MedicineID;
                    return;
                }
                else
                {
                    ZamenaText.Text = "";
                }
            }
        }
    }
}

