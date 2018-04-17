using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace TestTask
{
    class XMLDocument
    {
        private string docDate;
        private string path;

        public string DocDate
        {
            get { return docDate; }
            set { docDate = value; }
        }

        public void Generate(int k1, int k3, int k5)
        {
            XDocument xdoc = new XDocument(new XDeclaration("1.0", "windows-1251", ""));

            XElement rootEl = new XElement("payments");
            var pay1 = createPayment(1000 * k1, 1000);
            var pay2 = createPayment(3000 * k3, 3000);
            var pay3 = createPayment(5000 * k5, 5000);

            rootEl.Add(pay1);
            rootEl.Add(pay2);
            rootEl.Add(pay3);

            xdoc.Add(rootEl);

            var destinationFile = System.IO.Path.Combine(path, "test.xml");
            xdoc.Save(destinationFile);

            string message = "test.xml успешно выгружен";
            string caption = "XML";
            MessageBox.Show(message, caption);
        }  
        
        private XElement createPayment(float value, int number)
        {
            XElement payment = new XElement("payment");
            XAttribute date = new XAttribute("doc-date", docDate);
            XElement val = new XElement("value", value);
            XElement num = new XElement("account_number", number);

            payment.Add(date);
            payment.Add(val);
            payment.Add(num);

            return payment;
        }

        public void SetPath()
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result.ToString() == "OK")
                    path = dialog.SelectedPath;
                else
                    path = "";
            }
        }
    }
}
