using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class Courier
    {
        /*
        * Author: Pablo Sanchez Narro 40486559
        * Description of class: Defines courier class (with properties and methods)
        * Date last modified: 22/11/2021
        */

        private int id;
        private string type;
        private int limitDeliveryArea;
        private int capacity;
        private int parcelsAssigned;
        private List<string> deliveryAreas = new List<string>();
        public Courier(int _id)
        {
            id = _id;
        }


        //Getters & Setters
        public string Type
        {
            get 
            { 
                return type; 
            }
            set
            {
                //Type of courier
                if (value == "Van" || value == "Cycle" || value == "Walking")
                {
                    type = value; 

                    //Characteristics of each type of courier
                    if (value == "Van")
                    {
                        limitDeliveryArea = 22;
                        capacity = 100;
                        parcelsAssigned = 0;
                    }
                    else if (value == "Cycle")
                    {
                        limitDeliveryArea = 1;
                        capacity = 10;
                        parcelsAssigned = 0;
                    }
                    else if (value == "Walking")
                    {
                        limitDeliveryArea = 1;
                        capacity = 5;
                        parcelsAssigned = 0;
                    }
                }
                else
                    throw new ArgumentException("Type not recognized");
            }
        }
        public int Capacity
        {
            get
            {
                return capacity;
            }
            set 
            {
                capacity = value;
            }
        }        
        public int LimitDeliveryArea
        {
            get
            {
                return limitDeliveryArea;
            }
        }        
        public int Id
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
        public int ParcelsAssigned
        {
            get
            {
                return parcelsAssigned;
            }
            set
            {
                parcelsAssigned = value;
            }
        }
        public List<string> DeliveryAreas
        {
            set 
            { 
                deliveryAreas = value; 
            }
            get 
            {
                return deliveryAreas;
            }
        }
    }
}