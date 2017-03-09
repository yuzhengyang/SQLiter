using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace EFSqliteCodeFirst.Data
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            this.Property(o => o.UserName).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
        }
    }

    public class RolerMap : EntityTypeConfiguration<Role>
    {
        public RolerMap()
        {
            this.HasMany(o => o.Customers).WithMany(o => o.Roles);
        }
    }

    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            this.HasOptional(o => o.Parent).WithMany(o => o.Children).HasForeignKey(o => o.ParentId);
        }
    }

    public class PostMap : EntityTypeConfiguration<Post>
    {
        public PostMap()
        {
            this.HasOptional(o => o.Category).WithMany(o => o.Posts);
        }
    }
}