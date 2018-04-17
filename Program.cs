using DeafX.Belfour.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeafX.Belfour
{
    class Program
    {
        static void Main(string[] args)
        {
            var bglService = new BGLService();

            var payloadStr = bglService.GetBusinessGeneratedLink(new
            {
                email = "test@test.com"
            });

            var customers = ExcelService.ParseCustomers(@"C:\Temp\test.csv");

            foreach(var customer in customers)
            {
                customer.LinkUrl = bglService.GetBusinessGeneratedLink(new
                {
                    email = customer.Email
                });
            }

            ExcelService.WriteCustomers(@"C:\Temp\test2.xlsx", customers);


        }
    }
}
