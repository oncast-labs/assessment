using Assessment.Configuration;
using Microsoft.Win32;
using System;

namespace Assessment.Services
{
    public class IOServices : IIOServices
    {
        #region Public methods

        public bool LoadOrderingConfigurationFromFile(OrderingConfiguration pOrderingConfiguration)
        {
            OpenFileDialog vOpenFileDialog = new OpenFileDialog();
            vOpenFileDialog.DefaultExt = ".cfg";
            vOpenFileDialog.Filter = "Configuration files (*.cfg)|*.cfg|Text files (*.txt)|*.txt";

            Nullable<bool> vResult = vOpenFileDialog.ShowDialog();

            if (vResult.HasValue && vResult.Value)
            {
                FileConfigurationLoader vFileConfigurationLoader = new FileConfigurationLoader();
                vFileConfigurationLoader.LoadConfigurationOrders(vOpenFileDialog.FileName, pOrderingConfiguration);
            }

            return vResult.HasValue && vResult.Value;
        }

        #endregion
    }
}
