using KinopiletidLappolainen.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace KinopiletidLappolainen
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        StreamWriter to_file;
        Image green = Image.FromFile("Image/green.png");
        Image yellow = Image.FromFile("Image/yellow.png");
        Image red = Image.FromFile("Image/red.png");
        Label[,] _arr = new Label[4, 4];
        Label[] read = new Label[4];
        Button osta;
        Button kinni;
        bool ost = false;
        private void Form2_Load(object sender, EventArgs e)
        {

            string text = "";
            if (!File.Exists("Kino.txt"))
            {
                StreamWriter to_file = new StreamWriter("Kino.txt", false);
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        text += i + "," + j + ",false;";
                    }
                    text += "\n";
                }
                to_file.Write(text);
                to_file.Close();
            }
                StreamReader from_file = new StreamReader("Kino.txt", false);
                string[] arr = from_file.ReadToEnd().Split('\n');
                from_file.Close();
                for (int i = 0; i < 4; i++)
                {
                    read[i] = new Label();
                    read[i].Text = "Rida" + (1 + i);
                    read[i].ForeColor = Color.White;
                    read[i].BackColor = Color.Transparent;
                    read[i].Size = new Size(50, 50);
                    read[i].Location = new Point(200, i * 50 + 110);
                    this.Controls.Add(read[i]);
                    for (int j = 0; j < 4; j++)
                    {
                        _arr[i, j] = new Label();
                        string[] arv = arr[i].Split(';');
                        string[] ardNum = arv[j].Split(',');
                        if(ardNum[2] == "true")
                        {
                            _arr[i, j].Image = red;
                        }
                        else
                        {
                            _arr[i, j].Image = green;
                        }
                        //_arr[i, j].Text = "Koht " + (j + 1); 
                        _arr[i, j].Size = new Size(50, 50);
                        _arr[i, j].Image = green;
                        _arr[i, j].BackColor = Color.Transparent;
                       // _arr[i, j].BorderStyle = BorderStyle.Fixed3D;
                        _arr[i, j].Location = new Point(j * 50 + 300, i * 50 + 100);
                        this.Controls.Add(_arr[i, j]);
                        _arr[i, j].Tag = new int[] { i, j };
                        _arr[i, j].Click += new System.EventHandler(Form2_Click);

                    }
                }
                osta = new Button();
                osta.Text = "Osta";
                osta.Location = new Point(325, 350);
                this.Controls.Add(osta);
                osta.Click += Osta_Click;
                kinni = new Button();
                kinni.Text = "Kinni";
                kinni.Location = new Point(415, 350);
                this.Controls.Add(kinni);
                kinni.Click += Kinni_Click;
                
                
            
        }private void Osta_Click(object sender, EventArgs e)
        {
            Osta_Click_Func();
            ost = true;
            
        }

        private void Osta_Click_Func()
        {
            if(ost == true)
            {
                var vastus = MessageBox.Show("Kas oled kindel?", "Appolo küsib", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (vastus == DialogResult.Yes)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (_arr[i, j].Image == yellow)
                            {
                                _arr[i, j].Image = red;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (_arr[i, j].Image == yellow)
                            {
                                _arr[i, j].Image = green;
                            }
                        }
                    }
                }
            }
        }

        private void Kinni_Click(object sender, EventArgs e)
        {
            string text = " ";
            to_file = new StreamWriter("Kino.txt", false);
            for (int i = 0; i<4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    if(_arr[i,j].Image == yellow)
                    {
                        Osta_Click_Func();
                    }
                }
            }
            for(int i = 0; i< 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    if(_arr[i,j].Image == red)
                    {
                        text += i + "," + j + ",true;";
                    }
                    else
                    {
                        text += i + "," + j + ",false;";
                    }
                }
                text += "\n";
                
            }
            to_file.Write(text);
            to_file.Close();
            this.Close();
        }

        

        private void Form2_Click(object sender, EventArgs e)
        {
            var label = (Label)sender;
            var tag = (int[])label.Tag;
            _arr[tag[0], tag[1]].Text = "Kinni";
           if( _arr[tag[0], tag[1]].Image == red)
            {
                MessageBox.Show("See koht on kinni");
            }
            else
            {
                _arr[tag[0], tag[1]].Image = yellow;
            }
        }
    }
}
