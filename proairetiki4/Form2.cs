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
        Enemy[] Flys = new Enemy[3];
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
        /* name conflict,using class User insted
        DataTable scores = new DataTable();
        */
        public void NewFly()
        {
            //PictureBox Fly = new PictureBox();
            for (int i = 0; i < Flys.Length; ++i)
            {
                this.Flys[i] = new Enemy();
                this.Controls.Add(Flys[i]);
                this.Flys[i].BackColor = System.Drawing.Color.Transparent;
                this.Flys[i].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.Flys[i].Image = global::proairetiki4.Properties.Resources.fly_PNG3947;
                this.Flys[i].Location = new System.Drawing.Point(304, 251);
                this.Flys[i].Name = "pictureBox" + (i + 2).ToString();
                this.Flys[i].Size = new System.Drawing.Size(138, 113);
                this.Flys[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.Flys[i].TabIndex = 0;
                this.Flys[i].TabStop = false;
                //this.Flys[i].Click += new System.EventHandler(this.pictureBox1_Click_1);
                this.Flys[i].MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
                this.Flys[i].MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
                this.Flys[i].MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
                this.Flys[i].Show();
                this.Flys[i].Enabled = true;
                //((System.ComponentModel.ISupportInitialize)(this.Flys[i])).BeginInit();
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
        private void randomPosition(int idofFly=-1)
        {
            if (idofFly == -1) {
                for (int i = 0; i < Flys.Length; ++i)
                {
                    Flys[i].Hide();
                    Flys[i].dx = r.Next(-maxspeed, maxspeed);
                    Flys[i].dy = r.Next(-maxspeed, maxspeed);
                    Point p = new Point(r.Next(0, this.Width - Flys[i].Width), r.Next(0, this.Height - Flys[i].Height));
                    Flys[i].Location = p;
                    Flys[i].Show();
                }
            }
            else
            {
                Flys[idofFly].Hide();
                Flys[idofFly].dx = r.Next(-maxspeed, maxspeed);
                Flys[idofFly].dy = r.Next(-maxspeed, maxspeed);
                Point p = new Point(r.Next(0, this.Width - Flys[idofFly].Width), r.Next(0, this.Height - Flys[idofFly].Height));
                Flys[idofFly].Location = p;
                Flys[idofFly].Show();

            }
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

            Enemy tempFly = (Enemy)sender;
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
                for (int i = 0; i < Flys.Length; ++i)
                {
                    Flys[i].Enabled = false;
                    Flys[i].Hide();
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
        private void timer3_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < Flys.Length; ++i)
            {
                Point p = new Point(Flys[i].Location.X + Flys[i].dx, Flys[i].Location.Y + Flys[i].dy);
                Flys[i].Location = p;
                if ((Flys[i].Location.X + Flys[i].Size.Width / 2 < 0)|| (Flys[i].Location.Y + Flys[i].Size.Height / 2 < 0)|| (Flys[i].Location.Y + Flys[i].Size.Height / 2 < 0)|| (Flys[i].Location.Y + Flys[i].Size.Height / 2 > this.Size.Height)|| (Flys[i].Location.X + Flys[i].Size.Width / 2 > this.Size.Width))
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

        private void button2_Click(object sender, EventArgs e)
        {
            string tempText = textBox1.Text;//gia tin periptosi pou exei pola kena , oxi mono ena
            if (textBox1.Text != null && tempText.Replace(" ", "") != "" && textBox1.Text.Length<=10)//an exei valei o xrhstus username, xreiazetai veltiwsh o elegxos
            {
                List<User> Users;//orise mia lista users
                if (File.Exists("Scores.json"))//an iparxi to arxio
                {
                    
                    using (StreamReader r = new StreamReader("Scores.json"))//energopiise stream yia Read sto Scores.json
                    {
                        string json = r.ReadToEnd();//diabase to arxio
                        Users = JsonConvert.DeserializeObject<List<User>>(json);//metetrepse to periexomeno toy se lista apo User
                        Users.Add(new User(score, time, dif, textBox1.Text));//pros8ese to kenourgio user
                        
                    }
                    string output = JsonConvert.SerializeObject(Users);//metetrepse tin lista se json
                    File.WriteAllText("Scores.json", output);//apo8ikefse to json sto arxio
                }
                else//an den uparxi to arxio , paralipti tin anagnosi tou
                {
                    Users = new List<User>();
                    Users.Add(new User(score,time,dif, textBox1.Text));
                    string output = JsonConvert.SerializeObject(Users, Formatting.Indented);
                    File.WriteAllText("Scores.json", output);
                }
                //https://social.msdn.microsoft.com/Forums/vstudio/en-US/13d6d2ee-2252-4325-a2c3-70412653394f/two-dimensional-arraylist?forum=csharpgeneral
                /* 
                scores.Columns.Add("Nickname", typeof(string));
                scores.Columns.Add("Score", typeof(int));
      
                scores.Rows.Add(new object[] { textBox1.Text.ToString(), score});
                */
                Form3 scoreboard = new Form3();//den pernao tpt gt 8a diabazi to arxio apo thn arxi
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
