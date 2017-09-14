using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using System.Text;


namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            foreach (var i in arr)
            {
                Console.Write(i);
            }
            Console.WriteLine();

            var newarr = YZ.Common.Util.Utils.GetRandomArray(arr);

            foreach (var i in newarr)
            {
                Console.Write(i);
            }
            Console.WriteLine();


            Assert.IsTrue(true);
        }

        [TestMethod]
        public void md5test()
        {

            string f = "yu123456";

            //3a5df20b164e15af29f9a15c844f5d2c 
            //164e15af29f9a15c 

            string s = YZ.Common.Cryptography.CrypMD5.MD5_Enode(f);




            Console.WriteLine();
            Assert.IsTrue(true);
        }
    }
}
