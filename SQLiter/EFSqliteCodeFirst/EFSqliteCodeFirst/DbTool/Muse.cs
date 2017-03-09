using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace EFSqliteCodeFirst.DbTool
{
    public class Muse : DbContext
    {
        public Muse()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(typeof(Muse).Assembly);
#if DEBUG
            Database.SetInitializer(new MyDbInitializer(Database.Connection.ConnectionString, modelBuilder));
#endif
        }
    }

    public class MyDbInitializer : SqliteDropCreateDatabaseAlways<Muse>
    {
        public MyDbInitializer(string connectionString, DbModelBuilder modelBuilder)
            : base( modelBuilder) { }

        protected override void Seed(Muse context)
        {
            context.Set<Book>().Add(new Book { Name = "Book", Desc = "Desc", Price = 10.5 });
            base.Seed(context);
        }
    }
}