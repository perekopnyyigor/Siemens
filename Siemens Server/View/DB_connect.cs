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
    public partial class DB_connect : Form
    {
        Present.DB_connect db_connect = new Present.DB_connect();
        public DB_connect()
        {
            InitializeComponent();
            db_connect.ini(dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db_connect.read(dataGridView1);
        }
    }
}
