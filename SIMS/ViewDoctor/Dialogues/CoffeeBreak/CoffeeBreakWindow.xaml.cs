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

namespace SIMS.LekarGUI.Dialogues.CoffeeBreak
{
    /// <summary>
    /// Interaction logic for CoffeeBreakWindow.xaml
    /// </summary>
    public partial class CoffeeBreakWindow : Window
    {
        private int currentPageNumber = 0;

        public CoffeeBreakWindow()
        {
            InitializeComponent();
            LeftArrow.Visibility = Visibility.Hidden;
        }

        private void PreviousPage(object sender, RoutedEventArgs e)
        {
            if (currentPageNumber > 0)
                PageImage.Source = GetPageByNumber(--currentPageNumber);    
            
            if (currentPageNumber == 0)
                LeftArrow.Visibility = Visibility.Hidden;

            if (currentPageNumber == 6)
                RightArrow.Visibility = Visibility.Visible;
        }

        private void NextPage(object sender, RoutedEventArgs e)
        {
            if (currentPageNumber < 7)
                PageImage.Source = GetPageByNumber(++currentPageNumber);

            if (currentPageNumber == 7)
                RightArrow.Visibility = Visibility.Hidden;

            if (currentPageNumber == 1)
                LeftArrow.Visibility = Visibility.Visible;

        }

        private BitmapImage GetPageByNumber(int idx)
        {
            if (idx == 0)
                return new BitmapImage(new Uri(@"../../../src/CoffeeBreak/page0.png", UriKind.Relative));
            else if (idx == 1)
                return new BitmapImage(new Uri(@"../../../src/CoffeeBreak/page1.png", UriKind.Relative));
            else if (idx == 2)
                return new BitmapImage(new Uri(@"../../../src/CoffeeBreak/page2.png", UriKind.Relative));
            else if (idx == 3)
                return new BitmapImage(new Uri(@"../../../src/CoffeeBreak/page3.png", UriKind.Relative));
            else if (idx == 4)
                return new BitmapImage(new Uri(@"../../../src/CoffeeBreak/page4.png", UriKind.Relative));
            else if (idx == 5)
                return new BitmapImage(new Uri(@"../../../src/CoffeeBreak/page5.png", UriKind.Relative));
            else if (idx == 6)
                return new BitmapImage(new Uri(@"../../../src/CoffeeBreak/page6.png", UriKind.Relative));
            else
                return new BitmapImage(new Uri(@"../../../src/CoffeeBreak/page7.png", UriKind.Relative));
        }

        private void LeftArrowOverlay(object sender, MouseEventArgs e)
        {
            LeftArrow.Source = new BitmapImage(new Uri(@"../../../src/CoffeeBreak/arrow_left_overlay.png", UriKind.Relative));
        }

        private void LeftArrowDefault(object sender, MouseEventArgs e)
        {
            LeftArrow.Source = new BitmapImage(new Uri(@"../../../src/CoffeeBreak/arrow_left.png", UriKind.Relative));
        }

        private void RightArrowOverlay(object sender, MouseEventArgs e)
        {
            RightArrow.Source = new BitmapImage(new Uri(@"../../../src/CoffeeBreak/arrow_right_overlay.png", UriKind.Relative));
        }

        private void RightArrowDefault(object sender, MouseEventArgs e)
        {
            RightArrow.Source = new BitmapImage(new Uri(@"../../../src/CoffeeBreak/arrow_right.png", UriKind.Relative));
        }

        private void WindowKeyListener(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
