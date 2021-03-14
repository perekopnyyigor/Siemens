using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Modbus_TCP_Server.Model
{
    class SaveOpen
    {
        public void create_file()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "db file (*.db)|*.db";
            saveFileDialog.ShowDialog();
            if(saveFileDialog.FileName != "" && saveFileDialog.FileName != null)
                SQLite.create_db(saveFileDialog.FileName);
           
        }

        public void save_parametr()
        {
            string str = "DROP TABLE IF EXISTS Parametrs; ";
            SQLite.create_table(str);
            str = "CREATE TABLE  IF NOT EXISTS  Parametrs (parametr TEXT, value TEXT);";
            SQLite.create_table(str);
            SQLite.insert_parametrs("ip", Data.ip);
            SQLite.insert_parametrs("port", Data.port.ToString());
        }

        public void save_connects()
        {
            string str = "DROP TABLE IF EXISTS Connects; ";
            SQLite.create_table(str);
            str = "CREATE TABLE  IF NOT EXISTS  Connects (name TEXT, ip TEXT, rack INT, slot INT, plc TEXT);";
            SQLite.create_table(str);
            foreach(string name in Data.connects.Keys)
            {
                string ip = Data.connects[name].ip;
                int rack = Data.connects[name].rack;
                int slot = Data.connects[name].slot;
                string plc = Data.connects[name].cpu_str;

                SQLite.insert_connects(name,ip, rack, slot, plc);
            }
        }

        public void save_tags()
        {
            string str = "DROP TABLE IF EXISTS Tags; ";
            SQLite.create_table(str);
            str = "CREATE TABLE  IF NOT EXISTS  Tags (name TEXT, registr_number TEXT, tag_number INT, type TEXT, plc TEXT);";
            SQLite.create_table(str);
            for (int i = 0; i < Data.tags.Count; i++)
            {
                string name = Data.tags[i].name;
                string registr_number = Data.tags[i].address;
                int tag_number = Data.tags[i].tag_number;
                string type = Data.tags[i].type;
                string plc = Data.tags[i].str_plc;
                SQLite.insert_tags(name, registr_number, tag_number, type,plc);
            }
        }
        public void save_write_tags()
        {
            string str = "DROP TABLE IF EXISTS Tags_write; ";
            SQLite.create_table(str);
            str = "CREATE TABLE  IF NOT EXISTS  Tags_write (name TEXT, registr_number TEXT, tag_number INT, type TEXT, plc TEXT);";
            SQLite.create_table(str);
            for (int i = 0; i < Data.tags_write.Count; i++)
            {
                string name = Data.tags_write[i].name;
                string registr_number = Data.tags_write[i].address;
                int tag_number = Data.tags_write[i].tag_number;
                string type = Data.tags_write[i].type;
                string plc = Data.tags_write[i].str_plc;
                SQLite.insert_write_tags(name, registr_number, tag_number, type, plc);
            }
        }
        public void open_file()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            Data.database_name=openFileDialog.FileName;
            if(Data.database_name!="")
            {
                SQLite.sel_query_tags();
                SQLite.sel_query_tags_write();
                SQLite.sel_query_parametr();
                SQLite.sel_query_connects();
            }
            
        }

    }
}
