using SQLiter.TestEF.DbTool;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace SQLiter.TestEF
{
    class Program
    {
        static void Main(string[] args)
        {
            //基本增删改查
            Add();
            //Update();
            //Delete("b6eac056-6374-4635-848b-794df1aeac58");
            //List<Room> rooms = GetAllRooms();
            Console.WriteLine("The End.");
            Console.ReadLine();
        }

        static void Add()
        {
            using (var db = new Muse())
            {
                try
                {
                    Room r = new Room { Number = "501", Address = "Road 1", Size = 10, CreateTime = DateTime.Now };
                    IdCard i = new IdCard { IdNumber = "1001011", Desc = "XXXX学院", Type = "学生证" };
                    Student s = new Student { Name = "张三", Birthday = DateTime.Now };
                    db.Set<Room>().Add(r);
                    db.Set<IdCard>().Add(i);
                    db.Set<Student>().Add(s);
                    db.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Console.WriteLine(
                                    "Class: {0}, Property: {1}, Error: {2}",
                                    validationErrors.Entry.Entity.GetType().FullName,
                                    validationError.PropertyName,
                                    validationError.ErrorMessage);
                        }
                    }
                }
            }
        }
        static bool Delete(string id)
        {
            using (var db = new Muse())
            {
                try
                {
                    Room r = new Room() { Id = id };
                    Room a = db.Set<Room>().Attach(r);
                    Room b = db.Set<Room>().Remove(r);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        static void Update()
        {
            using (var db = new Muse())
            {
                Room r = db.Set<Room>().Where(x => x.Number == "501").FirstOrDefault();
                r.Number = "3501";

                Room r501 = db.Set<Room>().Where(x => x.Number == "501").FirstOrDefault();
                Room r1011 = db.Set<Room>().Where(x => x.Number == "1011").FirstOrDefault();

                db.SaveChanges();
            }
        }
        static List<Room> GetAllRooms()
        {
            using (Muse db = new Muse())
            {
                List<Room> rs = db.Set<Room>().ToList();
                if (rs.Count > 0)
                    return rs;
            }
            return null;
        }
    }
}
