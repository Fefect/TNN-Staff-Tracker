using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameMeAPI
{
    public partial class MainFrame : Form
    {
        public MainFrame(ArrayList players)
        {
            InitializeComponent();
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            comboBox1.DropDownHeight = players.Count * 100;
            foreach (User used in players)
            {
                comboBox1.Items.Add(used);
            } 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            User currentUser = (User) comboBox1.SelectedItem;
            label1.Text = "Username: "+ currentUser.name;
            label2.Text = "Playtime: " + currentUser.time;
            label3.Text = "Activity: " + currentUser.activity;
            label4.Text = "Country: " + currentUser.country;
        }

        private void MainFrame_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(new ThreadStart(BrowserThread));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
        }

        private void BrowserThread()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string strFileName = openFileDialog1.FileName;
                textBox1.Invoke(new Action(() => textBox1.Text = strFileName));
                textBox1.Text = strFileName;
                StreamReader reader = new StreamReader(strFileName);
                var containsInfo = File.ReadAllLines(strFileName);
                var dataList = new ArrayList();
                foreach (var line in containsInfo)
                {
                    if (line.Contains("] NexusNation: Last report (") && line.Contains(") was handled by: "))
                    {
                        dataList.Add(line.Substring(line.LastIndexOf(": ") + 2));
                    }
                }
                
                Hashtable ht = new Hashtable();
                foreach (string s in dataList)
                {
                    if (!ht.Contains(s))
                    {
                        ht.Add(s, 1);
                    }
                    else
                    {
                        ht[s] = (int)ht[s] + 1;
                    }
                }

                foreach (DictionaryEntry data in ht)
                {
                    textBox1.Text += ("\r\n" + data.Key + " - " + data.Value + " report(s)");
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
    }
