using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace BLL.Interface.Models
{
    public class BllPicture
    {
        public BllPicture()
        {
            this.Likes = new HashSet<Likes>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }
    
        public virtual ICollection<Likes> Likes { get; set; }
        public virtual Users User { get; set; }
    }
}
