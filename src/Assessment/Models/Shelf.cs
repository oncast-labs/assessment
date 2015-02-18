using Assessment.Services;
using System.Collections.Generic;
using System.Linq;

namespace Assessment.Models
{
    public class Shelf
    {
        #region Fields

        private List<Book> fBooks;

        #endregion

        #region Properties

        public List<Book> Books
        {
            get
            {
                return fBooks;
            }
        }

        #endregion

        #region Constructors

        public Shelf()
        {
            fBooks = new List<Book>();
        }

        #endregion

        #region Public methods

        public void Clear()
        {
            fBooks.Clear();
        }

        public void Add(Book pBook)
        {
            fBooks.Add(pBook);
        }

        public void AddDefaultBooks()
        {
            fBooks.Add(new Book("Java How to Program", "Deitel & Deitel", 2007));
            fBooks.Add(new Book("Patterns of Enterprise Application Architecture", "Martin Fowler", 2002));
            fBooks.Add(new Book("Head First Design Patterns", "Elisabeth Freeman", 2004));
            fBooks.Add(new Book("Internet & World Wide Web: How to Program", "Deitel & Deitel", 2007));
        }

        public List<Book> GetSortedBookList(IBookComparer pBookComparer)
        {
            if (pBookComparer == null)
            {
                throw new OrderingException("Book comparer cannot be null!");
            }
            else if (pBookComparer.Orders == null)
            {
                throw new OrderingException("Book comparer orders cannot be null!");
            }
            else
            {
                List<Book> vSortedBooks = new List<Book>();

                if (pBookComparer.Orders.Any())
                {
                    vSortedBooks.AddRange(fBooks);
                    vSortedBooks.Sort(pBookComparer);
                }

                return vSortedBooks;
            }
        }

        #endregion
    }
}
