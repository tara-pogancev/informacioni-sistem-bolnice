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
    /// 

    public class ObavjestenjeWpf
    {
        Obavestenje obavestenje;
        string displayedImage;



        public ObavjestenjeWpf()
        {

        }
        public ObavjestenjeWpf(Obavestenje obv,String src)
        {
            obavestenje = obv;
            displayedImage = src;
        }
        public string DisplayedImage
        {

            get
            {
                
                if (obavestenje.Autor.Equals("Sekretarijat"))
                {
                    return "/src/sekretar.png";
                }
                else
                {
                    return "/src/reports.png";
                }
            }


        }

        public Obavestenje Obavestenje { get => obavestenje; set => obavestenje = value; }
    }
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
            
            obavjestenja.Reverse();
            //obavestenjeView = new ObservableCollection<Obavestenje>(listaObavestenja);
            List<ObavjestenjeWpf> obavjestenjaWpfs = new List<ObavjestenjeWpf>();
            foreach(Obavestenje obavestenje in obavjestenja)
            {
                obavjestenjaWpfs.Add(new ObavjestenjeWpf(obavestenje,""));
            }
            viewerObavestenja.ItemsSource = obavjestenjaWpfs;

            this.DataContext = obavjestenjaWpfs;
        }

        
    }
}
