using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modbus_TCP_Server.View
{
    public partial class Connect : Form
    {
        Present.Connect_present present = new Present.Connect_present();
        public Connect()
        {
            InitializeComponent();
            present.ini(dataGridView1);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            present.connect(dataGridView1);
            this.Close();
        }
    }
}
