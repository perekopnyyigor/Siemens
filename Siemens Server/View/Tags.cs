using Modbus_TCP_Server.Present;
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
    
    public partial class Tags : Form
    {
        public DataGridView dataGrid;
        Tags_present tags_present = new Tags_present();
        Present.Present present = new Present.Present();
        public Tags()
        {
            InitializeComponent();
            tags_present.ini(dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tags_present.read(dataGridView1);
            present.reload_datagrid(dataGrid);
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Tags_list tags_list = new Tags_list();
            tags_list.DataGridView = this.dataGridView1;
            tags_list.Show();
        }
    }
}
