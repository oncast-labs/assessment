using Assessment.Models;
using System.Collections.Generic;

namespace Assessment.Services
{
    public interface IBookComparer : IComparer<Book>
    {
        #region Properties

        List<Order> Orders { get; set; }

        #endregion
    }
}
