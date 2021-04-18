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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for Obavjestenja.xaml
    /// </summary>
    public partial class Obavjestenja : UserControl
    {
        private List<Obavestenje> obavjestenja;
        private Pacijent pacijent;
        public Obavjestenja()
        {
            InitializeComponent();
            pacijent = PocetnaStranica.getInstance().Pacijent;
            obavjestenja = new List<Obavestenje>();
            ObavestenjaStorage obavestenjaStorage = new ObavestenjaStorage();
            obavjestenja = obavestenjaStorage.ReadByUser(pacijent.Jmbg);
            this.DataContext = this;
            obavjestenja.Reverse();
            //obavestenjeView = new ObservableCollection<Obavestenje>(listaObavestenja);
            viewerObavestenja.ItemsSource = obavjestenja;
        }
    }
}
