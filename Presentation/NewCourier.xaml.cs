using System;
using Business;
using Data;
using System.Windows;
using System.IO;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class NewCourier : Window
    {
        public NewCourier()
        {
            InitializeComponent();
            Btn_Next.IsEnabled = false;

            //Hide all components
            Btn_AddDeliveryArea.Visibility = Visibility.Hidden;
            Capacity_txt.Visibility = Visibility.Hidden;
            DeliveryArea_txt.Visibility = Visibility.Hidden;
            capacity.Visibility = Visibility.Hidden;
            deliveryArea.Visibility = Visibility.Hidden;
            limitTxt.Visibility = Visibility.Hidden;
            //CourierFactory.numberOfCouriers(objectlist.allCouriers.Count);
        }

        //Instance of the lists
        ObjectsList objectlist = ObjectsList.Instance;        
        Courier courier = CourierFactory.CreateCourierFactory();    //Instantiate courier class with CustomerFactory
        int counterOfDevA = 0;
        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            //Back to home screen
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
        private void TypeOfCourier_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            //Assign the type of courier selected from the Combox
            string type = TypeOfCourier.SelectedValue.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", "");
            courier.Type = type;

            //Show caracteristics of the type of courier selected
            capacity.Content = courier.Capacity + " parcels";
            Capacity_txt.Visibility = Visibility.Visible;
            DeliveryArea_txt.Visibility = Visibility.Visible;
            capacity.Visibility = Visibility.Visible;

            //Ask the user for the delivery area
            deliveryArea.Visibility = Visibility.Visible;
            Btn_AddDeliveryArea.Visibility = Visibility.Visible;

            //Limit text
            limitTxt.Content = counterOfDevA + " / " + courier.LimitDeliveryArea + " used";
            limitTxt.Visibility = Visibility.Visible;

        }
        private void Btn_AddDeliveryArea_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int devAreas = Int32.Parse(deliveryArea.Text);

                //Only delivery areas supported         
                if (devAreas > 0 && devAreas < 23)
                {
                    counterOfDevA++;
                    if(counterOfDevA <= courier.LimitDeliveryArea)
                    {
                        //Add delivery areas to the courier
                        string deliveryA = "EH" + deliveryArea.Text;
                        if(Int32.Parse(deliveryArea.Text) > 4 && courier.Type == "Walking") //Walking couriers can only deliver from EH1-EH4
                        {
                            MessageBox.Show("Walking couriers can only deliver from EH1-EH4!");
                            counterOfDevA--;    //Do not count this
                        }
                        else
                        {
                            deliveryArea.Clear();
                            //Suitable delivery area
                            courier.DeliveryAreas.Add(deliveryA);
                            //Enable Next Button
                            Btn_Next.IsEnabled = true;

                            //Limit text
                            limitTxt.Content = counterOfDevA + " / " + courier.LimitDeliveryArea + " used";
                        }

                    }
                    else 
                    {
                        MessageBox.Show("No more delivery areas allowed!");
                        counterOfDevA--;    //Dont include couriers
                        //Limit text
                        limitTxt.Content = counterOfDevA + " / " + courier.LimitDeliveryArea + " used";
                    }

                }
                else
                {
                    //Force the user to input delivery areas supported
                    MessageBox.Show("Only EH1-EH22 supported");
                    //Clear text box
                    deliveryArea.Clear();
                }
            }
            catch
            {
                MessageBox.Show("Hey! that's not a number >:(");
                //Clear text box
                deliveryArea.Clear();
            }
         
        }
        private void Btn_Next_Click(object sender, RoutedEventArgs e)
        {
            //Add courier to the objects list
            objectlist.AddCourier(courier);

            //Log text file//
            //Stored in Presentation/bin folder
            string path = @"../log.txt";
            if (File.Exists(path))  //If the file exists, append
            {
                using (var tw = new StreamWriter(path, true))
                {
                    DateTime now = DateTime.Now;    //Date & Time
                    tw.WriteLine(now + " New Courier Added - id:" + courier.Id + ", type=" + courier.Type);
                    tw.Close();
                }
            }
            else   //If the file doesn't exist, create
            {
                TextWriter tw = new StreamWriter(path, true);
                DateTime now = DateTime.Now;    //Date and time
                tw.WriteLine(now + " New Courier Added - id:" + courier.Id + ", type=" + courier.Type);
                tw.Close();
            }

            //Courier added screen
            CourierAdded ca = new CourierAdded();
            ca.Show();
            this.Close();
        }
    }
}
