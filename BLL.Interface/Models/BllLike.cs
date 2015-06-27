using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace BLL.Interface.Models
{
    public class BllLike
    {
        public int UserId { get; set; }
        public int PictureId { get; set; }
        public bool? Like { get; set; }

        public virtual Pictures Picture { get; set; }
        public virtual Users User { get; set; }
    }
}
