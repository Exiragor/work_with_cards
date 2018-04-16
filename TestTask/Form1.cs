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
        }

        private void btn_setCards_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int c1r = Card.SetCards(richTextBox1.Text, 1000);
            int c2r = Card.SetCards(richTextBox2.Text, 3000);
            int c3r = Card.SetCards(richTextBox3.Text, 5000);

            label5.Text = (c1r + c2r + c3r).ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int c1r = Card.RealizeCards(richTextBox4.Text, 5000);
            int c2r = Card.RealizeCards(richTextBox5.Text, 3000);
            int c3r = Card.RealizeCards(richTextBox6.Text, 1000);

            label6.Text = (c1r + c2r + c3r).ToString();
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

            label11.Text = result;
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

            label11.Text = result;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result.ToString() == "OK")
                    button5.Text = dialog.SelectedPath;
            }
        }
    }
}
