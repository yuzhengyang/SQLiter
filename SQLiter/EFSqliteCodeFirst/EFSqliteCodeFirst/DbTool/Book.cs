using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFSqliteCodeFirst.DbTool
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public double Price { get; set; }
    }
}