using SIMS.Repositories.PatientRepo;
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

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for ZakazivanjeTermina.xaml
    /// </summary>
    public partial class ZakazivanjeTermina : Page
    {
        private Patient pacijent;
        private static ZakazivanjeTermina instance = null;

        public Patient Pacijent { get => pacijent; set => pacijent = value; }

        public static ZakazivanjeTermina getInstance()
        {
            if (instance == null)
            {
                instance = new ZakazivanjeTermina();
            }
            return instance;
        }
        private ZakazivanjeTermina()
        {            
            InitializeComponent();
            Zakazivanje1.Children.Add(new Zakazivanje(pacijent));
        }

        private void Preporuceni_Click(object sender, RoutedEventArgs e)
        {
            Zakazivanje1.Children.Clear();
            Zakazivanje1.Children.Add(new PreporukaTermina(pacijent));
        }

        private void Obicno_Click(object sender, RoutedEventArgs e)
        {
            Zakazivanje1.Children.Clear();
            Zakazivanje1.Children.Add(new Zakazivanje(pacijent));
        }
    }
}
