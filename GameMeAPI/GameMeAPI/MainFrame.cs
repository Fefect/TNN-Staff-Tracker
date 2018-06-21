using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
    }
}
