using System.Windows.Input;

namespace Assessment.Commands
{
    public interface IRaiseCanExecuteChanged
    {
        #region Methods

        void RaiseCanExecuteChanged();

        #endregion
    }

    public static class CommandExtensions
    {
        #region Public static methods

        public static void RaiseCanExecuteChanged(this ICommand pCommand)
        {
            IRaiseCanExecuteChanged vCanExecuteChanged = pCommand as IRaiseCanExecuteChanged;

            if (vCanExecuteChanged != null)
            {
                vCanExecuteChanged.RaiseCanExecuteChanged();
            }
        }

        #endregion
    }
}
