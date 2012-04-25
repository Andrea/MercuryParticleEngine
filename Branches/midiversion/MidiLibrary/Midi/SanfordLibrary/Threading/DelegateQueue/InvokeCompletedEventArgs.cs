namespace Sanford.Threading
{
    using System;

    public class InvokeCompletedEventArgs : EventArgs
    {
        private object[] args;
        private Exception error;
        private Delegate method;
        private object result;

        public InvokeCompletedEventArgs(Delegate method, object[] args, object result, Exception error)
        {
            this.method = method;
            this.args = args;
            this.result = result;
            this.error = error;
        }

        public object[] GetArgs()
        {
            return this.args;
        }

        public Exception Error
        {
            get
            {
                return this.error;
            }
        }

        public Delegate Method
        {
            get
            {
                return this.method;
            }
        }

        public object Result
        {
            get
            {
                return this.result;
            }
        }
    }
}

