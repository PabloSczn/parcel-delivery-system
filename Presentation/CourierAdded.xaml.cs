using System;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using Business;
using Data;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for CourierAdded.xaml
    /// </summary>
    public partial class CourierAdded : Window
    {
        public CourierAdded()
        {
            InitializeComponent();
        }

        private void Btn_Home_Click(object sender, RoutedEventArgs e)
        {
            //Back home
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
