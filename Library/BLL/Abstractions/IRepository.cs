using Library.BLL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BLL.Abstractions
{
    public interface IRepository
    {
        Task<ObservableCollection<Press>> GetPresses();
        Task<ObservableCollection<Author>> GetAuthors();
        Task<ObservableCollection<Category>> GetCategories();
        Task<ObservableCollection<Theme>> GetThemes();
        Task<ObservableCollection<Book>> GetBooks();
        Task<ObservableCollection<Student>> GetStudents();
        Task<ObservableCollection<S_Card>> Get_Cards();
        Task UpdateS_Cards(S_Card card);
        Task InsertS_Cards(S_Card card);
        Task DELETEBook(int id);
        Task AddPress(Press press);
        Task AddAutor(Author author);
        Task AddCategory(Category category);
        Task AddTheme(Theme theme);
        Task AddBook(Book book);
    }
}
