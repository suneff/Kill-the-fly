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
        bool flag = true; //if game is open

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
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
            Form2 game = new Form2(dif);
            game.Show();
            //this.Hide();
           
        //disable th forma 1 etsi wste na mi mporei na anoiksei 
        //polles fores to game patwntas ta koumpia.
    }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dif = 1;
            createForm();
        }
    }
}
