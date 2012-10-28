using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplashPageComics.Business.Threading
{
    public interface ThreadManagement
    {
        void ExecuteInSeparateThread<T>(Func<T> process);

        void ExecuteInSeparateThread(Action process);

        void ExecuteInSeparateThread(Action process, Action completeProcess);

        void ExecuteInSeparateThread<T>(Func<T> process, Action<T> completeProcess);

        void ExecuteInSeparateThread<T>(Action<T> process, T argument, Action completeProcess);

        void ExecuteInSeparateThread<T1, T2>(Action<T1, T2> process, T1 argument1, T2 argument2, Action completeProcess);

        void ExecuteInSeparateThread<T>(Action<T> process, T argument);

        void ExecuteInSeparateThread<T1, T2, T3>(Action<T1, T2, T3> process, T1 argument1, T2 argument2, T3 argument3);

        void ExecuteInSeparateThread<I, O>(Func<I, O> process, I inputParameter, Action<O> completeProcess);

        void Sleep(TimeSpan sleepTime);
    }

    internal class InputFunctionCaller<I, O> : BaseCaller<O>
    {
        public InputFunctionCaller(Func<I, O> process, Action<O> completeProcess = null)
        {
            Process = process;
            CompleteProcess = completeProcess;
        }

        private Func<I, O> Process { get; set; }

        public void Execute(I inputParameter)
        {
            Process.BeginInvoke(inputParameter, OnComplete, null);
        }

        protected override void FindResult(IAsyncResult asyncResult)
        {
            result = Process.EndInvoke(asyncResult);
        }
    }

    internal class VoidCompleteFunctionCaller<INPUT>
    {
        public static void Run(Action<INPUT> process, INPUT argument, Action completeProcess)
        {
            var caller = new VoidCompleteFunctionCaller<INPUT>(process, argument, completeProcess);

            caller.Execute();
        }

        private VoidCompleteFunctionCaller(Action<INPUT> process, INPUT argument, Action completeProcess)
        {
            Process = process;
            CompleteProcess = completeProcess;
            Argument = argument;
        }

        public Action<INPUT> Process { get; set; }
        public Action CompleteProcess { get; set; }
        public INPUT Argument { get; set; }

        private void Execute()
        {
            Process.BeginInvoke(Argument, OnComplete, null);
        }

        private void OnComplete(IAsyncResult asyncResult)
        {
            CompleteProcess();
        }
    }

    internal class VoidCompleteFunctionCaller<INPUT1, INPUT2>
    {
        public static void Run(Action<INPUT1, INPUT2> process, INPUT1 argument1, INPUT2 argument2, Action completeProcess)
        {
            var caller = new VoidCompleteFunctionCaller<INPUT1, INPUT2>(process, argument1, argument2, completeProcess);

            caller.Execute();
        }

        private VoidCompleteFunctionCaller(Action<INPUT1, INPUT2> process, INPUT1 argument1, INPUT2 argument2, Action completeProcess)
        {
            Process = process;
            CompleteProcess = completeProcess;
            Argument1 = argument1;
            Argument2 = argument2;
        }

        public Action<INPUT1, INPUT2> Process { get; set; }
        public Action CompleteProcess { get; set; }
        public INPUT1 Argument1 { get; set; }
        public INPUT2 Argument2 { get; set; }

        private void Execute()
        {
            Process.BeginInvoke(Argument1, Argument2, OnComplete, null);
        }

        private void OnComplete(IAsyncResult asyncResult)
        {
            CompleteProcess();
        }
    }
}
