using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    /*
    * Author: Pablo Sanchez Narro 40486559
    * Description of class: Defines parcel class (with properties and methods)
    * Date last modified: 22/11/2021
    */

    public class Parcel
    {
        private string address;
        private string postcode;
        private int courier;
        private string deliveryArea;
        private string id;
        public Parcel()
        { }

        
        //Getters & Setters
        public string Address
        {
            get
            {
                return address;
            }
            set 
            {
                address = value;
            }
        }        
        public string Postcode
        {
            get
            {
                return postcode;
            }
            set 
            {
                postcode = value;
            }
        }        
        public int Courier
        {
            get
            {
                return courier;
            }
            set 
            {
                courier = value;
            }
        }
        public string DeliveryArea
        {
            get
            {
                return deliveryArea;
            }
            set 
            {
                deliveryArea = value;
            }
        }
        public string Id
        {
            get 
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
    }
}
