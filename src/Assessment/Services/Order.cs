using System;

namespace Assessment.Services
{
    public enum Direction
    {
        Ascending = 0,
        Descending = 1
    }

    public class Order
    {
        #region Properties

        public string PropertyName { get; set; }
        public Type PropertyType { get; set; }
        public Direction OrderingDirection { get; set; }

        #endregion
    }
}
