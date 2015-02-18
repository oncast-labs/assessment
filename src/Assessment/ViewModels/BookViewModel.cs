using Assessment.Models;

namespace Assessment.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        #region Fields

        private Book fBook;

        #endregion

        #region Properties

        public Book Book
        {
            get
            {
                return fBook;
            }

            set
            {
                fBook = value;
                RaisePropertyChanged();
                RaisePropertyChanged("BookData");
            }
        }

        public string BookData
        {
            get
            {
                return string.Format("{0}, {1} - {2}", fBook.Title, fBook.Author, fBook.Edition);
            }
        }

        #endregion

        #region Constructors

        public BookViewModel(Book pBook)
        {
            Book = pBook;
        }

        #endregion
    }
}
