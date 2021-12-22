using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class CourierFactory
    {
        /*
         * Author: Pablo Sanchez Narro 40486559
         * Description of class: Find next available ID and create courier object with that ID
         * Date last modified: 23/11/2021
         * Design pattern: Factory method
         */

        static private int courierCounter = 0;
    
        //Counts number of customers in customer list
        static public void NumberOfCouriers(int courierid)
        {
            courierCounter = courierid;
        }
        
        
        //Increments number of customer in list to set the next customerID
        public static Courier CreateCourierFactory()
        {
            courierCounter++;

            Courier newCourier = new Courier(courierCounter);

            return newCourier;
        }
    }
}
