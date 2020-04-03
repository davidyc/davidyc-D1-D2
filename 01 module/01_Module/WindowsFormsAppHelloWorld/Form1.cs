using System;
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
            var createrString = new HelloCreaterString(new SystemTime());
            string str = createrString.HelloCreateString(name);
            textBoxName.Text = "";
            MessageBox.Show(str);

        }
    }
}
