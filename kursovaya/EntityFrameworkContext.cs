using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursovaya
{
    public partial class EntityFrameworkFW : DbContext
    {
        private static EntityFrameworkFW _context;
        
        
        public static EntityFrameworkFW GetContext()
        {
            if (_context == null)
                _context = new EntityFrameworkFW();
            return _context;
        }

    }
}
