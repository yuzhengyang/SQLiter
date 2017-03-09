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
            using (var db = new Muse())
            {
                try
                {
                    Room room = new Room { Number = "501" };
                    IdCard ic = new IdCard { IdNumber = "1001011", Desc = "学生证" };
                    Student s = new Student { Name = "张三", Birthday = DateTime.Now, IdCardId = ic.Id };
                    db.Set<Room>().Add(room);
                    db.Set<IdCard>().Add(ic);
                    db.Set<Student>().Add(s);
                    db.SaveChanges();

                    List<Student> temp = db.Set<Student>().Where(x => x.Name != null).ToList();

                    Room r = db.Set<Room>().Where(x => x.Id == "44815657-47fb-409a-8aa6-82a647e5dd76").FirstOrDefault();
                    List<Room> rr = db.Set<Room>().ToList();
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
            //Student s = null;
            //using (Muse db = new Muse())
            //{
            //    db.Student.Add(new Student() { Name = "张三", Birthday = DateTime.Now });
            //    db.SaveChanges();

            //    s = db.Student.FirstOrDefault();
            //}
            Console.WriteLine("done ...");
            Console.ReadLine();
        }
    }
}
