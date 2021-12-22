using System;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using Business;
using Data;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for TransferParcels.xaml
    /// </summary>
    public partial class TransferParcels : Window
    {
        public TransferParcels()
        {
            InitializeComponent();
            ObjectsList objectlist = ObjectsList.Instance;
            //Initialize couriers
            foreach (Courier couriers in objectlist.AllCouriers)
            {
                foreach (Parcel parcels in objectlist.AllParcels)
                {
                    if(parcels.Courier == couriers.Id)
                    {
                        LstCouriers1.Items.Add("Courier " + couriers.Id + " (" + (couriers.ParcelsAssigned) + "/" + couriers.Capacity + ") - Parcel Id:" + parcels.Id);
                    }
                }
            }
            foreach (Courier couriers in objectlist.AllCouriers)
            {
                if (!LstCouriers2.Items.Contains(couriers))
                {
                    LstCouriers2.Items.Add("Courier " + couriers.Id + " (" + (couriers.ParcelsAssigned) + "/" + couriers.Capacity + ")");
                }
            }
        }

        int courier1;
        int courier2;
        string parcelTransfer;
        //Instance of the lists
        ObjectsList objectlist = ObjectsList.Instance;
        public Courier courier01;
        public Courier courier02;
        public Parcel parcel;
        private void Btn_Home_Click(object sender, RoutedEventArgs e)
        {
            //Main window screen
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void LstCouriers1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //Get the id of the courier you'll transfer FROM
            string c1 = ((string)LstCouriers1.SelectedItem).Substring(0,9);
            c1 = c1.Replace("Courier ", "");
            c1 = c1.Trim();
            courier1 = Int32.Parse(c1);
            
            //Get the id of the parcel you'll transfer
            parcelTransfer = ((string)LstCouriers1.SelectedItem).Substring(((string)LstCouriers1.SelectedItem).Length - 3);
        }

        private void LstCouriers2_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //Get the id of the courier you'll transfer TO
            string c2 = ((string)LstCouriers2.SelectedItem).Substring(0, 9);
            c2 = c2.Replace("Courier ", "");
            c2 = c2.Trim();
            courier2 = Int32.Parse(c2);
        }

        private void Btn_Transfer_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                courier01 = objectlist.FindCourier(courier1);  //Find courier FROM with that id
                courier02 = objectlist.FindCourier(courier2);  //Find courier TO with that id
                parcel = objectlist.FindParcel(parcelTransfer); //Find parcel with its id
                
                if (courier01.Id == courier02.Id)
                {
                    MessageBox.Show("Hey! That parcel is already assigned to that courier >:(");
                }
                else if (!courier02.DeliveryAreas.Contains(parcel.DeliveryArea))
                {
                    MessageBox.Show("This courier does not deliver to the parcel's area");
                }
                else
                { 
                    if (courier02.ParcelsAssigned == courier02.Capacity)    //If second couriers capacity is full
                    {
                        MessageBox.Show("This courier cannot take any more parcels!");
                    }
                    else
                    {
                        courier01.ParcelsAssigned = courier01.ParcelsAssigned - 1;  //De-assign one parcel from Courier01
                        parcel.Courier = courier02.Id;  //Change Id of parcel to the second courier
                        courier02.ParcelsAssigned = courier02.ParcelsAssigned + 1; //Assign parcel to Courier02
                        //MessageBox.Show("SUCCESS!! Update info to see the changes!");
                        //Update info (create a new window)
                        TransferParcels tp = new TransferParcels();
                        tp.Show();
                        this.Close();
                    }

                }
            }
            catch
            {
                MessageBox.Show("Select the parcel you want to transfer AND the courier you are transferring to!");
            }

        }
    }
}
