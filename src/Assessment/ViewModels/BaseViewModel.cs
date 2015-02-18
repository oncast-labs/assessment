using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Assessment.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region Fields

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Protected methods

        protected void RaisePropertyChanged([CallerMemberName] string pPropertyName = "")
        {
            OnPropertyChanged(new PropertyChangedEventArgs(pPropertyName));
        }

        #endregion

        #region Public methods

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs pArgs)
        {
            var vHandler = this.PropertyChanged;

            if (vHandler != null)
            {
                vHandler(this, pArgs);
            }
        }

        #endregion
    }
}
