using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EFSqliteCodeFirst
{
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(typeof(SqliteDbContext).Assembly);
#if DEBUG
            Database.SetInitializer(new MyDbInitializer(Database.Connection.ConnectionString, modelBuilder));
#endif
        }
    }

    public class MyDbInitializer : SqliteDropCreateDatabaseAlways<SqliteDbContext>
    {
        public MyDbInitializer(string connectionString, DbModelBuilder modelBuilder)
            : base( modelBuilder) { }

        protected override void Seed(SqliteDbContext context)
        {
            context.Set<Customer>().Add(new Customer { UserName = "user" + DateTime.Now.Ticks.ToString(), Roles = new List<Role> { new Role { RoleName = "user" } } });
            context.Set<Post>().Add(new Post { Category = new Category() });
            base.Seed(context);
        }
    }
}