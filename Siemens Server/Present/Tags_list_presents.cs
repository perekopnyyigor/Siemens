using Modbus_TCP_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modbus_TCP_Server.Present
{
    class Tags_list_presents
    {
        

        public void ini(TextBox textBox)
        {
            string text = "";
            for(int i=0;i<Data.tags.Count;i++)
            {
                text += Data.tags[i].name + "\t" + Data.tags[i].address + "\t" + Data.tags[i].type + "\t" + Data.tags[i].plc + "\r\n";
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
            for(int i=0;i<context.Length;i++)
            {
                if(context[i]!="")
                {
                    string[] word = context[i].Split(new char[] { '&' });
                    Data.tags.Add(new Tag(word[0], word[1], i, word[2], word[3]));
                }
                              
            }

            
           
        }
    }
}
