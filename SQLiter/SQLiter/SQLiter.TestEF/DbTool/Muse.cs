using SQLite.CodeFirst;
using SQLiter.TestEF.Migrations;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SQLiter.TestEF.DbTool
{
    public class Muse : DbContext
    {
        public Muse() : base("DefaultConnection")
        {
            //? 允许丢失数据的更新表
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Muse, Configuration>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(typeof(Muse).Assembly);
#if DEBUG
            //Database.SetInitializer(new MyDbInitializer(Database.Connection.ConnectionString, modelBuilder));//每次都会创建新数据库表
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
