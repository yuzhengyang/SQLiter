using SQLite.CodeFirst;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SQLiter.TestEF.DbTool
{
    public class Muse : DbContext
    {
        public Muse() : base("DefaultConnection")
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
            : base(modelBuilder) { }

        protected override void Seed(Muse context)
        {
            //context.Set<Student>().Add(new Student { Name = "Tom", Birthday = DateTime.Now });
            base.Seed(context);
        }
    }
}
