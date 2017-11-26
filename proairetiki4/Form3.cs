using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace proairetiki4
{
    public partial class Form3 : Form
    {
        //DataTable score;
        public Form3()
        {
            InitializeComponent();
            
            if (File.Exists("Scores.json"))//an iparxi to arxio
            {
                richTextBox1.Text = "";
                using (StreamReader r = new StreamReader("Scores.json"))
                {
                    string json = r.ReadToEnd();
                    List<User> Users = JsonConvert.DeserializeObject<List<User>>(json).OrderByDescending(o => o.score).ToList();
                    foreach (User user in Users)
                    {
                        richTextBox1.Text += user.name + " score:" + user.score +" dif:"+user.dif+" time:"+user.time+ "\n";
                    }
                }
            }
            else
            {
                richTextBox1.Text = "there is no Score,be the 1st";
            }
            //this.scores = scores; too tired to fix this
        }
    }
}
