#region Usings

using System;
using System.ComponentModel;

#endregion

namespace KGySoft.ComponentModelDemo.ViewModel
{
    /// <summary>
    /// Provides arguments for the <see cref="MainViewModel.CommandError"/> event.
    /// </summary>
    public class CommandErrorEventArgs : HandledEventArgs
    {
        #region Properties

        public Exception Exception { get; }
        public string Operation { get; }

        #endregion

        #region Constructors

        public CommandErrorEventArgs(Exception exception, string operation)
        {
            Exception = exception;
            Operation = operation;
        }

        #endregion
    }
}
