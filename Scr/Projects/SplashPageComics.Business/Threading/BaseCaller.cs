using System;

namespace SplashPageComics.Business.Threading
{
    internal abstract class BaseCaller<O>
    {
        protected O result;
        protected Action<O> CompleteProcess { get; set; }

        protected abstract void FindResult(IAsyncResult asyncResult);

        protected void OnComplete(IAsyncResult asyncResult)
        {
            FindResult(asyncResult);

            ExecuteCompleteProcess();
        }

        private void ExecuteCompleteProcess()
        {
            if (CompleteProcess != null) CompleteProcess(result);
        }
    }
}