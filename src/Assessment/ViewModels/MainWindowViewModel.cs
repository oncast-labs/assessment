using Assessment.Commands;
using Assessment.Configuration;
using Assessment.Models;
using Assessment.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Assessment.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields

        private IIOServices fIOServices;
        private Shelf fShelf;
        private OrderingConfiguration fOrderingConfiguration;
        private BookComparer fBookComparer;
        private string fStatus;

        #endregion

        #region Properties

        public ObservableCollection<OrderingConfigurationItemViewModel> ConfigurationOrders { get; private set; }
        public ObservableCollection<BookViewModel> ShelfBooks { get; private set; }
        public ObservableCollection<BookViewModel> ShelfSortedBooks { get; private set; }

        public string Status
        {
            get
            {
                return fStatus;
            }

            private set
            {
                fStatus = value;
                RaisePropertyChanged();
            }
        }

        public ICommand LoadConfigurationFromFile { get; private set; }

        #endregion

        #region Constructors

        public MainWindowViewModel(IIOServices pIOServices)
        {
            fIOServices = pIOServices;

            fShelf = new Shelf();
            fShelf.AddDefaultBooks();

            fOrderingConfiguration = new OrderingConfiguration();

            fBookComparer = new BookComparer();

            ConfigurationOrders = new ObservableCollection<OrderingConfigurationItemViewModel>();
            ShelfBooks = new ObservableCollection<BookViewModel>();
            ShelfSortedBooks = new ObservableCollection<BookViewModel>();

            LoadConfigurationFromFile = new DelegateCommand(ExecuteLoadConfigurationFromFile);

            UpdateConfiguration();
            UpdateShelf();
        }

        #endregion

        #region Private methods

        private void HandleException(Exception pException)
        {
            string vExceptionType = pException.GetType().Name;
            string vExceptionMessage = pException.Message;

            Status = string.Format("An exception occurred. Type = {0}, Message = {1}", vExceptionType, vExceptionMessage);
        }

        private void UpdateShelf()
        {
            try
            {
                ShelfBooks.Clear();
                foreach (Book vBook in fShelf.Books)
                {
                    ShelfBooks.Add(new BookViewModel(vBook));
                }
                RaisePropertyChanged("ShelfBooks");

                ShelfSortedBooks.Clear();
                foreach (Book vBook in fShelf.GetSortedBookList(fBookComparer))
                {
                    ShelfSortedBooks.Add(new BookViewModel(vBook));
                }
                RaisePropertyChanged("ShelfShelfSortedBooksBooks");

                Status = "Shelf lists updated";
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void UpdateConfiguration()
        {
            try
            { 
                ConfigurationOrders.Clear();
                foreach (OrderingConfigurationItem vConfigurationOrder in fOrderingConfiguration.Orders)
                {
                    ConfigurationOrders.Add(new OrderingConfigurationItemViewModel(vConfigurationOrder));
                }
                RaisePropertyChanged("ConfigurationOrders");

                Status = "Configuration updated";
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void ExecuteLoadConfigurationFromFile()
        {
            try
            {
                if (fIOServices.LoadOrderingConfigurationFromFile(fOrderingConfiguration))
                {
                    fBookComparer.Orders = fOrderingConfiguration.GetOrders();

                    UpdateConfiguration();
                    UpdateShelf();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        #endregion
    }
}
