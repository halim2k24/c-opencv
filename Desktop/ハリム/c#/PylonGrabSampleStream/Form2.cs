using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PylonGrabSampleStream
{
    public partial class StartUpForm : Form
    {
        public Form1 FormInspec { get; set; }
        public StartUpForm()
        {
            InitializeComponent();
            this.FormInspec = new Form1();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.FormInspec.Show();
        }
    }
}
