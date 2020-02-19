using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Library.BLL.Abstractions;
using Library.BLL.Model;
using Library.Masseger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Library.PL_WPF.ViewModel
{
    public class AddViewModel : ViewModelBase
    {
        private IBooksService booksService;

        public AddViewModel(IBooksService bs)
        {
            booksService = bs;

            TaskAsync();

        }

        public async Task TaskAsync()
        {
            Presses = await booksService.GetPresses();
            Themes = await booksService.GetThemes();
            Categories = await booksService.GetCategories();
            Authors = await booksService.GetAuthors();
        }

        private ObservableCollection<Press> _presses;
        public ObservableCollection<Press> Presses
        {
            get { return _presses; }
            set { _presses = value; }
        }


        private ObservableCollection<Author> _authors;
        public ObservableCollection<Author> Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }


        private ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set { _categories = value; }
        }

        private ObservableCollection<Theme> _themes;
        public ObservableCollection<Theme> Themes
        {
            get { return _themes; }
            set { _themes = value; }
        }

        public string SelectedPress { get; set; }
        public string SelectedAuthors { get; set; }
        public string SelectedCategories { get; set; }
        public string SelectedThemes { get; set; }
        public string BookName { get; set; }
        public string Pages  { get; set; }
        public string YearPress { get; set; }
        public string Comment { get; set; }
        public string Quantity { get; set; }
       

        public ICommand AddBook_Click => new RelayCommand(async() =>
        {
            Book book = new Book()
            {
                Press = new Press() { Name = SelectedPress },
                Author = new Author() { FullName = SelectedAuthors },
                Category = new Category() { Name = SelectedCategories },
                Theme = new Theme() { Name = SelectedThemes },
                Name=BookName,
                Pages=Convert.ToInt32(Pages),
                YearPress = Convert.ToInt32(YearPress),
                Quantity = Convert.ToInt32(Quantity),
                Comment=Comment
            };
            bool IsNull = await booksService.AddBook(book);
            if (IsNull)
            {
                MessageBox.Show("Book add");
            }
            else
            {
                MessageBox.Show("Fill allthe fields");
            }


             Messenger.Default.Send(new BooksEdit { Book = book  });
        },
            () => { return true; });

    }
}
