using Assessment.Models;
using System.Collections.Generic;

namespace Assessment.Services
{
    public class BookComparer : IBookComparer
    {
        #region Properties

        public List<Order> Orders { get; set; }

        #endregion

        #region Private methods

        private object GetPropertyValue(Book pBook, string pPropertyName)
        {
            return pBook.GetType().GetProperty(pPropertyName).GetValue(pBook);
        }

        #endregion

        #region Public methods

        public int Compare(Book pBookX, Book pBookY)
        {
            if (pBookX == null)
            {
                return pBookY == null ? 0 : -1;
            }
            else
            {
                if (pBookY == null)
                {
                    return 1;
                }
                else
                {
                    int vCompareResult = 0;
                    foreach (Order vOrder in Orders)
                    {
                        if (vOrder.PropertyType.Equals(typeof(string)))
                        {
                            string vStringPropertyX = GetPropertyValue(pBookX, vOrder.PropertyName) as string;
                            string vStringPropertyY = GetPropertyValue(pBookY, vOrder.PropertyName) as string;

                            vCompareResult = vStringPropertyX.CompareTo(vStringPropertyY);
                        }
                        else if (vOrder.PropertyType.Equals(typeof(int)))
                        {
                            int vIntPropertyX = (int)GetPropertyValue(pBookX, vOrder.PropertyName);
                            int vIntPropertyY = (int)GetPropertyValue(pBookY, vOrder.PropertyName);

                            vCompareResult = vIntPropertyX.CompareTo(vIntPropertyY);
                        }

                        if (vCompareResult != 0)
                        {
                            if (vOrder.OrderingDirection == Direction.Descending)
                            {
                                vCompareResult *= -1;
                            }

                            break;
                        }
                    }

                    return vCompareResult;
                }
            }
        }

        #endregion
    }
}
