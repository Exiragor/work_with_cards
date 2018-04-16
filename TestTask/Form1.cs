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
            int c1r = Card.setCards(richTextBox1.Text, 1000);
            int c2r = Card.setCards(richTextBox2.Text, 3000);
            int c3r = Card.setCards(richTextBox3.Text, 5000);

            label5.Text = (c1r + c2r + c3r).ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int c1r = Card.realizeCards(richTextBox4.Text, 5000);
            int c2r = Card.realizeCards(richTextBox5.Text, 3000);
            int c3r = Card.realizeCards(richTextBox6.Text, 1000);

            label6.Text = (c1r + c2r + c3r).ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string result = "";
            Card[] cards = Card.getCards(false);
            foreach (var card in cards)
            {
                result += card.Value + "-" + card.Code + "\n";
            }

            label11.Text = result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string result = "";
            Card[] cards = Card.getCards(true);
            foreach (var card in cards)
            {
                result += card.Value + "-" + card.Code + "\n";
            }

            label11.Text = result;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
