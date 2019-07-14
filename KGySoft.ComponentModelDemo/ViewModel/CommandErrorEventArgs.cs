using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
