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
using Model;

namespace SIMS.LekarGUI.Dialogues.Materijali_i_lekovi
{
    /// <summary>
    /// Interaction logic for MedicineDenialWriteMessage.xaml
    /// </summary>
    public partial class MedicineDenialWriteMessage : Window
    {
        public MedicineDenialWriteMessage()
        {
            InitializeComponent();
        }

        private void CancelMessage(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da otkažete pisanje poruke?", "Otkaži pisanje?",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void AcceptMessage(object sender, RoutedEventArgs e)
        {
            String notificationAuthor = LekarUI.GetInstance().GetUser().ImePrezime;
            String notificationText = NotificationTextBox.Text;
            List<String> notificationTarget = getUpravnikKeys();

            Notification notification = new Notification(notificationAuthor, DateTime.Now, notificationText, notificationTarget);
            NotificationRepository.Instance.CreateEntity(notification);

            this.Close();
            MessageBox.Show("Poruka uspešno poslata!");
            
        }

        private List<String> getUpravnikKeys()
        {
            List<String> retVal = new List<String>();

            foreach(Manager upravnik in ManagerRepository.Instance.ReadEntities())
            {
                retVal.Add(upravnik.Jmbg);
            }

            return retVal;
        } 
    }
}
