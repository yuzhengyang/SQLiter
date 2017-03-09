using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLiter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Print("更新 Polycom Sqlite登录信息库");
            string connStr = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + @"RPD.s3db";
            string sql = string.Format("select value from PreferenceTable where key='CMA_SERVER_ADDRESS'");
            using (SQLiteConnection connection = new SQLiteConnection(connStr))
            {
                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    try
                    {
                        cmd.CommandText = sql;
                        SQLiteDataReader sdr = cmd.ExecuteReader();
                    }
                    catch (SQLiteException ex) { }
                }
                connection.Close();
            }

            Print("");
        }

        public void Print(string s)
        {
            BeginInvoke(new Action(() =>
            {
                TbTxt.AppendText(s + Environment.NewLine);
            }));
        }
    }
}