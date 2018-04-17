using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var now = DateTime.Now;
            dateTimePicker1.Value = new DateTime(now.Year, now.Month, now.Day, 0, 0, 1);
            dateTimePicker2.Value = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            dateTimePicker3.Value = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
        }

        private void btn_setCards_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Count c1r = Card.SetCards(richTextBox1.Text, 1000);
            Count c2r = Card.SetCards(richTextBox2.Text, 3000);
            Count c3r = Card.SetCards(richTextBox3.Text, 5000);

            int successed = c1r.Successed + c2r.Successed + c3r.Successed;
            int failed = c1r.Failed + c2r.Failed + c3r.Failed;

            label5.Text = "Успешно выполнено: " + successed + "\n";
            label5.Text += "Невыполнено из-за ошибки: " + failed;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Count c1r = Card.RealizeCards(richTextBox4.Text, 5000);
            Count c2r = Card.RealizeCards(richTextBox5.Text, 3000);
            Count c3r = Card.RealizeCards(richTextBox6.Text, 1000);

            int successed = c1r.Successed + c2r.Successed + c3r.Successed;
            int failed = c1r.Failed + c2r.Failed + c3r.Failed;

            label6.Text = "Успешно выполнено: " + successed + "\n";
            label6.Text += "Невыполнено из-за ошибки: " + failed;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string result = "";
            Timestamp time = new Timestamp();
            time.SetUnix(dateTimePicker1.Value.ToString());
            var intervalFrom = time.Unix;
            time.SetUnix(dateTimePicker2.Value.ToString());
            var intervalTo = time.Unix;

            Card[] cards = Card.GetCards(false, intervalFrom, intervalTo);
            foreach (var card in cards)
            {
                result += card.Value + "-" + card.Code + "\n";
            }

            richTextBox7.Text = result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string result = "";
            Timestamp time = new Timestamp();
            time.SetUnix(dateTimePicker1.Value.ToString());
            var intervalFrom = time.Unix;
            time.SetUnix(dateTimePicker2.Value.ToString());
            var intervalTo = time.Unix;
            Card[] cards = Card.GetCards(true, intervalFrom, intervalTo);
            foreach (var card in cards)
            {
                result += card.Value + "-" + card.Code + "\n";
            }

            richTextBox7.Text = result;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            XMLDocument xml = new XMLDocument();
            xml.SetPath();

            var date = dateTimePicker3.Value.ToString("yyyy-MM-dd");
            xml.DocDate = date;

            Card[] cards = Card.GetCards(true, date);
            int[] counts = Card.GetCountCards(cards);
            xml.Generate(counts[0], counts[1], counts[2]);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
