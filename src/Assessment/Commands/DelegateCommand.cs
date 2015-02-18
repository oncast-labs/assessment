using System;
using System.Windows.Input;

namespace Assessment.Commands
{
    public class DelegateCommand : DelegateCommand<object>
    {
        #region Constructors

        public DelegateCommand(Action pExecute) : base(c => pExecute()) { }

        public DelegateCommand(Action pExecute, Func<bool> pCanExecute) : base(c => pExecute(), c => pCanExecute()) { }

        #endregion
    }

    public class DelegateCommand<T> : ICommand, IRaiseCanExecuteChanged
    {
        #region Fields

        private readonly Func<T, bool> fCanExecute;
        private readonly Action<T> fExecute;
        private bool fIsExecuting;

        #endregion

        #region Properties

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion

        #region Constructors

        public DelegateCommand(Action<T> pExecute) : this(pExecute, null) { }

        public DelegateCommand(Action<T> pExecute, Func<T, bool> pCanExecute)
        {
            if ((pExecute == null) && (pCanExecute == null))
            {
                throw new ArgumentNullException("pExecute", "Execute Method cannot be null");
            }

            fExecute = pExecute;
            fCanExecute = pCanExecute;
        }

        #endregion

        #region ICommand explicit members

        bool ICommand.CanExecute(object pParameter)
        {
            return !fIsExecuting && CanExecute((T)pParameter);
        }

        void ICommand.Execute(object pParameter)
        {
            fIsExecuting = true;

            try
            {
                RaiseCanExecuteChanged();
                Execute((T)pParameter);
            }
            finally
            {
                fIsExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Public methods

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public bool CanExecute(T pParameter)
        {
            if (fCanExecute == null)
            {
                return true;
            }

            return fCanExecute(pParameter);
        }

        public void Execute(T pParameter)
        {
            fExecute(pParameter);
        }

        #endregion
    }
}
