using System;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using Business;
using Data;
using System.IO;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for NewParcel.xaml
    /// </summary>
    public partial class NewParcel : Window
    {
        //Instance of the lists
        ObjectsList objectlist = ObjectsList.Instance;
        Parcel parcel = new Parcel();    //Instantiate courier class
        public Courier aCourier;

        int id; //Save id of Courier

        string dArea;   //Store the delivery area
        public NewParcel()
        {
            InitializeComponent();
            Btn_FindCourier.IsEnabled = false;
            Btn_Next.Visibility = Visibility.Hidden;    //Hide next button
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            //Back to home screen
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
        private void AddressTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //Save address
            parcel.Address = AddressTextBox.Text;
            if(parcel.Postcode != null) //Force the user to input address and postocode to find a courier
            {
                Btn_FindCourier.IsEnabled = true;
            }

        }

        //Method to check if the Postocode is valid
        public static bool ValidatePostcode(string postcode)
        {
            return Regex.Match(postcode, "(^gir\\s?0aa$)|(^[a-z-[qvx]](\\d{1,2}|[a-hk-y]\\d{1,2}|\\d[a-hjks-uw]|[a-hk-y]\\d[abehmnprv-y])\\s?\\d[a-z-[cikmov]]{2}$)", RegexOptions.IgnoreCase).Success;
        }

        private void PostcodeTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //If the postcode is valid
            if (ValidatePostcode(PostcodeTextBox.Text) == true)
            {
                //Save postcode, delivery area and its id in parcel
                parcel.Postcode = PostcodeTextBox.Text;
                string parcelId = PostcodeTextBox.Text.Substring(PostcodeTextBox.Text.Length - 3);
                parcel.Id = parcelId;
                dArea = PostcodeTextBox.Text.Replace(parcelId, "");  //Get only the first part of the postcode
                dArea = RemoveWhiteSpaces(dArea);
                parcel.DeliveryArea = dArea;

                if (parcel.Address != null) //Force the user to input address and postocode to find a courier
                {
                    Btn_FindCourier.IsEnabled = true;
                }
            }
            else
            {
                Btn_FindCourier.IsEnabled = false;
            }
        }
        private void InitItemBox(string deliArea)
        {
            //Add couriers to Items box
            foreach (Courier couriers in objectlist.AllCouriers)    //For each courier in the data layer
            {
                string cours = "Id:" + couriers.Id + " " + couriers.Type + " courier  -  Capacity: " + (couriers.Capacity - couriers.ParcelsAssigned);
                if (!LstCourierBox.Items.Contains(cours) && couriers.DeliveryAreas.Contains(deliArea))
                {
                    LstCourierBox.Items.Add(cours);
                }
            }
            if(LstCourierBox.Items.IsEmpty) //If the list is empty
            {
                MessageBox.Show("No couriers available!");
            }
        }

        //Find couriers for the delivery area
        private void Btn_FindCourier_Click(object sender, RoutedEventArgs e)
        {
            LstCourierBox.Items.Clear();
            InitItemBox(RemoveWhiteSpaces(parcel.DeliveryArea));
        }

        //Remove spaces in case the user inputs the postcode with a space
        public static string RemoveWhiteSpaces(string text)
        {
            return Regex.Replace(text, @"\s+", "");
        }
        private void LstCourierBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if the listbox is not empty
            if (LstCourierBox.SelectedItem != null)
            {
                //Get the courier id from the selected courier
                string item = ((string)LstCourierBox.SelectedItem).Substring(0, 4);
                string idofCo = item.Replace("Id:","");
                id = Int32.Parse(idofCo);   //Id saved
                //Show Next Button
                Btn_Next.Visibility = Visibility.Visible;

            }
        }
        private void Btn_Next_Click(object sender, RoutedEventArgs e)
        {
            parcel.Courier = id;    //Assign courier
            aCourier = objectlist.FindCourier(id);  //Find courier with that id
            aCourier.ParcelsAssigned = aCourier.ParcelsAssigned + 1;    //Assign parcel
            if(aCourier.ParcelsAssigned <= aCourier.Capacity)
            {
                //Add parcel to object list
                objectlist.AddParcel(parcel);

                //Log text file//
                //Stored in Presentation/bin folder
                string path = @"../log.txt";
                if (File.Exists(path))  //If the file exists, append
                {
                    using (var tw = new StreamWriter(path, true))
                    {
                        DateTime now = DateTime.Now;    //Date & Time
                        tw.WriteLine(now + " New Parcel Added (" + parcel.Postcode + ", '" + AddressTextBox.Text + "') Allocated to Courier " + aCourier.Id);
                        tw.Close();
                    }
                }
                else   //If the file doesn't exist, create
                {
                    TextWriter tw = new StreamWriter(path, true);
                    DateTime now = DateTime.Now;    //Date and time
                    tw.WriteLine(now + " New Parcel Added (" + parcel.Postcode + ", '" + AddressTextBox.Text + "') Allocated to Courier " + aCourier.Id);
                    tw.Close();
                }



                //Next screen
                ParcelAdded pa = new ParcelAdded(aCourier, parcel);
                pa.Show();
                this.Close();
            }
            //No capacity!
            else
            {
                aCourier.ParcelsAssigned = aCourier.ParcelsAssigned - 1;
                MessageBox.Show("No capacity!");
            }
            
        }
    }
}
