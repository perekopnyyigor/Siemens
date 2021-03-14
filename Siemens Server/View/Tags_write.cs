using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modbus_TCP_Server.Present;

namespace Modbus_TCP_Server.View
{
    public partial class Tags_write : Form
    {
        Tags_write_present tags_present = new Tags_write_present();
        Present.Present present = new Present.Present();
        public DataGridView dataGrid = new DataGridView();

        public Tags_write()
        {
            InitializeComponent();
            tags_present.ini(dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tags_present.read(dataGridView1);
            present.reload_datagrid1(dataGrid);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Tags_write_list tags_write_list = new Tags_write_list();
            tags_write_list.dataGridView = this.dataGridView1;
            tags_write_list.Show();
        }
    }
}
