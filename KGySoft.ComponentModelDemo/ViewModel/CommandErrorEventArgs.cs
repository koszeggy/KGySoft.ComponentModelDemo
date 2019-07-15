using System;
using System.ComponentModel;

namespace KGySoft.ComponentModelDemo.ViewModel
{
    public class CommandErrorEventArgs : HandledEventArgs
    {
        public Exception Exception { get; }
        public string Operation { get; }

        public CommandErrorEventArgs(Exception exception, string operation)
        {
            Exception = exception;
            Operation = operation;
        }
    }
}
