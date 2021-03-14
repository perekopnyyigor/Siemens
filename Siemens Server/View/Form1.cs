using Modbus_TCP_Server.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modbus_TCP_Server
{
    public partial class Form1 : Form
    {
        Present.Present present = new Present.Present();
        public Form1()
        {
            InitializeComponent();
          


        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            present.start(dataGridView1, progressBar1);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            present.write(dataGridView2);
        }

        private void соединениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View.Connect connect = new View.Connect();
            connect.Show();
        }

        private void тегиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            present.stop();
            View.Tags tags = new View.Tags();
            tags.dataGrid = this.dataGridView1;
            tags.Show();
        }

       

        private void исходящиеТегиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View.Tags_write tags = new View.Tags_write();
            tags.dataGrid = this.dataGridView2;
            tags.Show();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            present.save_file();
        }

        private void открытьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            present.open_file(dataGridView1, dataGridView2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            present.stop();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            present.connect_DB(progressBar2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            present.stop_db();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            present.read_bd(dataGridView2, progressBar3);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            present.stop_read_bd();
        }

        private void настройкаДБToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DB_connect dB_Connect = new DB_connect();
            dB_Connect.Show();
        }
    }
}
