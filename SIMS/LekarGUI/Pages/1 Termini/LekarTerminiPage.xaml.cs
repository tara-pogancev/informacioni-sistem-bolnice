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

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Stranica Lekara u okviru glavnog prozora koja daje u uvid sve aktivne termine uz mogucnost editovanja istih
    /// </summary>
    public partial class LekarTerminiPage : Page
    {
        public static LekarTerminiPage instance;

        private static Lekar lekarUser;

        private ObservableCollection<Termin> terminiView;
        public ObservableCollection<Termin> TerminiView { get => terminiView; set => terminiView = value; }

        public static LekarTerminiPage GetInstance(Lekar l)
        {
            if (instance == null)
            {
                lekarUser = l;
                instance = new LekarTerminiPage();
            }
            return instance;
        }

        public static LekarTerminiPage GetInstance()
        {
            return instance;
        }

        public LekarTerminiPage()
        {
            InitializeComponent();

            //Tabela pregleda

            this.DataContext = this;
            terminiView = new ObservableCollection<Termin>(TerminStorage.Instance.ReadList());
            refreshView();
        }

        private void refreshView()
        {
            terminiView.Clear();
            List<Termin> temp = new List<Termin>(TerminStorage.Instance.ReadByDoctor(lekarUser));
            
            foreach (Termin t in temp)
            {
                if (!t.IsPast && !t.Evidentiran)
                   terminiView.Add(t);

                t.Pacijent = new PacijentStorage().Read(t.Pacijent.Jmbg);
                t.Prostorija = new ProstorijaStorage().Read(t.Prostorija.Broj);
            }
        }

        private void Button_Pregled(object sender, RoutedEventArgs e)
        {
            //Button: Zakazi pregled
            TerminCreate terminCreate = new TerminCreate();
            terminCreate.Show();
        }

        private void Button_Operacija(object sender, RoutedEventArgs e)
        {
            //Button: Zakazi operaciju
            OperacijaCreate operacijaCreate = new OperacijaCreate();
            operacijaCreate.Show();
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            //Button: Uredi termin
            if (dataGridTermini.SelectedItem != null)
            {
                TerminUpdate terminUpdate = new TerminUpdate((Termin)dataGridTermini.SelectedItem);
                terminUpdate.Show();
            }

        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            //Button: Otkaži pregled

            if (dataGridTermini.SelectedItem != null)
            {

                if (MessageBox.Show("Da li ste sigurni da želite da otkažete termin?",
                "Otkaži termin", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Termin toDelete = (Termin)dataGridTermini.SelectedItem;
                    TerminStorage.Instance.Delete(toDelete.TerminKey);
                    refreshView();
                    MessageBox.Show("Termin je uspešno otkazan!");
                }

            }
        }

        public void dodajTermin(Termin termin)
        {
            TerminStorage.Instance.Create(termin);
            refreshView();
            MessageBox.Show("Termin uspešno zakazan.");
        }

        public void refresh()
        {
            refreshView();
        }

        public void RemoveInstance()
        {
            instance = null;
        }

        private void Button_Home(object sender, MouseButtonEventArgs e)
        {
            LekarUI.GetInstance().ChangeTab(0);
        }

        private void Event_Karton(object sender, MouseButtonEventArgs e)
        {
            if (dataGridTermini.SelectedItem != null)
            {
                Termin t = (Termin)dataGridTermini.SelectedItem;
                Pacijent p = PacijentStorage.Instance.Read(t.Pacijent.Jmbg);

                LekarUI.GetInstance().SellectedTab.Content = PacijentKartonView.GetInstance(p);
            }
        }
    }
}
