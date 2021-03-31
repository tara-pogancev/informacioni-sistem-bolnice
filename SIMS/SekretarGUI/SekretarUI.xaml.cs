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
using System.Windows.Shapes;
using System.ComponentModel;

namespace SIMS
{
    
    public partial class SekretarUI : Window
    {
        private ObservableCollection<Model.Pacijent> pacijenti;
        private static SekretarUI instance = null;

        public static SekretarUI getInstance()
        {
            if (instance == null)
                instance = new SekretarUI();
            return instance;
        }

        private SekretarUI()
        {
            InitializeComponent();
            PacijentStorage storage = new PacijentStorage();
            pacijenti = new ObservableCollection<Model.Pacijent>(storage.ReadList());

            tabelaPacijenata.ItemsSource = pacijenti;
            Closing += new CancelEventHandler(Sekretar_Closing);
        }

        private void Sekretar_Closing(object sender, CancelEventArgs e)
        {
            //new PacijentStorage().Create(new List<Model.Pacijent>(pacijenti));
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            Dodaj dodaj = new Dodaj();
            dodaj.Show();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            Izmeni izmeni = new Izmeni((Model.Pacijent)tabelaPacijenata.SelectedItem);
            izmeni.Show();
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            Pacijent toDelete = (Model.Pacijent)tabelaPacijenata.SelectedItem;
            PacijentStorage.Instance.Delete(toDelete.Jmbg);
            refresh();
        }

        public void refresh()
        {
            pacijenti.Clear();
            List<Pacijent> pacijentiAll = PacijentStorage.Instance.ReadList();
            foreach (Pacijent p in pacijentiAll)
                pacijenti.Add(p);

        }


    }
}
