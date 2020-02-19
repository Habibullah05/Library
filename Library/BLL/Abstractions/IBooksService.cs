using Library.BLL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BLL.Abstractions
{
    public interface IBooksService
    {
        Task<ObservableCollection<Book>> SearchBooks(string text = "");
        Task<ObservableCollection<Student>> SearchStuds(string text = "");
        Task<ObservableCollection<Student>> GetStudents();
        Task<ObservableCollection<Book>> GetStudentBooks(int id);
        Task<ObservableCollection<Press>> GetPresses();
        Task<ObservableCollection<Theme>> GetThemes();
        Task<ObservableCollection<Author>> GetAuthors();
        Task<ObservableCollection<Category>> GetCategories();
        Task ReturnBook(int IdBook, int IdStud);
        Task<string> GiveBook(int IdBook, int IdStud);
        Task DeleteBook(int id);
        Task<bool> AddBook(Book book);
    }
}
