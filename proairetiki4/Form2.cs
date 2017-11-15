using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace proairetiki4
{
    public partial class Form2 : Form
    {
        Random r;
        int score=0;
        int dif;
        int count = 0;
        Form1 mainMenu;
        DataTable scores = new DataTable();

        public Form2(int dif, Form1 mainMenu) //pernaw to difficulty sto game
        {
            InitializeComponent();
            this.dif = dif;
            this.mainMenu = mainMenu;
            timer1.Interval = timer1.Interval / dif;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            r = new Random();
            timer1.Enabled = true;
            timer2.Enabled = true;
            button1.Hide();
            button1.Enabled = false;
            button4.Hide();
            button4.Enabled = false;
            label3.Hide();
            label3.Enabled = false;
            textBox1.Hide();
            textBox1.Enabled = false;
            button2.Hide();
            button2.Enabled = false;
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
            pictureBox1.Hide();
            Point p = new Point(r.Next(0, this.Width - pictureBox1.Width), r.Next(0, this.Height - pictureBox1.Height));
            pictureBox1.Location = p;
            pictureBox1.Show();
        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            timer1.Stop();
            score += dif * 10;
            label1.Text = score.ToString() + " points";
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
            label2.Text = (60 - count).ToString() + " seconds left" ;
            if (count == 60)
            {
                timer1.Stop();
                timer2.Stop();
                pictureBox1.Enabled = false;
                pictureBox1.Hide();
                button1.Enabled = true;
                button1.Show();
                button4.Enabled = true;
                button4.Show();
                label3.Show();
                label3.Enabled = true;
                textBox1.Show();
                textBox1.Enabled = true;
                button2.Enabled = true;
                button2.Show();

            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainMenu.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            //kati paizei me to textbox, to emfanizei transparent
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox1.Text != " " && textBox1.Text.Length<=10)//an exei valei o xrhstus username, xreiazetai veltiwsh o elegxos
            {
                //https://social.msdn.microsoft.com/Forums/vstudio/en-US/13d6d2ee-2252-4325-a2c3-70412653394f/two-dimensional-arraylist?forum=csharpgeneral
                scores.Columns.Add("Nickname", typeof(string));
                scores.Columns.Add("Score", typeof(int));
      
                scores.Rows.Add(new object[] { textBox1.Text.ToString(), score});

                Form3 scoreboard = new Form3(scores);
                scoreboard.Show();
                scoreboard.Closed += (s, args) => mainMenu.Show(); //otan kleinei to scoreboard kleinei emfanizetai h forma1
                this.Hide();
            }
            else
            {
                MessageBox.Show("You need to enter a valid nickname up to 10 characters!");
            }
        }
    }
}
