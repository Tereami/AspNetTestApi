using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DomainModel;

namespace ClientDesktopApp
{
    public partial class Form1 : Form
    {
        WebConnector connector;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {

            connector = new WebConnector(textBoxServerUrl.Text);
        }
    }
}
