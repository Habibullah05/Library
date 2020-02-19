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
    public class StudentsViewModel : ViewModelBase
    {
        IBooksService booksService;

        public StudentsViewModel(IBooksService bs)
        {
            booksService = bs;

            setstud();
        }

        public async Task setstud()
        {
            await GetStuds();
        }


        private ObservableCollection<Book> books;
        public ObservableCollection<Book> Books
        {
            set { books = value; RaisePropertyChanged(); }
            get {return books;}
        }


        private async Task GetBooks()
        {

            Books = await booksService.GetStudentBooks(_IdSelctedStud);


        }



        private ObservableCollection<Student> students;
        public ObservableCollection<Student> Students
        {
            set
            {
                students = value;
                RaisePropertyChanged();

            }
            get
            {
                return students;
            }
        }

        private async Task GetStuds()
        {

            Students = await booksService.SearchStuds(SearchTextStud);


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

        private int _IdSelctedBook;

        public Book IdSelctedBook { set { if(value!=null) _IdSelctedBook = value.Id; } }

        private int _IdSelctedStud=0;

        public Student IdSelctedStud { set { _IdSelctedStud = value.Id; GetBooks(); } }

        private ICommand _return_Click;
        public ICommand Return_Click => _return_Click ?? (_return_Click = new RelayCommand(async () =>
        {
            int idBook = _IdSelctedBook;
           await booksService.ReturnBook(_IdSelctedBook, _IdSelctedStud);
            // Messenger.Default.Send(new BooksEdit { BookId = idBook });
            await GetBooks();
            //Books.Where(b => b.Id == _IdSelctedBook).FirstOrDefault().Quantity = Books.Where(b => b.Id == _IdSelctedBook).FirstOrDefault().Quantity - 1;
        }));

    }
}
