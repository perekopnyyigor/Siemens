using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modbus_TCP_Server.Model;

namespace Modbus_TCP_Server.Present
{
    class Tags_write_present
    {
        private void create_column_connect(DataGridView dataGridView)
        {
            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            dgvCmb.HeaderText = "Соединение";
            foreach (string name in Data.write_connects.Keys)
            {
                dgvCmb.Items.Add(name);
            }

            dgvCmb.Name = "connect";
            dataGridView.Columns.Add(dgvCmb);
        }
        private void read_datagrid(DataGridView dataGridView)
        {
            Data.tags_write.Clear();
            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                if (dataGridView.Rows[i].Cells["type"].Value == null)
                {
                    MessageBox.Show("Не выбран тип тега");
                }
                else if (dataGridView.Rows[i].Cells["tag"].Value == null)
                {
                    MessageBox.Show("Не заполнено название");
                }
                else if (dataGridView.Rows[i].Cells["registr"].Value == null)
                {
                    MessageBox.Show("Не указан регистр");
                }
                else
                {

                    string tag = dataGridView.Rows[i].Cells["tag"].Value.ToString();
                    string reg = dataGridView.Rows[i].Cells["registr"].Value.ToString();
                    string type = dataGridView.Rows[i].Cells["type"].Value.ToString();
                    string connect = dataGridView.Rows[i].Cells["connect"].Value.ToString();
                    Data.tags_write.Add(new Tag_write(tag, reg, i, type, connect));
                }
            }
        }
        private void write_datagrid(DataGridView dataGridView)
        {
            for (int i = 0; i < Data.tags_write.Count; i++)
            {
                dataGridView.Rows.Add();
                dataGridView.Rows[i].Cells["tag"].Value = Data.tags_write[i].name;
                dataGridView.Rows[i].Cells["registr"].Value = Data.tags_write[i].address;
                dataGridView.Rows[i].Cells["type"].Value = Data.tags_write[i].type;
                dataGridView.Rows[i].Cells["connect"].Value = Data.tags_write[i].str_plc;
            }
        }
        
        public void read(DataGridView dataGridView)
        {
            read_datagrid(dataGridView);
        }
        public void ini(DataGridView dataGridView)
        {
            create_column_connect(dataGridView);
            write_datagrid(dataGridView);
            
        }
    }
}
