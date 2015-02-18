
namespace Assessment.Models
{
    public class Book
    {
        #region Properties

        public string Title { get; set; }
        public string Author { get; set; }
        public int Edition { get; set; }

        #endregion

        #region Constructors

        public Book(string pTitle, string pAuthor, int pEdition)
        {
            Title = pTitle;
            Author = pAuthor;
            Edition = pEdition;
        }

        #endregion
    }
}
