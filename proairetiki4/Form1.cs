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
    public partial class Form1 : Form
    {
        int dif = 1; //initialize difficulty

        public Form1()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            dif = 2;
            createForm();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dif = 3;
            createForm();
        }

        private void createForm()
        {
            Form2 game = new Form2(dif, this);
            game.Show();
            game.Closed += (s, args) => this.Show(); //otan kleinei to game emfanizetai h forma1
            this.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dif = 1;
            createForm();
        }
         
        private void button4_Click(object sender, EventArgs e)
        {
            Form3 scoreboard = new Form3();
            scoreboard.Show();
            scoreboard.Closed += (s, args) => this.Show(); //otan kleinei to scoreboard emfanizetai h forma1
            this.Hide();
        }
    }
}
