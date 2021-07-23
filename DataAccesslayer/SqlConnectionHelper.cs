using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataAccesslayer.Models;

namespace DataAccesslayer
{
    public class SqlConnectionHelper 
    {
        private readonly TodoContext _context;
        public SqlConnectionHelper()
        {
            _context = new TodoContext();
        }

        public TodoContext GetConnection()
        {
            return _context;
        }
    }
}
