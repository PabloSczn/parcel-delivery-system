using System;
using Business;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class ObjectsList
    {
        /*
        * Author: Pablo Sanchez Narro
        * Description of class: Methods to read and save objects to lists
        * Date last modified: 28/11/2021
        * Dessign pattern: Singleton
        */
        
        private List<Courier> courierList = new List<Courier>();
        private List<Parcel> parcelList = new List<Parcel>();

        //Singleton 
        private static ObjectsList instance;


        //Singleton constructor
        private ObjectsList() 
        { }
        public static ObjectsList Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ObjectsList();
                }
                return instance;
            }
        }

        //Add courier
        public void AddCourier(Courier courier)
        {
            courierList.Add(courier);
        }


        //Add parcel
        public void AddParcel(Parcel parcel)
        {
            parcelList.Add(parcel);
        }

        //Find Courier by id
        public Courier FindCourier(int id)
        {
            foreach (Courier c in courierList)
            {
                if (id == c.Id)
                {
                    return c;
                }
            }
            return null;
        }

        //Find Parcel by id
        public Parcel FindParcel(string idParcel)
        {
            foreach (Parcel p in parcelList)
            {
                if (idParcel == p.Id)
                {
                    return p;
                }
            }
            return null;
        }


        //Return complete lists
        public List<Courier> AllCouriers
        {
            get 
            { 
                return courierList; 
            }
            set 
            { 
                courierList = value; 
            }
        }
        public List<Parcel> AllParcels
        {
            get 
            { 
                return parcelList; 
            }
            set 
            {
                parcelList = value; 
            }
        }

    }
}
