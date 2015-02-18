using Assessment.Configuration;
using System.Collections.Generic;

namespace Assessment.Services
{
    public interface IIOServices
    {
        #region Methods

        bool LoadOrderingConfigurationFromFile(OrderingConfiguration pOrderingConfiguration);

        #endregion
    }
}
