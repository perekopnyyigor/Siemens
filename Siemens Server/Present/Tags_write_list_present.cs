using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modbus_TCP_Server.Model;

namespace Modbus_TCP_Server.Present
{
    class Tags_write_list_present
    {

        public void ini(TextBox textBox)
        {
            string text = "";
            for (int i = 0; i < Data.tags_write.Count; i++)
            {
                text += Data.tags_write[i].name + "\t" + Data.tags_write[i].address + "\t" + Data.tags_write[i].type + "\t" + Data.tags_write[i].str_plc + "\r\n";
            }
            textBox.Text = text;
        }
        public void read(TextBox textBox)
        {
            string text;
            text = textBox.Text.Replace("\r\n", "@");
            text = text.Replace("\t", "&");

            string[] context = text.Split(new char[] { '@' });
            Data.tags.Clear();
            for (int i = 0; i < context.Length; i++)
            {
                string[] word = context[i].Split(new char[] { '&' });
                Data.tags_write.Add(new Tag_write(word[0], word[1], i, word[2], word[3]));
            }



        }
    }
}
