using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiter.DbUtils
{
public class Mapping
{
    public class BusMap : EntityTypeConfiguration<Bus>
    {
        public BusMap()
        {
        }
    }
    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
        }
    }
}
}
