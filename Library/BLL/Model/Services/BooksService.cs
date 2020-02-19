using Library.BLL.Abstractions;
using Library.BLL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library.BLL.Services
{
    public class BooksService : IBooksService
    {
        private IRepository repository;

        public BooksService(IRepository rep) { repository = rep; }

        private async Task<ObservableCollection<Book>> SetAllEntity()
        {
            ObservableCollection<Book> books = await repository.GetBooks();
            foreach (var item in books)
            {
                item.Press = await GetPress(item.Press.Id);
                item.Author = await GetAuthor(item.Author.Id);
                item.Category = await GetCategory(item.Category.Id);
                item.Theme = await GetTheme(item.Theme.Id);
            }
            return books;
        }

        public async Task<Book> GetBook(int id) { return (await SetAllEntity()).Where(t => t.Id == id).FirstOrDefault(); }

        public async Task<ObservableCollection<Book>> SearchBooks(string text = "")
        {
            if (text.Length == 0) return await SetAllEntity();
            return new ObservableCollection<Book>((await SetAllEntity()).Where(x => x.Name.ToUpper().Contains(text.ToUpper())));
        }

        public async Task<ObservableCollection<Student>> SearchStuds(string text = "")
        {
            if (text.Length == 0) return await repository.GetStudents();
            return new ObservableCollection<Student>((await repository.GetStudents()).Where(x => x.LastName.ToUpper().Contains(text.ToUpper()) || x.FirstName.ToUpper().Contains(text.ToUpper())));
        }

        public async Task<ObservableCollection<Book>> GetStudentBooks(int id_Stud)
        {
            if (id_Stud == 0) return null;
            var tmp = (await repository.Get_Cards()).Where(x => x.Id_Student == id_Stud && x.DateIn is null);

            ObservableCollection<Book> books = new ObservableCollection<Book>();
            foreach (var item in (await SetAllEntity()))
            {
                if (tmp.Any(t => t.Id_Book == item.Id)) { books.Add(item); }
            }
            return books;
        }

        public async Task<ObservableCollection<Press>> GetPresses() { return await repository.GetPresses(); }
        public async Task<Press> GetPress(int id)
        {
            return (await repository.GetPresses()).Where(t => t.Id == id).FirstOrDefault();
        }

        public async Task<ObservableCollection<Category>> GetCategories() { return await repository.GetCategories(); }
        public async Task<Category> GetCategory(int id) { return (await repository.GetCategories()).Where(t => t.Id == id).FirstOrDefault(); }

        public async Task<ObservableCollection<Theme>> GetThemes() { return await repository.GetThemes(); }
        public async Task<Theme> GetTheme(int id) { return (await repository.GetThemes()).Where(t => t.Id == id).FirstOrDefault(); }

        public async Task<ObservableCollection<Author>> GetAuthors() { return await repository.GetAuthors(); }
        public async Task<Author> GetAuthor(int id) { return (await repository.GetAuthors()).Where(t => t.Id == id).FirstOrDefault(); }

        public async Task<ObservableCollection<Student>> GetStudents() { return await repository.GetStudents(); }
        //  public ObservableCollection<S_Card> Get_Cards() { return  repository.Get_Cards(); }

        public async Task<Student> GetStudent(int id) { return (await GetStudents()).Where(t => t.Id == id).FirstOrDefault(); }

        public async Task<string> GiveBook(int IdBook, int IdStud)
        {
            if ((await GetBook(IdBook)).Quantity != 0)
            {
                S_Card card = new S_Card()
                {
                    Id = (await repository.Get_Cards()).OrderByDescending(c => c.Id).FirstOrDefault().Id + 1,
                    Id_Student = IdStud,
                    Id_Book = IdBook
                };
                await repository.InsertS_Cards(card);

                return $"ok))";
            }
            else { return "Quantity 0!("; }
        }
        public async Task ReturnBook(int IdBook, int IdStud)
        {
            var tmp = (await repository.Get_Cards()).Where(c => c.Id_Book == IdBook && c.Id_Student == IdStud && c.DateIn is null).FirstOrDefault();
            tmp.DateIn = DateTime.Now;
            await repository.UpdateS_Cards(tmp);

        }
        public async Task DeleteBook(int id) { await repository.DELETEBook(id); }
        public async Task<bool> AddBook(Book book)
        {
            if (book.Name != null && book.Pages != 0 && book.Press.Name != null && book.Quantity != 0 && book.YearPress != 0 && book.Theme.Name != null && book.Category.Name != null && book.Author.FullName != null)
            {
                try
                {

                    if ((await repository.GetPresses()).Where(a => a.Name.ToUpper().Contains(book.Press.Name.ToUpper())).Count() > 0)
                    {
                        book.Press.Id = (await repository.GetPresses()).Where(a => a.Name.ToUpper().Contains(book.Press.Name.ToUpper())).FirstOrDefault().Id;

                    }
                    else
                    {
                        book.Press.Id = repository.GetPresses().Result.Max(p => p.Id) + 1;
                        await repository.AddPress(book.Press);
                    }
                    if ((await repository.GetAuthors()).Where(a => a.FullName.ToUpper().Contains(book.Author.FullName.ToUpper())).Count() > 0)
                    {
                        book.Author.Id = (await repository.GetAuthors()).Where(a => a.FullName.ToUpper().Contains(book.Author.FullName.ToUpper())).FirstOrDefault().Id;
                    }
                    else
                    {
                        book.Author.Id = (await repository.GetAuthors()).Max(p => p.Id) + 1;
                        await repository.AddAutor(book.Author);
                    }
                    if ((await repository.GetCategories()).Where(a => a.Name.ToUpper().Contains(book.Category.Name.ToUpper())).Count() > 0)
                    {
                        book.Category.Id = (await repository.GetCategories()).Where(a => a.Name.ToUpper().Contains(book.Category.Name.ToUpper())).FirstOrDefault().Id;
                    }
                    else
                    {
                        book.Category.Id = (await repository.GetCategories()).Max(p => p.Id) + 1;
                        await repository.AddCategory(book.Category);
                    }
                    if ((await repository.GetThemes()).Where(a => a.Name.ToUpper().Contains(book.Theme.Name.ToUpper())).Count() > 0)
                    {
                        book.Theme.Id = (await repository.GetThemes()).Where(a => a.Name.ToUpper().Contains(book.Theme.Name.ToUpper())).FirstOrDefault().Id;

                    }
                    else
                    {
                        book.Theme.Id = (await repository.GetThemes()).Max(p => p.Id) + 1;
                        await repository.AddAutor(book.Author);
                    }
                    await repository.AddBook(book);
                }

                catch (Exception ex)
                {

                    throw;
                }
                return true;
            }
            else { return false; }


        }





    }
}
