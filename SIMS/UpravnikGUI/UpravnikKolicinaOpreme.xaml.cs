﻿using System;
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
using SIMS.Repositories.SecretaryRepo;

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for UpravnikKolicinaOpreme.xaml
    /// </summary>
    public partial class UpravnikKolicinaOpreme : Page
    {
        UpravnikInventarProstorijePage ParentPage;
        string BrojProstorije;
        Inventory Oprema;

        public UpravnikKolicinaOpreme(UpravnikInventarProstorijePage ParentPage, string BrojProstorije, Inventory Oprema)
        {
            this.ParentPage = ParentPage;
            this.BrojProstorije = BrojProstorije;
            this.Oprema = Oprema;
            InitializeComponent();

            ID.Text = Oprema.ID;
            Naziv.Text = Oprema.Name;
            Tip.ItemsSource = Conversion.GetTipoviOpreme();
            Tip.SelectedItem = Oprema.TypeToString;

            ID.IsEnabled = false;
            Naziv.IsEnabled = false;
            Tip.IsEnabled = false;
            Kolicina.Text = RoomInventoryFileRepository.Instance.Read(BrojProstorije, Oprema.ID).Kolicina.ToString();
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            Kolicina.Text = (int.Parse(Kolicina.Text) + int.Parse(Delta.Text)).ToString();
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            Kolicina.Text = (int.Parse(Kolicina.Text) - int.Parse(Delta.Text)).ToString();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            int amount;
            try
            {
                amount = int.Parse(Kolicina.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Uneti broj.");
                return;
            }

            if (amount < 0)
            {
                MessageBox.Show("Uneti broj veći od 0.");
                return;
            }
            RoomInventoryFileRepository.Instance.Update(new RoomInventory(BrojProstorije, Oprema.ID, amount));

            ParentPage.Update();

            UpravnikWindow.Instance.SetContent(ParentPage);
            UpravnikWindow.Instance.SetLabel("Prostorija " + BrojProstorije);
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(ParentPage);
            UpravnikWindow.Instance.SetLabel("Prostorija " + BrojProstorije);
        }
    }
}
