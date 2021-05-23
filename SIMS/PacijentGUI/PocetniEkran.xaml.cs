using SIMS.Repositories.SecretaryRepo;
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
    /// Interaction logic for PocentaStranica.xaml
    /// </summary>
    
    public partial class PocetniEkran : Page
    {
        private Patient pacijent;
        public PocetniEkran(Patient p)
        {
            this.pacijent = p;
            InitializeComponent();
        }

        public Patient Pacijent { get => pacijent; set => pacijent = value; }

        private void Profil_Click(object sender, RoutedEventArgs e)
        {
            PocetnaStranica.getInstance().frame.Content=new KorisnickiProfil();
        }
    }
}
