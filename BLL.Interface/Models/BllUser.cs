using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace BLL.Interface.Models
{
    public class BllUser
    {
        public BllUser()
        {
            this.Likes = new HashSet<Likes>();
            this.Pictures = new HashSet<Pictures>();
            this.Roles = new HashSet<Roles>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    
        public virtual ICollection<Likes> Likes { get; set; }
        public virtual ICollection<Pictures> Pictures { get; set; }
        public virtual ICollection<Roles> Roles { get; set; }
    }
}
