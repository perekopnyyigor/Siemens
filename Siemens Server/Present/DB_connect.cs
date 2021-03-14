using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modbus_TCP_Server.Present
{
    class DB_connect
    {
        public void ini(DataGridView dataGridView)
        {
            dataGridView.Rows.Add();
            dataGridView.Rows[0].Cells["name"].Value = "server";
            dataGridView.Rows.Add();
            dataGridView.Rows[1].Cells["name"].Value = "user";
            dataGridView.Rows.Add();
            dataGridView.Rows[2].Cells["name"].Value = "database";
            dataGridView.Rows.Add();
            dataGridView.Rows[3].Cells["name"].Value = "port";
            dataGridView.Rows.Add();
            dataGridView.Rows[4].Cells["name"].Value = "password";
            if (Model.Data.Base.server==null)
            {                
                dataGridView.Rows[0].Cells["value"].Value = "localhost";
                dataGridView.Rows[1].Cells["value"].Value = "mysql";              
                dataGridView.Rows[2].Cells["value"].Value = "base";               
                dataGridView.Rows[3].Cells["value"].Value = "3306";               
                dataGridView.Rows[4].Cells["value"].Value = "mysql";
            }
            else
            {
                dataGridView.Rows[0].Cells["value"].Value = Model.Data.Base.server;
                dataGridView.Rows[1].Cells["value"].Value = Model.Data.Base.user;
                dataGridView.Rows[2].Cells["value"].Value = Model.Data.Base.database;
                dataGridView.Rows[3].Cells["value"].Value = Model.Data.Base.port.ToString();
                dataGridView.Rows[4].Cells["value"].Value = Model.Data.Base.password;
            }
           
        }

        public void read(DataGridView dataGridView)
        {
            Model.Data.Base.server = dataGridView.Rows[0].Cells["value"].Value.ToString();
            Model.Data.Base.user = dataGridView.Rows[1].Cells["value"].Value.ToString();
            Model.Data.Base.database = dataGridView.Rows[2].Cells["value"].Value.ToString();
            Model.Data.Base.port = Convert.ToInt32(dataGridView.Rows[3].Cells["value"].Value);
            Model.Data.Base.password= dataGridView.Rows[4].Cells["value"].Value.ToString();
        }
    }
}
