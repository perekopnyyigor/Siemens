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
    public partial class Tags_list : Form
    {
        public DataGridView DataGridView;

        Present.Tags_list_presents tags_List_Presents = new Present.Tags_list_presents();
        Present.Tags_present tags_present = new Present.Tags_present();
        public Tags_list()
        {
            InitializeComponent();
            tags_List_Presents.ini(textBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tags_List_Presents.read(textBox1);
            DataGridView.Rows.Clear();
            tags_present.ini(DataGridView);
            this.Close();
        }
    }
}
