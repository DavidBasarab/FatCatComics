using System;
using System.Threading;

namespace SplashPageComics.Business.Threading
{
    public class SimpleThreadManagement : ThreadManagement
    {
        private static void ExectueFunction<T>(Func<T> process, Action<T> completeProcess = null)
        {
            var caller = new FunctionCaller<T>(process, completeProcess);

            caller.Execute();
        }

        public void ExecuteInSeparateThread<T>(Func<T> process)
        {
            ExectueFunction(process);
        }

        public void ExecuteInSeparateThread(Action process)
        {
            process.BeginInvoke(null, null);
        }

        public void ExecuteInSeparateThread(Action process, Action completeProcess)
        {
            AsyncCallback callback = r => completeProcess();

            process.BeginInvoke(callback, completeProcess);
        }

        public void ExecuteInSeparateThread<T>(Func<T> process, Action<T> completeProcess = null)
        {
            ExectueFunction(process, completeProcess);
        }

        public void ExecuteInSeparateThread<T>(Action<T> process, T argument, Action completeProcess)
        {
            VoidCompleteFunctionCaller<T>.Run(process, argument, completeProcess);
        }

        public void ExecuteInSeparateThread<T1, T2>(Action<T1, T2> process, T1 argument1, T2 argument2, Action completeProcess)
        {
            VoidCompleteFunctionCaller<T1, T2>.Run(process, argument1, argument2, completeProcess);
        }

        public void ExecuteInSeparateThread<T>(Action<T> process, T argument)
        {
            process.BeginInvoke(argument, null, null);
        }

        public void ExecuteInSeparateThread<T1, T2, T3>(Action<T1, T2, T3> process, T1 argument1, T2 argument2, T3 argument3)
        {
            process.BeginInvoke(argument1, argument2, argument3, null, null);
        }

        public void ExecuteInSeparateThread<I, O>(Func<I, O> process, I inputParameter, Action<O> completeProcess)
        {
            var caller = new InputFunctionCaller<I, O>(process, completeProcess);

            caller.Execute(inputParameter);
        }

        public void Sleep(TimeSpan sleepTime)
        {
            new ManualResetEvent(false).WaitOne(sleepTime);
        }
    }
}