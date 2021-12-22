using System;
using Business;
using Data;
using System.Windows;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for ExistingOrders.xaml
    /// </summary>
    public partial class ExistingOrders : Window
    {
        public ExistingOrders()
        {
            InitializeComponent();
            //Instance of the lists
            ObjectsList objectlist = ObjectsList.Instance;
            
            //Display list
            foreach (Courier couriers in objectlist.AllCouriers)    //For each courier in the data layer
            {
                foreach(string dArea in couriers.DeliveryAreas) //For each delivery area of each courier
                {
                    lstOrders.Items.Add(dArea + " Courier " + couriers.Id + " (" + couriers.Type + "): " + couriers.ParcelsAssigned + "/" + couriers.Capacity);
                }
            }
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
