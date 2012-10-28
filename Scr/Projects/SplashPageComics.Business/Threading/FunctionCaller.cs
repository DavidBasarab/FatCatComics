using System;

namespace SplashPageComics.Business.Threading
{
    internal class FunctionCaller<T> : BaseCaller<T>
    {
        public FunctionCaller(Func<T> process, Action<T> completeProcess = null)
        {
            Process = process;
            CompleteProcess = completeProcess;
        }

        private Func<T> Process { get; set; }

        public void Execute()
        {
            Process.BeginInvoke(OnComplete, null);
        }

        protected override void FindResult(IAsyncResult asyncResult)
        {
            result = Process.EndInvoke(asyncResult);
        }
    }
}