﻿using System;
using System.Windows.Forms;
using HelloWorldClassLiblary;

namespace WindowsFormsAppHelloWorld
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string str = CreaterString.CreateString(name);
            textBoxName.Text = "";
            MessageBox.Show(str);

        }
    }
}
