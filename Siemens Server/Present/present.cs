using Modbus_TCP_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modbus_TCP_Server.Present
{

    class Present
    {
        Modbus modbus = new Modbus();
        
        SaveOpen saveOpen = new SaveOpen();
        MySQL mySql = new MySQL();
        private void write_datagrid(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            for (int i = 0; i < Data.tags.Count; i++)
            {
                dataGridView.Rows.Add();
                dataGridView.Rows[i].Cells["tag"].Value = Data.tags[i].name;
                dataGridView.Rows[i].Cells["registr"].Value = Data.tags[i].address;
                dataGridView.Rows[i].Cells["type"].Value = Data.tags[i].type;
            }
        }

        private void write_datagrid1(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            for (int i = 0; i < Data.tags_write.Count; i++)
            {
                dataGridView.Rows.Add();
                dataGridView.Rows[i].Cells["tag_write"].Value = Data.tags_write[i].name;
                dataGridView.Rows[i].Cells["registr_write"].Value = Data.tags_write[i].address;
                dataGridView.Rows[i].Cells["type_write"].Value = Data.tags_write[i].type;
            }
        }
        public void start(DataGridView dataGridView,ProgressBar progressBar)
        {
            write_datagrid(dataGridView);
            modbus.start(dataGridView, progressBar);
        }
        public void stop()
        {
            modbus.stop();
           
        }
        public void write(DataGridView dataGridView)
        {
           
            modbus.write(dataGridView);
        }
        public void reload_datagrid(DataGridView dataGridView)
        {
            write_datagrid(dataGridView);
        }

        public void reload_datagrid1(DataGridView dataGridView)
        {
            write_datagrid1(dataGridView);
        }
        public void save_file()
        {
            if (Data.database_name != "")
            {
                saveOpen.create_file();
                saveOpen.save_parametr();
                saveOpen.save_tags();
                saveOpen.save_write_tags();
                saveOpen.save_connects();
            }
                
        }
        public void open_file(DataGridView dataGridView, DataGridView dataGridView1)
        {
            saveOpen.open_file();
            write_datagrid(dataGridView);
            write_datagrid1(dataGridView1);
        }

        public void connect_DB(ProgressBar progressBar)
        {
            mySql.progressBar = progressBar;
            mySql.connect();
            mySql.start();
        }
        public void stop_db()
        {
            mySql.stop();
        }
        public void read_bd(DataGridView dataGridView, ProgressBar progressBar)
        {
            mySql.progressBar1 = progressBar;
            mySql.dataGrid = dataGridView;
            modbus.write_tags_connect();
            mySql.create_table_out();
            mySql.start_read();
        }
        public void stop_read_bd()
        {
            mySql.stop_read();
        }
    }
}
