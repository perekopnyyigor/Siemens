using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyModbus;
using S7.Net;
namespace Modbus_TCP_Server.Model
{
    static class Data
    {
        public static List<Tag> tags = new List<Tag>();
        public static List<Tag_write> tags_write = new List<Tag_write>();
        public static Dictionary<string,Connect> connects= new Dictionary<string, Connect>();
        public static Dictionary<string, Connect> write_connects = new Dictionary<string, Connect>();

        public static string ip;
        public static int port;
        public static int time;
        public static ModbusClient modbusClient;
        public static ModbusClient modbusClient1;
        public static string database_name;
        public static class Base
        {
            public static string server;
            public static string user;
            public static string database;
            public static int port;
            public static string password;
        }
        public class Connect
        {
            public string name;
            public string ip;
            public short rack;
            public short slot;
            public string cpu_str;
            public CpuType cpu;
            public Plc plc; 
            public Connect(string name, string ip, short rack, short slot, string cpu)
            {
                this.name = name;
                this.ip = ip;
                this.rack = rack;
                this.slot = slot;
                cpu_str = cpu;
                this.cpu = find_cpu(cpu);
                try
                {
                    plc = new Plc(this.cpu, ip, rack, slot);
                    plc.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            private CpuType find_cpu(string str_cpu)
            {
                if (str_cpu == "S7200")
                    cpu = CpuType.S7200;
                else if (str_cpu == "S7300")
                    cpu = CpuType.S7300;
                else if (str_cpu == "S7400")
                    cpu = CpuType.S7400;
                else if (str_cpu == "S71200")
                    cpu = CpuType.S71200;
                else if (str_cpu == "S71500")
                    cpu = CpuType.S71500;
                return cpu;
            }
           
           
        }
    }
}
