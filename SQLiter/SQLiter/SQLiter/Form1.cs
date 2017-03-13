using SQLiter.DbUtils;
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
        //创建Bus
        Bus Bus = new Bus() { Id = Guid.NewGuid().ToString(), Name = "11 路" };
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //默认往数据库添加一个Bus
            using (SQLiteDb db = new SQLiteDb())
            {
                db.Set<Bus>().Add(Bus);
                db.SaveChanges();
            }
        }
        //添加人员
        void InsertPerson(string name)
        {
            using (SQLiteDb db = new SQLiteDb())
            {
                Bus dbBus = db.Set<Bus>().Where(x => x.Id == Bus.Id).FirstOrDefault();
                db.Set<Person>().Add(new Person() { Id = Guid.NewGuid().ToString(), Name = name, Bus = dbBus });
                db.SaveChanges();
            }
        }
        //删除人员
        void DeletePerson(string id)
        {
            using (SQLiteDb db = new SQLiteDb())
            {
                Person p = new Person() { Id = id };
                Person person = db.Set<Person>().Attach(p);
                db.Set<Person>().Remove(person);
                db.SaveChanges();
            }
        }
        //更新人员
        void UpdatePerson(string id, string name)
        {
            using (SQLiteDb db = new SQLiteDb())
            {
                Person p = db.Set<Person>().Where(x => x.Id == id).FirstOrDefault();
                p.Name = name;
                db.SaveChanges();
            }
        }

        #region 点击按钮操作
        private void BtInsert_Click(object sender, EventArgs e)
        {
            //清空文本框
            TbTxt.Text = "";
            //插入人员
            InsertPerson(TbInsert.Text);
            //显示人员信息
            using (SQLiteDb db = new SQLiteDb())
            {
                List<Person> persons = db.Set<Person>().ToList();
                if (persons != null)
                {
                    persons.ForEach(x =>
                    {
                        TbTxt.Text += x.Id + "  " + x.Name + Environment.NewLine;
                    });
                }
            }
        }
        private void BtDelete_Click(object sender, EventArgs e)
        {
            TbTxt.Text = "";
            DeletePerson(TbDelete.Text);
            using (SQLiteDb db = new SQLiteDb())
            {
                List<Person> persons = db.Set<Person>().ToList();
                if (persons != null)
                {
                    persons.ForEach(x =>
                    {
                        TbTxt.Text += x.Id + "  " + x.Name + Environment.NewLine;
                    });
                }
            }
        }
        private void BtUpdate_Click(object sender, EventArgs e)
        {
            TbTxt.Text = "";
            UpdatePerson(TbUpdate.Text, DateTime.Now.ToString("mm:ss"));
            using (SQLiteDb db = new SQLiteDb())
            {
                List<Person> persons = db.Set<Person>().ToList();
                if (persons != null)
                {
                    persons.ForEach(x =>
                    {
                        TbTxt.Text += x.Id + "  " + x.Name + Environment.NewLine;
                    });
                }
            }
        }
        private void BtSelect_Click(object sender, EventArgs e)
        {
            TbTxt.Text = "";
            if (TbSelect.Text == "")
            {
                //查询所有人员信息
                using (SQLiteDb db = new SQLiteDb())
                {
                    List<Person> persons = db.Set<Person>().ToList();
                    if (persons != null)
                    {
                        persons.ForEach(x =>
                        {
                            TbTxt.Text += x.Id + "  " + x.Name + Environment.NewLine;
                        });
                    }
                }
            }
            else
            {
                //根据Id查询人员
                using (SQLiteDb db = new SQLiteDb())
                {
                    Person person = db.Set<Person>().Where(x => x.Id == TbSelect.Text).FirstOrDefault();
                    TbTxt.Text = person.Id + "  " + person.Name + Environment.NewLine;
                }
            }
        }
        #endregion
    }
}