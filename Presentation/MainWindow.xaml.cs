using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Business;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Data;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Force user to only import data once
        public static bool isCsvPressed = false;
        public MainWindow()
        {
            InitializeComponent();

            //If the user has already imported data, dont allow to do it again
            if(isCsvPressed)
            {
                Btn_ImportData.Visibility = Visibility.Hidden;
            }

        }
        private void Btn_NewCourier_Click(object sender, RoutedEventArgs e)
        {
            //New courier screen
            NewCourier nc = new NewCourier();
            nc.Show();
            this.Close();
        }

        private void Btn_NewParcel_Click(object sender, RoutedEventArgs e)
        {
            //New Parcel screen
            NewParcel np = new NewParcel();
            np.Show();
            this.Close();
        }

        private void Btn_ExistingCouriers_Click(object sender, RoutedEventArgs e)
        {
            //Existing orders screen
            ExistingOrders eo = new ExistingOrders();
            eo.Show();
            this.Close();
        }

        private void Btn_TransferParcels_Click(object sender, RoutedEventArgs e)
        {
            //Transfer parcels screen
            TransferParcels tp = new TransferParcels();
            tp.Show();
            this.Close();
        }

        private void Btn_ImportData_Click(object sender, RoutedEventArgs e)
        {
            CsvClass csv = new CsvClass();
            csv.ImportData();
            //User imported data
            isCsvPressed = true;
            Btn_ImportData.Visibility=Visibility.Hidden;

        }
    }
}
