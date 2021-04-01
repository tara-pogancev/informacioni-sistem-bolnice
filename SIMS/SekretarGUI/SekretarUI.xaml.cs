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
using System.Windows.Threading;

namespace SIMS
{

    public partial class SekretarUI : Window
    {
        private ObservableCollection<Pacijent> pacijenti;
        private static SekretarUI instance = null;
        private Sekretar sekretar;

        public static SekretarUI GetInstance()
        {
            return instance;
        }

        public static SekretarUI GetInstance(Sekretar s)
        {
            if (instance == null)
                instance = new SekretarUI(s);
            return instance;
        }

        private SekretarUI(Sekretar s)
        {
            InitializeComponent();

            sekretar = s;
            UsernameLabel.Content = sekretar.ImePrezime;

            dateAndTime.Content = DateTime.Now.ToString("HH:mm │ dd/MM/yyyy");
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                dateAndTime.Content = DateTime.Now.ToString("HH:mm │ dd/MM/yyyy");
            }, Dispatcher);

            PacijentStorage storage = new PacijentStorage();
            pacijenti = new ObservableCollection<Pacijent>(storage.ReadList());

            tabelaPacijenata.ItemsSource = pacijenti;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            Dodaj dodaj = new Dodaj();
            dodaj.Show();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (tabelaPacijenata.SelectedItem == null)
            {
                MessageBox.Show("Morate izabrati pacijenta za izmenu.", "Pacijent nije izabran");
            }
            else
            {
                Izmeni izmeni = new Izmeni((Pacijent)tabelaPacijenata.SelectedItem);
                izmeni.Show();
            }
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            if (tabelaPacijenata.SelectedItem == null)
            {
                MessageBox.Show("Morate izabrati pacijenta za brisanje.", "Pacijent nije izabran");
            }
            else
            {
                Pacijent toDelete = (Pacijent)tabelaPacijenata.SelectedItem;
                PacijentStorage.Instance.Delete(toDelete.Jmbg);
                refresh();
            }
        }

        public void refresh()
        {
            pacijenti.Clear();
            List<Pacijent> pacijentiAll = PacijentStorage.Instance.ReadList();
            foreach (Pacijent p in pacijentiAll)
                pacijenti.Add(p);

        }

        private void Button_Log_Out(object sender, RoutedEventArgs e)
        {

            new MainWindow().Show();
            instance = null;
            this.Close();
        }
    }
}
