using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiter.DbUtils
{
    public class Bus
    {
        public string Id { get; set; }
        public string Name { get; set; }
        List<Person> Persons { get; set; }
    }
    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BusId { get; set; }
        public Bus Bus { get; set; }
    }
}
