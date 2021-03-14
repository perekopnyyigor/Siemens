using Modbus_TCP_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modbus_TCP_Server.Present
{
    class Connect_present
    {
        Modbus modbus = new Modbus();
        public void ini(DataGridView dataGridView)
        {
            int i = 0;
            foreach (string name in Data.connects.Keys)
            {
                dataGridView.Rows.Add();
                dataGridView.Rows[i].Cells["name"].Value = name;
                dataGridView.Rows[i].Cells["ip"].Value = Data.connects[name].ip;
                dataGridView.Rows[i].Cells["rack"].Value = Data.connects[name].rack;
                dataGridView.Rows[i].Cells["slot"].Value = Data.connects[name].slot;
                dataGridView.Rows[i].Cells["cpu"].Value = Data.connects[name].cpu_str;
                i++;
            }
        }

        public void connect(DataGridView dataGridView )
        {
            Data.connects.Clear();
            Data.write_connects.Clear();
            for (int i=0; i<dataGridView.Rows.Count-1; i++)
            {
                string name = dataGridView.Rows[i].Cells["name"].Value.ToString();
                MessageBox.Show(name);
                string ip = dataGridView.Rows[i].Cells["ip"].Value.ToString();
                short rack = (short)Convert.ToInt32(dataGridView.Rows[i].Cells["rack"].Value);
                short slot = (short)Convert.ToInt32(dataGridView.Rows[i].Cells["slot"].Value);
                string cpu = dataGridView.Rows[i].Cells["cpu"].Value.ToString();

                Data.connects.Add(name, new Data.Connect(name, ip, rack, slot, cpu));
                Data.write_connects.Add(name, new Data.Connect(name, ip, rack, slot, cpu));
            }
                
        }
    }
}
