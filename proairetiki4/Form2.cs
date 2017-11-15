using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proairetiki4
{
    public partial class Form2 : Form
    {
        Random r;
        int score=0;
        int dif;
        int count = 0;
        public Form2(int dif) //pernaw to difficulty sto game
        {
            InitializeComponent();
            this.dif = dif;
            timer1.Interval = timer1.Interval / dif;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            r = new Random();
            timer1.Enabled = true;
            timer2.Enabled = true;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            randomPosition();
        }
        private void randomPosition()
        {
            Point p = new Point(r.Next(0, this.Width - pictureBox1.Width), r.Next(0, this.Height - pictureBox1.Height));
            pictureBox1.Location = p;
        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            timer1.Stop();
            score += dif * 10;
            textBox1.Text = score.ToString();
            randomPosition();
            timer1.Start();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            count++;
            textBox2.Text = (60 - count).ToString() + " seconds left" ;
            if (count == 60)
            {
                timer1.Stop();
                timer2.Stop();
                pictureBox1.Enabled = false;
                pictureBox1.Hide();

            }

        }
    }
}
