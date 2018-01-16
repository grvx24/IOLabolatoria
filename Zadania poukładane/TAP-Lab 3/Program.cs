using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace InzynieriaOprogramowaniaLab3
{
    class Program
    {
        static void zadanie3XML()
        {
            ZadaniaTAP z = new ZadaniaTAP();

            var task = z.Zadanie3("http://www.feedforall.com/sample.xml");
            var xml = task.Result;

            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                xml.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                Console.WriteLine(stringWriter.GetStringBuilder().ToString());
            }
        }

        static void Main(string[] args)
        {
            ZadaniaTAP zad = new ZadaniaTAP();
            /*zadanie1*/
            //var z = zad.Zadanie1();
            //Console.WriteLine(z.I1 + z.I2);
            //Console.WriteLine("Main");

            /*zadanie3*/
            //zadanie3XML();

            zad.Zadanie48();


        }
    }
}
