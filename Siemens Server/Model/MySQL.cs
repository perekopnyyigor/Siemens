using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Threading;
namespace Modbus_TCP_Server.Model
{
    class MySQL
    {
        public Thread thread; 
        public Thread thread_read;
        public ProgressBar progressBar;
        public ProgressBar progressBar1;
        public DataGridView dataGrid;

        public void connect()
        {
            string connStr =
                    "server=" + Data.Base.server + ";" +
                    "user=" + Data.Base.user + ";" +
                    "database=" + Data.Base.database + ";" +
                    "port=" + Data.Base.port + ";" +
                    "password=" + Data.Base.password + "";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                MessageBox.Show("Connecting to MySQL...");
                conn.Open();

                string sql = "DROP TABLE IF EXISTS Tags;" +
                             "CREATE TABLE  IF NOT EXISTS  Tags (name TEXT, value TEXT);";

                for (int i = 0; i < Data.tags.Count; i++)
                    sql += "INSERT INTO  Tags (name) VALUES('" + Data.tags[i].name + "');";


                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            conn.Close();
            
        }
        public void create_table_out()
        {
            string connStr =
                    "server=" + Data.Base.server + ";" +
                    "user=" + Data.Base.user + ";" +
                    "database=" + Data.Base.database + ";" +
                    "port=" + Data.Base.port + ";" +
                    "password=" + Data.Base.password + "";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                MessageBox.Show("Connecting to MySQL...");
                conn.Open();

                string sql = "DROP TABLE IF EXISTS Commands;" +
                             "CREATE TABLE  IF NOT EXISTS  Commands (name TEXT, value TEXT);";
                
                for (int i = 0; i < Data.tags_write.Count; i++)
                    sql += "INSERT INTO  Commands (name) VALUES('" + Data.tags_write[i].name + "');";


                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            conn.Close();

        }

        public void update()
        {
            while (true) 
            {
                string connStr =
                    "server=" + Data.Base.server + ";" +
                    "user=" + Data.Base.user + ";" +
                    "database=" + Data.Base.database + ";" +
                    "port=" + Data.Base.port + ";" +
                    "password=" + Data.Base.password + "";
                Action action = () => { progressBar.Value = 0; };
                progressBar.Invoke(action);
                MySqlConnection conn = new MySqlConnection(connStr);
                try
                {

                    conn.Open();


                    string sql = "";
                    for (int i = 0; i < Data.tags.Count; i++)
                    {
                        try
                        {
                            sql += "UPDATE Tags SET value = '" + Data.tags[i].num.ToString() + "' WHERE name = '" + Data.tags[i].name + "';";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }


                    }
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                conn.Close();
                action = () => { progressBar.Value = 100; };
                progressBar.Invoke(action);
                Thread.Sleep(1000);
            }
            
            
        }

        public void update_read()
        {
            while(true)
            {
                string connStr = 
                    "server="+Data.Base.server+ ";" +
                    "user=" + Data.Base.user + ";" +
                    "database=" + Data.Base.database + ";" +
                    "port=" + Data.Base.port + ";" +
                    "password=" + Data.Base.password + "";
                MySqlConnection conn = new MySqlConnection(connStr);
                Action action_progress = () => { progressBar1.Value = 0; };
                progressBar1.Invoke(action_progress);
                try
                {
                    

                    conn.Open();

                    string sql = "SELECT value FROM Commands";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    int i = 0;
                    while (rdr.Read())
                    {
                        string str = rdr[0].ToString();
                        Action action = () => { dataGrid.Rows[i].Cells["value_write"].Value = str; };
                        dataGrid.Invoke(action);
                        Data.tags_write[i].write(str);
                        i++;
                    }
                    rdr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    thread_read.Abort();
                }
                action_progress = () => { progressBar1.Value = 100; };
                progressBar1.Invoke(action_progress);
                conn.Close();
                Thread.Sleep(100);
            }
            
            
        

    }
        public void start()
        {
            if (thread == null)
            {
                thread = new Thread(new ThreadStart(update));
                thread.Start();
            }
        }
        public void stop()
        {
            if (thread != null)
            {
                if (thread.IsAlive)
                {
                    thread.Abort();
                    thread = null;
                }

            }

        }

        public void start_read()
        {
           
            if (thread_read == null)
            {
                thread_read = new Thread(new ThreadStart(update_read));
                thread_read.Start();
            }
        }
        public void stop_read()
        {
            if (thread_read != null)
            {
                if (thread_read.IsAlive)
                {
                    thread_read.Abort();
                    thread_read = null;
                }

            }

        }


    }
}
