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
using System.Windows.Input;

namespace Library.PL_WPF.ViewModel
{
    public class RemoveViewModel : ViewModelBase
    {
        IBooksService booksService;

        public RemoveViewModel(IBooksService bs)
        {
            booksService = bs;
            GetBooks();
            
        }

        private ObservableCollection<Book> books;
        public ObservableCollection<Book> Books
        {
            set
            {

                books = value;
                RaisePropertyChanged();
            }

            get
            {


                return books;
            }

        }


        private async Task GetBooks()
        {

            Books = await booksService.SearchBooks(SearchTextBook);


        }

        private string _searchTextBook = "";

        public string SearchTextBook
        {
            get { return _searchTextBook; }
            set
            {
                _searchTextBook = value;
                GetBooks();

            }
        }

        private int _IdSelctedBook;

        public Book IdSelctedBook { set { _IdSelctedBook = value.Id; } }

        private ICommand _removeBook;
        public ICommand RemoveBook_Click => _removeBook ?? (_removeBook = new RelayCommand(async() =>
        {
           await booksService.DeleteBook(_IdSelctedBook);
           await GetBooks();

            Messenger.Default.Send(new BooksEdit { BookId = _IdSelctedBook });
        }));
    }
}
