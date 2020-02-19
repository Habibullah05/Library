using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BLL.Model
{
    public class S_Card
    {
        public int Id { get; set; }
        public int Id_Student { get; set; }
        public int Id_Book { get; set; }
        public DateTime DateOut { get; set; } = DateTime.Now;
        public Nullable<DateTime> DateIn { get; set; } = null;
       
    }
}
