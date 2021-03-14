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
    public partial class Tags_write_list : Form
    {
        public DataGridView dataGridView;

        Present.Tags_write_list_present tags_List_Presents = new Present.Tags_write_list_present();
        Present.Tags_write_present tags_present = new Present.Tags_write_present();
        public Tags_write_list()
        {
            InitializeComponent();
            tags_List_Presents.ini(textBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tags_List_Presents.read(textBox1);
            dataGridView.Rows.Clear();
            tags_present.ini(dataGridView);
            this.Close();
        }
    }
}
