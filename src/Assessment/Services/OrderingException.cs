using System;

namespace Assessment.Services
{
    public class OrderingException : Exception
    {
        #region Constructors

        public OrderingException(string pMessage): base(pMessage)
        {

        }

        #endregion
    }
}
