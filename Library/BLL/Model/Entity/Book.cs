using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BLL.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Pages { get; set; }
        public int YearPress { get; set; }
        public Press Press { get; set; }
        public  Category Category { get; set; }
        public Author Author { get; set; }
        public Theme Theme { get; set; }
        public string Comment { get; set; }
        public int Quantity { get; set; }

        public Book()
        {
            Press = new Press();
            Category = new Category();
            Author = new Author();
            Theme = new Theme();
        }    
    }

    public class Press
    {
        public int Id { get; set; } = -1;
        public string Name { get; set; }
    }
    public class Author
    {
        public int Id { get; set; } = -1;
        public string FullName { get; set; }
       
    }
    public class Category
    {
        public int Id { get; set; } = -1;
        public string Name { get; set; }
    }
    public class Theme
    {
        public int Id { get; set; } = -1;
        public string Name { get; set; }
    }
}
