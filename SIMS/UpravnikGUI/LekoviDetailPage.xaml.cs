using Model;
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

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for LekoviDetailPage.xaml
    /// </summary>
    public partial class LekoviDetailPage : Page
    {
        class Sastojak
        {
            public Alergen Alergen { get; set; }
            public bool IsChecked { get; set; }

            public Sastojak(Alergen alergen)
            {
                Alergen = alergen;
                IsChecked = false;
            }

            public Sastojak(Alergen alergen, bool isChecked)
            {
                Alergen = alergen;
                IsChecked = isChecked;
                
            }
        }

        Lek lek;
        ObservableCollection<Sastojak> Sastojci = new ObservableCollection<Sastojak>();

        public LekoviDetailPage(string ID) //izmena postojece prostorije
        {
            lek = LekStorage.Instance.Read(ID);
            InitializeComponent();

            IDText.Text = lek.MedicineID;
            NazivText.Text = lek.MedicineName;

            IDText.IsEnabled = false;

            foreach (Alergen alergen in AlergeniStorage.Instance.ReadAll().Values)
            {
                Sastojci.Add(new Sastojak(alergen, lek.Components.Contains(alergen.ID)));
            }

            tabelaSastojaka.ItemsSource = Sastojci;
        }

        public LekoviDetailPage() //nov lek
        {
            lek = new Lek();
            InitializeComponent();

            foreach (Alergen alergen in AlergeniStorage.Instance.ReadAll().Values)
            {
                Sastojci.Add(new Sastojak(alergen));
            }
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

            lek.Components.Clear();
            foreach(Sastojak sastojak in Sastojci)
            {
                if (sastojak.IsChecked)
                {
                    lek.Components.Add(sastojak.Alergen.ID);
                }
            }

            LekStorage.Instance.CreateOrUpdate(lek);
            UpravnikWindow.Instance.SetContent(new LekoviPage());
            UpravnikWindow.Instance.SetLabel("Lekovi");
        }
    }
}

