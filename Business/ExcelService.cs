using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeafX.Belfour.Business
{
    public class ExcelService
    {
        public ExcelService()
        {

        }

        public static void WriteCustomers(string path, Customer[] customers)
        {
            //Creates a blank workbook. Use the using statment, so the package is disposed when we are done.
            using (var p = new ExcelPackage())
            {

                var ws = p.Workbook.Worksheets.Add("Customers");

                //To set values in the spreadsheet use the Cells indexer.
                ws.Cells[1, 1].Value = "MAIL";
                ws.Cells[1, 2].Value = "KUNDNR";
                ws.Cells[1, 3].Value = "URL";

                for (var i = 0; i < customers.Length; i++)
                {
                    ws.Cells[i + 2, 1].Value = customers[i].Email;
                    ws.Cells[i + 2, 2].Value = customers[i].CustomerNumber;
                    ws.Cells[i + 2, 3].Value = customers[i].LinkUrl;
                }

                //Save the new workbook. We haven't specified the filename so use the Save as method.
                p.SaveAs(new FileInfo(path));
            }
        }

        /// <summary>
        /// Parses a CSV file with the following structure:
        /// MAIL;KUNDNR;PERSNR;NAMN;ADRESS;POSTNR;STAD;PERSNR 2;NAMN 2;MOBIL
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Customer[] ParseCustomers(string path)
        {
            var customers = new List<Customer>();

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(stream))
                {
                    for (var i = 0; !reader.EndOfStream; i++)
                    {
                        var line = reader.ReadLine();

                        // First row is the header, skip
                        if (i == 0)
                        {
                            continue;
                        }

                        var values = line.Split(';');

                        customers.Add(new Customer()
                        {
                            Email = values[0],
                            CustomerNumber = values[1]
                        });
                    }
                }
            }

            return customers.ToArray();
        }

    }

    public class Customer
    {
        public string Email { get; set; }

        public string CustomerNumber { get; set; }

        public string LinkUrl { get; set; }
    }
}
