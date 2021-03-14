using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EasyModbus;
using S7.Net;
using EasyModbus.Exceptions;
using System.Windows.Forms;

namespace Modbus_TCP_Server.Model
{
    
    class Modbus
    {
       
        private Thread thread;
        public ModbusClient modbusClient;
        public List<Tag> tags;
        public DataGridView dataGridView;
        public ProgressBar progressBar;
        public void connect(string ip,int port)
        {
            try
            {
                Data.modbusClient = new ModbusClient(ip, port);

                Data.modbusClient1 = new ModbusClient(ip, port);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                Data.modbusClient.Connect();
                Data.modbusClient1.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void all_tag()
        {

        }
        private void read()
        {
            for (int i = 0; i < Data.tags.Count; i++)
            {
                Data.tags[i].plc = Data.connects[Data.tags[i].str_plc].plc;
            }
            while (true)
            {
               
                   
                    Action action = () => { progressBar.Value = 0; };
                    progressBar.Invoke(action);
                    for (int i = 0; i < Data.tags.Count; i++)
                    {
                        Data.tags[i].read();
                    }

                    action = () => { progressBar.Value = 100; };
                    progressBar.Invoke(action);
             
                    Thread.Sleep(100);

            }
        }


        public void start(DataGridView dataGridView, ProgressBar progressBar)
        {
            Tag.dataGrid = dataGridView;
            this.progressBar = progressBar;
           
                if (thread == null)
                {
                    thread = new Thread(new ThreadStart(read));
                    thread.Start();
                }
            
         
               
        }
        public void stop()
        {
            if(thread!=null)
            {
                if (thread.IsAlive)
                {
                    thread.Abort();
                    thread = null;
                }
                    
            }
            
        }
        public void write(DataGridView dataGridView)
        {
           
            for (int i = 0; i < Data.tags_write.Count; i++)
            {
                Data.tags_write[i].plc = Data.write_connects[Data.tags_write[i].str_plc].plc;
            }

            for (int i = 0; i < Data.tags_write.Count; i++)
            {
                
                Data.tags_write[i].write(dataGridView);
            }
                           
        }
        public void write_tags_connect()
        {

            for (int i = 0; i < Data.tags_write.Count; i++)
            {
               
                Data.tags_write[i].plc = Data.write_connects[Data.tags_write[i].str_plc].plc;
            }         

        }

    }
    abstract class Tags
    {
        public string name;
        public string address;
        public int tag_number;
        public string type;
        public string str_plc;

        public Plc plc;
        public static DataGridView dataGrid;
        public static ProgressBar progressBar;

        

    }
    class Tag: Tags
    {

        private int[] reg;
        private bool[] reg_bool;
        public object num;
        public Tag(string name, string address, int tag_number, string type, string str_plc)
        {
            this.name = name;
            this.address = address;
            this.tag_number = tag_number;
            this.type = type;
            this.str_plc = str_plc;
        }
        public void read()
        {
            
            try
            {
                if (type == "BOOL")
                {
                    num = (bool)plc.Read(address);
                }
                if (type == "WORD")
                {
                    num = (ushort)plc.Read(address);
                }
                if (type == "INT")
                {
                    num = ((ushort)plc.Read(address)).ConvertToShort();
                }

                if (type == "DWORD")
                {
                    num = (uint)plc.Read(address);
                }
                if (type == "DINT")
                {
                    num = (uint)plc.Read(address);
                }
                if (type == "REAL")
                {
                    num = ((uint)plc.Read(address)).ConvertToFloat();
                }

             
                Action action = () =>
                {
                    dataGrid.Rows[tag_number].Cells["value"].Value = num.ToString();
                    
                };
                dataGrid.Invoke(action);
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

    }
    class Tag_write:Tags
    {
        public Tag_write(string name, string address, int tag_number, string type, string str_plc)
        {
            this.name = name;
            this.address = address;
            this.tag_number = tag_number;
            this.type = type;
            this.str_plc = str_plc;
        }
        public void write(string arg)
        {
            
            try
            {
                if (type == "BOOL")
                {
                    if (arg == "true" || arg == "TRUE" || arg == "1")
                        plc.Write(address, true);
                    if (arg == "false" || arg == "FALSE" || arg == "0")
                        plc.Write(address, false);
                }

                if (type == "WORD")
                {

                    ushort data = (ushort)Convert.ToInt32(arg);
                    plc.Write(address, data);
                }
                if (type == "INT")
                {
                    ushort data = ((short)Convert.ToInt32(arg)).ConvertToUshort();
                    plc.Write(address, data);
                }

                if (type == "DWORD")
                {
                    uint data = (uint)Convert.ToInt32(arg);
                    plc.Write(address, data);
                }
                if (type == "DINT")
                {
                    int data = Convert.ToInt32(arg);
                    plc.Write(address, data);
                }
                if (type == "REAL")
                {
                    uint data = ((float)Convert.ToDouble(arg)).ConvertToUInt();
                    plc.Write(address, data);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void write(DataGridView dataGridView)
        {

            try
            {
              

                if (type == "BOOL")
                {
                    string arg = dataGridView.Rows[tag_number].Cells["value_write"].Value.ToString();
                    if (arg == "true" || arg == "TRUE" || arg == "1")
                        plc.Write(address, true);
                    if (arg == "false" || arg == "FALSE" || arg == "0")
                        plc.Write(address, false);
                }

                if (type == "WORD")
                {
                    string arg = dataGridView.Rows[tag_number].Cells["value_write"].Value.ToString();
                    ushort data = (ushort)Convert.ToInt32(arg);
                    plc.Write(address, data);
                }
                if (type == "INT")
                {
                    string arg = dataGridView.Rows[tag_number].Cells["value_write"].Value.ToString();
                    ushort data = ((short)Convert.ToInt32(arg)).ConvertToUshort();
                    plc.Write(address, data);
                }

                if (type == "DWORD")
                {
                    string arg = dataGridView.Rows[tag_number].Cells["value_write"].Value.ToString();
                    uint data = (uint)Convert.ToInt32(arg);
                    plc.Write(address, data);
                }
                if (type == "DINT")
                {
                    string arg = dataGridView.Rows[tag_number].Cells["value_write"].Value.ToString();
                    int data = Convert.ToInt32(arg);
                    plc.Write(address, data);
                }
                if (type == "REAL")
                {
                    string arg = dataGridView.Rows[tag_number].Cells["value_write"].Value.ToString();
                    uint data = ((float)Convert.ToDouble(arg)).ConvertToUInt();
                    plc.Write(address, data);
                }

            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            string arg1 = dataGridView.Rows[tag_number].Cells["value_write"].Value.ToString();
            MessageBox.Show(arg1);

        }
    }

}

