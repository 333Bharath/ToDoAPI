using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace DataAccesslayer.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext() : base("todoConnectionString")
        {

        }

        public DbSet<Todo> Todos { get; set; }
    }
}
