using GalaSoft.MvvmLight;
using System.Windows.Controls;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Library.BLL.Abstractions;
using Library.BLL.Services;

namespace Library.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

       
        IBooksService booksService ;
        private Pages _current;

        public Pages CurrentPage
        {
            get { return _current; }
            set { _current = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel(IBooksService bs)
        {
            booksService = bs;
            CurrentPage = Pages.Books;

           // CurrentPage = Add_book;
           ////if (IsInDesignMode)
           ////{
           ////    // Code runs in Blend --> create design time data.
           ////}
           ////else
           ////{
           ////    // Code runs "for real"
           ////}
        }


        private ICommand books;
        public ICommand Books_Click => books ?? (books = new RelayCommand(() => { if (CurrentPage != Pages.Books) CurrentPage = Pages.Books; }));

        private ICommand studs;
        public ICommand Studs_Click => studs ?? (studs = new RelayCommand(() => { if (CurrentPage != Pages.Studs) CurrentPage = Pages.Studs; }));

        private ICommand add;
        public ICommand Add_Book_Click => add ?? (add = new RelayCommand(() => { if (CurrentPage != Pages.Add_Book) CurrentPage = Pages.Add_Book; }));
        private ICommand remove;
        public ICommand Remove_Book_Click => remove ?? (remove = new RelayCommand(() => { if (CurrentPage != Pages.Remove) CurrentPage = Pages.Remove; }));


    }

    public enum Pages
    {
        Books,
        Studs,
        Add_Book,
        Remove
    }
}