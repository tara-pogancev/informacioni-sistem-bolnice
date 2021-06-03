using System;
using System.Windows;

namespace SIMS.ViewSecretary
{
    public partial class CustomMessageWindow : Window, IDisposable
    {
        public CustomMessageWindow()
        {
            InitializeComponent();
        }

        public CustomMessageWindow(string message)
        {
            InitializeComponent();

            this.messageText.Text = message;
        }

        public void Dispose()
        {
            this.Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public static class CustomMessageBox
    {
        public static void Show(string message)
        {
            using (CustomMessageWindow form = new CustomMessageWindow(message))
            {
                form.ShowDialog();
            }
        }
    }
}
