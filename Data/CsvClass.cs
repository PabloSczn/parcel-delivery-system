using System;
using Business;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Data
{
    public class CsvClass
    {
        /*
        * Author: Pablo Sanchez Narro 40486559
        * Description of class: Methods to import data from the csv file
        * Date last modified: 3/12/2021
        */

        //Instance of courier
        Courier c = new Courier(0);
        //Instance of the lists
        ObjectsList objectlist = ObjectsList.Instance;

        public CsvClass() 
        { }

        //Method to import data
        public void ImportData()
        {
            //csvLines store all the text from the csv file (the csv is located in Presentation/bin)
            string[] csvLines = System.IO.File.ReadAllLines(@"../couriers_parcels.csv");

            //For each line of the csv file
            for(int i = 1; i < csvLines.Length; i++)    //Skipping the header
            {
                ReadLine(csvLines[i]);  //Call read line method
            }

            objectlist.AddCourier(c);
        }

        public void ReadLine(string rowData)
        {
            Parcel parcel = new Parcel();

            //Split that by coma in order to read the data from the csv
            string[] data = rowData.Split(',');

            //Store information of the courier
            c.Id = Int32.Parse(data[1]);
            c.Type = data[0];
            c.ParcelsAssigned = Int32.Parse(data[2]);
            if (!c.DeliveryAreas.Contains(data[4]))
            {
                c.DeliveryAreas.Add(data[4]);
            }

            //Store information of the parcel
            parcel.Id = data[3];
            parcel.DeliveryArea = data[4];
            parcel.Address = data[5];       
            parcel.Postcode = data[4] + " " + data[3];
            parcel.Courier = c.Id;
            //Add parcel to the list
            objectlist.AddParcel(parcel);
        }
    }
}
