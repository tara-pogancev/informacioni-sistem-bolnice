using Model;
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
using System.Windows.Shapes;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for PocetnaStranica.xaml
    /// </summary>
    public partial class PocetnaStranica : Window
    {
        private Pacijent pacijent;
        public PocetnaStranica(Pacijent p)
        {
            this.pacijent = p;
            InitializeComponent();
        }

        

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;


            switch (index)
            {
                case 0:
                    Tabovi.Content=new PocetniEkran(pacijent);
                    break;
                case 1:
                    Tabovi.Content = new ZakazivanjeTermina(pacijent);
                    break;
                case 2:
                    Tabovi.Content = new MojiTermini(pacijent);
                    break;
                default:
                    break;
            }
        }

        public Pacijent Pacijent { get => pacijent; set => pacijent = value; }
    }
}
