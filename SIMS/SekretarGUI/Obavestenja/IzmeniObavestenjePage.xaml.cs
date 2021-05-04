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

namespace SIMS.SekretarGUI
{
    /// <summary>
    /// Interaction logic for IzmeniObavestenjePage.xaml
    /// </summary>
    public partial class IzmeniObavestenjePage : Page
    {
        private Sekretar sekretar;
        private ObservableCollection<Obavestenje> listaTekstova;
        private Obavestenje obavestenje;
        private ObservableCollection<ObavestenjeUloga> listaUloga;
        private List<Pacijent> listaPacijenata;
        private List<Lekar> listaLekara;
        private List<Sekretar> listaSekretara;
        private List<Upravnik> listaUpravnika;
        public IzmeniObavestenjePage(ObservableCollection<Obavestenje> listaTekstova, Obavestenje obavestenje, Sekretar sekretar)
        {
            InitializeComponent();

            this.sekretar = sekretar;
            this.listaTekstova = listaTekstova;
            this.obavestenje = obavestenje;

            listaPacijenata = PacijentStorage.Instance.ReadList();
            listaLekara = LekarStorage.Instance.ReadList();
            listaSekretara = SekretarStorage.Instance.ReadList();
            listaUpravnika = UpravnikStorage.Instance.ReadList();

            listaUloga = new ObservableCollection<ObavestenjeUloga>();
            listaUloga.Add(new ObavestenjeUloga("Svi", 0));
            listaUloga.Add(new ObavestenjeUloga("Svi pacijenti", 1));
            listaUloga.Add(new ObavestenjeUloga("Svi lekari", 2));
            listaUloga.Add(new ObavestenjeUloga("Svi sekretari", 3));
            listaUloga.Add(new ObavestenjeUloga("Svi upravnici", 4));
            int count = 5;
            foreach (Pacijent p in listaPacijenata)
            {
                listaUloga.Add(new ObavestenjeUloga(p.ImePrezime, count, p.Jmbg));
                count++;
            }
            foreach (Lekar l in listaLekara)
            {
                listaUloga.Add(new ObavestenjeUloga(l.ImePrezime, count, l.Jmbg));
                count++;
            }
            foreach (Sekretar s in listaSekretara)
            {
                listaUloga.Add(new ObavestenjeUloga(s.ImePrezime, count, s.Jmbg));
                count++;
            }
            foreach (Upravnik u in listaUpravnika)
            {
                listaUloga.Add(new ObavestenjeUloga(u.ImePrezime, count, u.Jmbg));
                count++;
            }

            ulogeComboBox.ItemsSource = listaUloga;
            ulogeComboBox.DisplayMemberPath = "Uloga";
            ulogeComboBox.SelectedMemberPath = "Indeks";
            foreach (string key in this.obavestenje.Target) 
            {
                if (key.Equals("All"))
                {
                    ulogeComboBox.SelectedItems.Add(listaUloga[0]);
                    break;
                }
                foreach (ObavestenjeUloga uloga in listaUloga)
                {
                    if (uloga.Indeks < 5)
                        continue;
                    if (uloga.Key.Equals(key))
                    {
                        ulogeComboBox.SelectedItems.Add(uloga);
                    }
                }
            } 

            viewerObavestenja.ItemsSource = this.listaTekstova;
            obavestenjeTextBox.Text = this.obavestenje.Tekst;
        }

        private void Button_Izmeni(object sender, RoutedEventArgs e)
        {
            if (ulogeComboBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Oznacite bar jednu ulogu.", "Nema uloge");
                return;
            }
            List<string> targets = new List<string>();
            foreach (Sekretar s in listaSekretara)
            {
                targets.Add(s.Jmbg);
            }
            foreach (ObavestenjeUloga ou in ulogeComboBox.SelectedItems)
            {
                if (ou.Indeks == 0)
                {
                    targets.Add("All");
                }
                else if (ou.Indeks == 1)
                {
                    foreach (Pacijent p in listaPacijenata)
                    {
                        targets.Add(p.Jmbg);
                    }
                }
                else if (ou.Indeks == 2)
                {
                    foreach (Lekar l in listaLekara)
                    {
                        targets.Add(l.Jmbg);
                    }
                }
                else if (ou.Indeks == 4)
                {
                    foreach (Upravnik u in listaUpravnika)
                    {
                        targets.Add(u.Jmbg);
                    }
                }
                else
                {
                    targets.Add(ou.Key);
                }
            }
            listaTekstova.Remove(obavestenje);
            ObavestenjaStorage.Instance.Delete(obavestenje.ID);

            obavestenje = new Obavestenje("Sekretarijat", DateTime.Now, obavestenjeTextBox.Text.Trim(), targets);
            obavestenje.Tekst = obavestenjeTextBox.Text;
            obavestenje.Vreme = DateTime.Now;
            obavestenje.ID = obavestenje.Autor + obavestenje.Vreme.ToString("yyMMddHHmmss");
            ObavestenjaStorage.Instance.Create(obavestenje);

            this.NavigationService.Navigate(new SekretarObavestenjaPage(sekretar));
        }

        private void Button_Otkazi(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SekretarObavestenjaPage(sekretar));
        }
    }
}
