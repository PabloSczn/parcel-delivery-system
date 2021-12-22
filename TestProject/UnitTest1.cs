using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Business;
using Data;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        Courier testCourier = new Courier(-1);
        Parcel testParcel = new Parcel();
        //Instance of the lists
        ObjectsList objectlist = ObjectsList.Instance;

        //Test courier type
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCourierType()
        {
            testCourier.Type = "Lorry";
        }

        //Test 'find courier'
        [TestMethod]
        public void TestObjectListCourier()
        { 
            Assert.AreEqual(objectlist.FindCourier(-100), null);
        }        
        
        //Test 'find parcel'
        [TestMethod]
        public void TestObjectListParcel()
        { 
            Assert.AreEqual(objectlist.FindParcel("EH48"), null);
        }
    }
}