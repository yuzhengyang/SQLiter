using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiter.TestEF.DbTool
{
    public class Student
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string RoomId { get; set; }
        public virtual Room Room { get; set; }
        public string IdCardId { get; set; }
        public virtual IdCard IdCard { get; set; }

        public Student()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
    public class IdCard
    {
        public string Id { get; set; }
        public string IdNumber { get; set; }
        public string Type { get; set; }
        public string Desc { get; set; }

        public IdCard()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
    public class Room
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public virtual List<Student> Students { get; set; }

        public Room()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
    public class Teacher
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Major { get; set; }
        public int Level { get; set; }

        public Teacher()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
