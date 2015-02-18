using Assessment.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Assessment.Configuration
{
    public class OrderingConfigurationItem
    {
        #region Properties

        public string Property { get; set; }
        public Direction Direction { get; set; }

        #endregion
    }

    public class OrderingConfiguration
    {
        #region Properties

        public List<OrderingConfigurationItem> Orders { get; set; }

        #endregion

        #region Constructors

        public OrderingConfiguration()
        {
            Orders = new List<OrderingConfigurationItem>();
        }

        #endregion

        #region Public static methods

        public static OrderingConfiguration GetOrderingConfiguration(string pJsonString)
        {
            return JsonConvert.DeserializeObject<OrderingConfiguration>(pJsonString);
        }

        #endregion

        #region Public methods

        public void Clear()
        {
            Orders.Clear();
        }

        public void AddOrder(string pOrder)
        {
            Orders.Add(JsonConvert.DeserializeObject<OrderingConfigurationItem>(pOrder));
        }

        public void AddOrders(params string[] pOrders)
        {
            foreach(string vOrder in pOrders)
            {
                AddOrder(vOrder);
            }
        }

        public List<Order> GetOrders()
        {
            List<Order> vOrders = null;

            if (Orders != null)
            {
                vOrders = new List<Order>();
                Order vOrder;

                foreach (OrderingConfigurationItem vOrderConfigurationItem in Orders)
                {
                    vOrder = new Order();
                    vOrder.OrderingDirection = vOrderConfigurationItem.Direction;
                    vOrder.PropertyName = vOrderConfigurationItem.Property;

                    if (vOrder.PropertyName.Equals("Edition", StringComparison.InvariantCultureIgnoreCase))
                    {
                        vOrder.PropertyType = typeof(int);
                    }
                    else
                    {
                        vOrder.PropertyType = typeof(string);
                    }

                    vOrders.Add(vOrder);
                }
            }

            return vOrders;
        }

        #endregion
    }
}
