using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace TestTask
{
    class XMLDocument
    { 
       public void Generate()
       {
            XDocument xdoc = new XDocument(new XDeclaration("1.0", "windows-1251", ""));

            XElement rootEl = new XElement("payments");
            var pay1 = createPayment(2000, 1000);
            var pay2 = createPayment(6000, 3000);
            var pay3 = createPayment(15000, 5000);

            rootEl.Add(pay1);
            rootEl.Add(pay2);
            rootEl.Add(pay3);

            xdoc.Add(rootEl);

            xdoc.Save("test.xml");
       }  
        
       private static XElement createPayment(float value, int number)
        {
            XElement payment = new XElement("payment");
            XAttribute date = new XAttribute("doc-date", "2018-04-11");
            XElement val = new XElement("value", value);
            XElement num = new XElement("account_number", number);

            payment.Add(date);
            payment.Add(val);
            payment.Add(num);

            return payment;
        }
    }
}
