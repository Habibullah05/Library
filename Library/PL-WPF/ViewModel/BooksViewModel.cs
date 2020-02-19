using GalaSoft.MvvmLight;
using Library.BLL.Abstractions;
using Library.BLL.Model;
using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.CommandWpf;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using Library.Masseger;
using System.Threading;

namespace Library.PL_WPF.ViewModel
{
    public class BooksViewModel : ViewModelBase
    {
        IBooksService booksService;

        public BooksViewModel(IBooksService bs)
        {
            booksService = bs;

            Init();
            Messenger.Default.Register<BooksEdit>(this, message => { Init(); });



        }

        private async Task Init()
        {
            await GetBooks();
            await GetStuds();
        }


        private ObservableCollection<Book> books;
        public ObservableCollection<Book> Books
        {
            set
            {
                books = value;
                RaisePropertyChanged();
            }
            get { return books; }
        }

        private async Task GetBooks() { Books = await booksService.SearchBooks(SearchTextBook); }

        private ObservableCollection<Student> students;
        public ObservableCollection<Student> Students
        {
            set
            {
                students = value;
                RaisePropertyChanged();

            }
            get { return students; }
        }

        private async Task GetStuds()
        {
            Students = await booksService.SearchStuds(SearchTextStud);

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

        private string _searchTextStud = "";
        public string SearchTextStud
        {
            get { return _searchTextStud; }
            set
            {
                _searchTextStud = value;
                GetStuds();
            }
        }

        private int _IdSelctedBook = -1;
        public Book IdSelctedBook { set { if (value != null) { _IdSelctedBook = value.Id; } } }

        private int _IdSelctedStud = -1;
        public Student IdSelctedStud { set { if (value != null) { _IdSelctedStud = value.Id; } } }

        private ICommand _giveBook;
        public ICommand GiveBook_Click => _giveBook ?? (_giveBook = new RelayCommand(() =>
        {
            if (_IdSelctedBook != -1 && _IdSelctedStud != -1)
            {
                MessageBox.Show(booksService.GiveBook(_IdSelctedBook, _IdSelctedStud).ToString());

                GetBooks();
            }

        }));

    }
}
