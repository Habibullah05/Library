using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BLL.Model
{
    public class S_Cards
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public Book Book { get; set; }
        public S_Cards()
        {
            Student = new Student();
            Book = new Book();
        }
    }
}
