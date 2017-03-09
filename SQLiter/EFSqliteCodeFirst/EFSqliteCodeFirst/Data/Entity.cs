using System.Collections.Generic;

namespace EFSqliteCodeFirst
{
    public class BaseEntity
    {
        public int Id { get; set; }
    }

    public class Customer : BaseEntity
    {
        public Customer()
        {
            this.Roles = new List<Role>();
        }

        public string UserName { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }

    public class Role : BaseEntity
    {
        public Role()
        {
            this.Customers = new List<Customer>();
        }

        public virtual ICollection<Customer> Customers { get; set; }

        public string RoleName { get; set; }
    }

    public class Category : BaseEntity
    {
        public Category()
        {
            this.Children = new List<Category>();
            this.Posts = new List<Post>();
        }

        public int? ParentId { get; set; }

        public virtual Category Parent { get; set; }

        public virtual ICollection<Category> Children { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }

    public class Post : BaseEntity
    {
        public virtual Category Category { get; set; }
    }
}