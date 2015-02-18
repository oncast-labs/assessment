using Assessment.Configuration;

namespace Assessment.ViewModels
{
    public class OrderingConfigurationItemViewModel: BaseViewModel
    {
        #region Fields

        private OrderingConfigurationItem fOrderingConfigurationItem;

        #endregion

        #region Properties

        public OrderingConfigurationItem OrderingConfigurationItem
        {
            get
            {
                return fOrderingConfigurationItem;
            }

            set
            {
                fOrderingConfigurationItem = value;
                RaisePropertyChanged();
                RaisePropertyChanged("OrderingConfigurationItemData");
            }
        }

        public string OrderingConfigurationItemData
        {
            get
            {
                return string.Format("{0}, {1}", fOrderingConfigurationItem.Property, fOrderingConfigurationItem.Direction.ToString());
            }
        }

        #endregion

        #region Constructor

        public OrderingConfigurationItemViewModel(OrderingConfigurationItem pOrderingConfigurationItem)
        {
            OrderingConfigurationItem = pOrderingConfigurationItem;
        }

        #endregion
    }
}
