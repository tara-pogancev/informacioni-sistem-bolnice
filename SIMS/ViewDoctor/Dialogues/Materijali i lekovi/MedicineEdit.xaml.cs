﻿using System;
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
using System.Windows.Shapes;
using SIMS.Repositories.SecretaryRepo;
using SIMS.DTO;
using SIMS.Model;
using SIMS.Repositories.AllergenRepo;
using SIMS.Controller;

namespace SIMS.LekarGUI.Dialogues.Materijali_i_lekovi
{
    /// <summary>
    /// Interaction logic for MedicineEdit.xaml
    /// </summary>
    public partial class MedicineEdit : Window
    {
        private Medication medicine;

        public ObservableCollection<Component> NewComponentsView { get; set; }
        public ObservableCollection<Component> CurrentComponentsView { get; set; }
        public List<Medication> MedicineSubstitutionList { get; set; }

        private MedicineController medicineController = new MedicineController();
        private AllergenController allergenController = new AllergenController();

        public MedicineEdit(Medication medicinePar)
        {
            medicine = medicineController.GetMedicine(medicinePar.MedicineID);

            InitializeComponent();
            DataContext = this;
            AddButton.Visibility = Visibility.Hidden;

            MedicineNameLabel.Content = "Izmena: " + medicine.MedicineName;
            CurrentSubstitute.Content = "Trenutna zamena: " + GetSubstituteName(medicine);

            NewComponentsView = new ObservableCollection<Component>();
            CurrentComponentsView = new ObservableCollection<Component>();
            MedicineSubstitutionList = new List<Medication>(medicineController.GetApprovedMedicine());

            RefreshView();

        }

        private String GetSubstituteName(Medication medicine)
        {
            return medicineController.GetMedicine(medicine.IDSubstitution).MedicineName;
        }

        public void RefreshView()
        {
            NewComponentsView.Clear();
            CurrentComponentsView.Clear();

            List<Component> allAllergens = new List<Component>(allergenController.GetAll());
            foreach (Component viewItem in allAllergens)
            {
                if (medicine.IncludesAllergen(viewItem))
                    CurrentComponentsView.Add(viewItem);
                else NewComponentsView.Add(viewItem);
            }

        }

        public List<Component> GetSelectedComponents()
        {
            if (TabbedPanel.SelectedIndex == 0)
                return GetAddedComponents();
            else
                return GetRemovedComponents();
        }

        private List<Component> GetAddedComponents()
        {
            List<Component> selectedItems = new List<Component>();

            foreach (Component currentAlergen in DataGridAddNew.SelectedItems)
            {
                Component data = currentAlergen as Component;
                selectedItems.Add(data);
            }

            return selectedItems;
        }

        private List<Component> GetRemovedComponents()
        {
            List<Component> selectedItems = new List<Component>();

            foreach (Component currentAlergen in DataGridComponents.SelectedItems)
            {
                Component data = currentAlergen as Component;
                selectedItems.Add(data);
            }

            return selectedItems;
        }


        private void ButtonEditMedicine(object sender, RoutedEventArgs e)
        {
            DoApplyChanges();
        }

        private void DoApplyChanges()
        {
            SetSubstituteMedicine();
            medicineController.UpdateMedicine(medicine);

            this.Close();
            MessageBox.Show("Lek uspešno izmenjen!");
        }

        private void ButtonCloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RemoveElements(object sender, RoutedEventArgs e)
        {
            foreach (Component component in GetSelectedComponents())
                medicine.RemoveComponent(component);

            RefreshView();
        }

        private void SetSubstituteMedicine()
        {
            if (SubstitutionMedicine.SelectedItem != null)
            {
                Medication SellectedSubstitutionMedicine = (Medication)SubstitutionMedicine.SelectedItem;
                medicine.IDSubstitution = SellectedSubstitutionMedicine.MedicineID;
            }
        }

        private void AddElements(object sender, RoutedEventArgs e)
        {
            foreach (Component component in GetSelectedComponents())
                medicine.Components.Add(allergenController.GetAllergen(component.ID));

            RefreshView();
        }

        private void TabChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (TabbedPanel.SelectedIndex == 0)
                {
                    //Switched to first
                    RemoveButton.Visibility = Visibility.Hidden;
                    AddButton.Visibility = Visibility.Visible;
                }
                else if (TabbedPanel.SelectedIndex == 1)
                {
                    //Switched to second
                    AddButton.Visibility = Visibility.Hidden;
                    RemoveButton.Visibility = Visibility.Visible;
                }
                else
                {
                    AddButton.Visibility = Visibility.Hidden;
                    RemoveButton.Visibility = Visibility.Hidden;
                }

            }
        }

        private void WindowKeyListener(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
            else if (e.Key == Key.Space)
                DoApplyChanges();
        }
    }
}
