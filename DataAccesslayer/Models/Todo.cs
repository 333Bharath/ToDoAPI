using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models
{
    [Table("Todos")]
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        public string TodoData { get; set; }
        public bool IsCompleted { get; set; }
    }
}
