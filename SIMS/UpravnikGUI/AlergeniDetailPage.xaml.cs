﻿using Model;
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

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for AlergeniDetailPage.xaml
    /// </summary>
    public partial class AlergeniDetailPage : Page
    {
        Alergen alergen;

        public AlergeniDetailPage(string ID) //izmena postojecg alergena
        {
            alergen = AlergeniStorage.Instance.Read(ID);
            InitializeComponent();

            NazivText.Text = alergen.Naziv;
            IDText.Text = alergen.ID;
            IDText.IsEnabled = false;
        }

        public AlergeniDetailPage() //nov alergen
        {
            alergen = new Alergen();
            InitializeComponent();
        }


        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(new AlergeniPage());
            UpravnikWindow.Instance.SetLabel("Alergeni");
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            alergen.Naziv = NazivText.Text;
            alergen.ID = IDText.Text;

            AlergeniStorage.Instance.CreateOrUpdate(alergen);
            UpravnikWindow.Instance.SetContent(new AlergeniPage());
            UpravnikWindow.Instance.SetLabel("Alergeni");
        }
    }
}