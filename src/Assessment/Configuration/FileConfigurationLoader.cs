using Assessment.Resources;
using Assessment.Services;
using System;
using System.IO;

namespace Assessment.Configuration
{
    public class FileConfigurationLoader
    {
        #region Public methods

        public void LoadConfigurationOrders(string pConfigurationFilePath, OrderingConfiguration pOrderingConfiguration)
        {
            if (File.Exists(pConfigurationFilePath) && (pOrderingConfiguration != null))
            {
                pOrderingConfiguration.Orders.Clear();

                using(StreamReader vReader = new StreamReader(pConfigurationFilePath))
                {
                    string[] vConfigurationParts = null;
                    string vConfiguration; // one line

                    Action<string, string> vCheckDirectionAddOrder = (pOrderAscending, pOrderDescending) =>
                    {
                        if (vConfigurationParts[1].Equals("Ascending", StringComparison.InvariantCultureIgnoreCase))
                        {
                            pOrderingConfiguration.AddOrder(pOrderAscending);
                        }
                        else if (vConfigurationParts[1].Equals("Descending", StringComparison.InvariantCultureIgnoreCase))
                        {
                            pOrderingConfiguration.AddOrder(pOrderDescending);
                        }
                    };

                    while ((vConfiguration = vReader.ReadLine()) != null)
                    {
                        vConfigurationParts = vConfiguration.Split(',');

                        if (vConfigurationParts.Length == 2)
                        {
                            if (!string.IsNullOrWhiteSpace(vConfigurationParts[0]) && !string.IsNullOrWhiteSpace(vConfigurationParts[1]))
                            {
                                if (vConfigurationParts[0].Equals("Author", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    vCheckDirectionAddOrder(OrderingConfigurations.AuthorAscending, OrderingConfigurations.AuthorDescending);
                                }
                                else if (vConfigurationParts[0].Equals("Edition", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    vCheckDirectionAddOrder(OrderingConfigurations.EditionAscending, OrderingConfigurations.EditionDescending);
                                }
                                else if (vConfigurationParts[0].Equals("Title", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    vCheckDirectionAddOrder(OrderingConfigurations.TitleAscending, OrderingConfigurations.TitleDescending);
                                }
                            }
                        }

                        continue;
                    }
                }
            }
            else
            {
                throw new OrderingException("Configuration file not found");
            }
        }

        #endregion
    }
}
