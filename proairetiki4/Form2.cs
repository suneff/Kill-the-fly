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
using System.IO;
using Newtonsoft.Json;
using System.Media;
namespace proairetiki4
{

    public partial class Form2 : Form
    {
        Enemy[] Flies = new Enemy[3];
        Random r;
        
        int time=60;
        int score=0;
        int dif=1;
        int count = 0;
        int maxspeed;
        Form1 mainMenu;
        SoundPlayer sndfly;
        SoundPlayer snd2;
        SoundPlayer snd;
        Cursor cur1,cur2;

        public void NewFly()
        {

            for (int i = 0; i < Flies.Length; ++i) //ftiaxnoume kainourio imagebox
            {
                this.Flies[i] = new Enemy();
                this.Controls.Add(Flies[i]);
                this.Flies[i].BackColor = System.Drawing.Color.Transparent;
                this.Flies[i].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.Flies[i].Image = global::proairetiki4.Properties.Resources.fly_PNG3947;
                this.Flies[i].Location = new System.Drawing.Point(304, 251);
                this.Flies[i].Name = "pictureBox" + (i + 2).ToString();
                this.Flies[i].Size = new System.Drawing.Size(138, 113);
                this.Flies[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.Flies[i].TabIndex = 0;
                this.Flies[i].TabStop = false;
                this.Flies[i].MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
                this.Flies[i].MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
                this.Flies[i].MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
                this.Flies[i].Show();
                this.Flies[i].Enabled = true;
            }

        }
        public Form2(int dif, Form1 mainMenu) //pernaw to difficulty sto game
        {
            InitializeComponent();
            this.dif = dif;
            this.mainMenu = mainMenu;
            timer1.Interval = timer1.Interval / dif;
            maxspeed = 10 * dif;
            NewFly();
            cur1  = new Cursor(Properties.Resources.migoskotostra101.Handle);
            cur2 = new Cursor(Properties.Resources.skotostra2.Handle);
            this.Cursor = cur1;
            sndfly = new SoundPlayer(Properties.Resources.Fly_sound_10);
            snd2 = new SoundPlayer(Properties.Resources.Squish_Sound_Effects_3);
            snd = new SoundPlayer(Properties.Resources.Quack_Sound_Effect);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            r = new Random();
            timer1.Enabled = true;
            timer2.Enabled = true;
            timer3.Enabled = true;
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
        private void timer1_Tick(object sender, EventArgs e)
        {
            randomPosition(); 
            sndfly.Play();
        }
        private void randomPosition(int idofFly=-1) //vazei th myga se mia random thesi
        {
            if (idofFly == -1) {
                for (int i = 0; i < Flies.Length; ++i)
                {
                    Flies[i].Hide();
                    Flies[i].dx = r.Next(-maxspeed, maxspeed);
                    Flies[i].dy = r.Next(-maxspeed, maxspeed);
                    Point p = new Point(r.Next(0, this.Width - Flies[i].Width), r.Next(0, this.Height - Flies[i].Height));
                    Flies[i].Location = p;
                    Flies[i].Show();
                }
            }
            else
            {
                Flies[idofFly].Hide();
                Flies[idofFly].dx = r.Next(-maxspeed, maxspeed);
                Flies[idofFly].dy = r.Next(-maxspeed, maxspeed);
                Point p = new Point(r.Next(0, this.Width - Flies[idofFly].Width), r.Next(0, this.Height - Flies[idofFly].Height));
                Flies[idofFly].Location = p;
                Flies[idofFly].Show();

            }
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

            Enemy tempFly = (Enemy)sender; //parsing object to enemy
            timer1.Stop();
            score += dif * 10;
            label1.Text = score.ToString() + " points";
            randomPosition(int.Parse(tempFly.Name.Substring(10))-2);
            timer1.Start();
            snd2.Play();
        }
        private void pictureBox1_MouseDown(object sender, EventArgs e)
        {
            this.Cursor = cur2;
        }
        private void pictureBox1_MouseUp(object sender, EventArgs e)
        {
            this.Cursor = cur1;
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            count++;
            label2.Text = (time - count).ToString() + " seconds left" ;
            if (count == time)
            {
                timer1.Stop();
                timer2.Stop();
                for (int i = 0; i < Flies.Length; ++i)
                {
                    Flies[i].Enabled = false;
                    Flies[i].Hide();
                }
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
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainMenu.Show();
        }
        private void timer3_Tick(object sender, EventArgs e) //metakinei kathe myga kata dx, dy
        {
            for (int i = 0; i < Flies.Length; ++i)
            {
                Point p = new Point(Flies[i].Location.X + Flies[i].dx, Flies[i].Location.Y + Flies[i].dy);
                Flies[i].Location = p;
                if ((Flies[i].Location.X + Flies[i].Size.Width / 2 < 0)|| (Flies[i].Location.Y + Flies[i].Size.Height / 2 < 0)|| (Flies[i].Location.Y + Flies[i].Size.Height / 2 < 0)|| (Flies[i].Location.Y + Flies[i].Size.Height / 2 > this.Size.Height)|| (Flies[i].Location.X + Flies[i].Size.Width / 2 > this.Size.Width))
                {
                    randomPosition(i);
                }
            }
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            this.Cursor = cur2;
            snd.Play();
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = cur1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 scoreboard = new Form3();
            scoreboard.Show();
            scoreboard.Closed += (s, args) => mainMenu.Show(); //otan kleinei to scoreboard emfanizetai h forma1
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tempText = textBox1.Text; //gia tin periptosi pou exei polla kena , oxi mono ena
            if (textBox1.Text != null && tempText.Replace(" ", "") != "" && textBox1.Text.Length<=10)//an exei valei o xrhsths username, xreiazetai veltiwsh o elegxos
            {
                List<User> Users;//orise mia lista users
                if (File.Exists("Scores.json"))//an iparxi to arxeio
                {
                    
                    using (StreamReader r = new StreamReader("Scores.json"))//energopoihse stream yia Read sto Scores.json
                    {
                        string json = r.ReadToEnd();//diabase to arxio
                        Users = JsonConvert.DeserializeObject<List<User>>(json);//metetrepse to periexomeno toy se lista apo User
                        Users.Add(new User(score, time, dif, textBox1.Text));//prosthese to kainourgio user
                        
                    }
                    string output = JsonConvert.SerializeObject(Users);//metetrepse tin lista se json
                    File.WriteAllText("Scores.json", output);//apothikeuse to json sto arxeio
                }
                else //an den uparxi to arxeio , paraliptei tin anagnosi tou
                {
                    Users = new List<User>();
                    Users.Add(new User(score,time,dif, textBox1.Text));
                    string output = JsonConvert.SerializeObject(Users, Formatting.Indented);
                    File.WriteAllText("Scores.json", output);
                }

                Form3 scoreboard = new Form3();
                scoreboard.Show();
                scoreboard.Closed += (s, args) => mainMenu.Show(); //otan kleinei to scoreboard emfanizetai h forma1
                this.Hide();
            }
            else
            {
                MessageBox.Show("You need to enter a valid nickname up to 10 characters!");
            }
        }
    }
    public class User 
    {
        public int score { get; set; }
        public int time { get; set; }
        public int dif { get; set; }
        public string name { get; set; }
        public User(int score, int time, int dif, string name)
        {
            this.name = name;
            this.score = score;
            this.dif = dif;
            this.time = time;
        }
    }
    public partial class Enemy : PictureBox
    {
        public int dx = 0, dy = 0;
    }
}
