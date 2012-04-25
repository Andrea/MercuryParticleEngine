namespace Sanford.Threading
{
    using System;
    using System.Threading;

    public class AsyncResult : IAsyncResult
    {
        private AsyncCallback callback;
        private bool completedSynchronously;
        private bool isCompleted;
        private object owner;
        private object state;
        private int threadId;
        private ManualResetEvent waitHandle = new ManualResetEvent(false);

        public AsyncResult(object owner, AsyncCallback callback, object state)
        {
            this.owner = owner;
            this.callback = callback;
            this.state = state;
            this.threadId = Thread.CurrentThread.ManagedThreadId;
        }

        public void Signal()
        {
            this.isCompleted = true;
            this.completedSynchronously = this.threadId == Thread.CurrentThread.ManagedThreadId;
            this.waitHandle.Set();
            if (this.callback != null)
            {
                this.callback(this);
            }
        }

        public object AsyncState
        {
            get
            {
                return this.state;
            }
        }

        public WaitHandle AsyncWaitHandle
        {
            get
            {
                return this.waitHandle;
            }
        }

        public bool CompletedSynchronously
        {
            get
            {
                return this.completedSynchronously;
            }
        }

        public bool IsCompleted
        {
            get
            {
                return this.isCompleted;
            }
        }

        public object Owner
        {
            get
            {
                return this.owner;
            }
        }
    }
}

