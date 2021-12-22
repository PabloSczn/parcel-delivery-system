using System;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using Business;
using Data;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for ParcelAdded.xaml
    /// </summary>
    public partial class ParcelAdded : Window
    {
        public ParcelAdded(Courier c, Parcel p)
        {
            //Parcel added
            InitializeComponent();
            ParcelAddedtxt.Content = "Parcel Id:" + p.Id + " assigned to courier with Id:" + c.Id + " of type " + c.Type +"!";
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
