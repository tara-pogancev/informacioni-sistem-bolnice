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
    /// Interaction logic for SekretarObavestenjaPage.xaml
    /// </summary>
    public partial class SekretarObavestenjaPage : Page
    {
        private Sekretar sekretar;
        private ObservableCollection<Obavestenje> listaTekstova;
        private ObservableCollection<ObavestenjeUloga> listaUloga;
        private List<Pacijent> listaPacijenata;
        private List<Lekar> listaLekara;
        private List<Sekretar> listaSekretara;
        private List<Upravnik> listaUpravnika;

        public SekretarObavestenjaPage(Sekretar sekretar)
        {
            InitializeComponent();
            this.sekretar = sekretar;
            List<Obavestenje> listaObavestenja = ObavestenjaStorage.Instance.ReadByUser(sekretar.Jmbg);
            listaObavestenja.Reverse();
            listaTekstova = new ObservableCollection<Obavestenje>(listaObavestenja);
            listaPacijenata = PacijentStorage.Instance.ReadList();
            listaLekara = LekarStorage.Instance.ReadList();
            listaSekretara = SekretarStorage.Instance.ReadList();
            listaUpravnika = UpravnikStorage.Instance.ReadList();

            viewerObavestenja.ItemsSource = listaTekstova;

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
            ulogeComboBox.SelectedItems.Add(listaUloga[0]);
        }

        private void Button_Dodaj(object sender, RoutedEventArgs e)
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

            Obavestenje obavestenje = new Obavestenje("Sekretarijat", DateTime.Now, obavestenjeTextBox.Text.Trim(), targets);
            ObavestenjaStorage.Instance.Create(obavestenje);

            listaTekstova.Insert(0, obavestenje);
        }

        private void Button_Obrisi(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Obavestenje zaBrisanje = null;
            foreach (Obavestenje o in listaTekstova)
            {
                if (o.ID.Equals(button.CommandParameter))
                {
                    zaBrisanje = o;
                }
            }
            listaTekstova.Remove(zaBrisanje);
            ObavestenjaStorage.Instance.Delete(zaBrisanje.ID);

        }

        private void Button_Izmeni(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Obavestenje zaIzmenu = null;
            foreach (Obavestenje o in listaTekstova)
            {
                if (o.ID.Equals(button.CommandParameter))
                {
                    zaIzmenu = o;
                }
            }
            this.NavigationService.Navigate(new IzmeniObavestenjePage(listaTekstova, zaIzmenu, sekretar));
        }
    }
}
