using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace DAL.Interface.Models
{
    public class DalRole
    {
        public DalRole()
        {
            this.Users = new HashSet<Users>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Users> Users { get; set; }
    }
}
