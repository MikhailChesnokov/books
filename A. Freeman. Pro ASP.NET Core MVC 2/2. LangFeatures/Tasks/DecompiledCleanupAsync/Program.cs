using System;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DecompiledCleanupAsync
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine(GetPageLengthAsync().Result);
        }

        [AsyncStateMachine(typeof(AsyncStateMachine))]
        [DebuggerStepThrough]
        private static Task<long?> GetPageLengthAsync()
        {
            var stateMachine = new AsyncStateMachine
            {
                Builder = AsyncTaskMethodBuilder<long?>.Create(),
                State = -1
            };
            stateMachine.Builder.Start(ref stateMachine);
            return stateMachine.Builder.Task;
        }

        [CompilerGenerated]
        private sealed class AsyncStateMachine : IAsyncStateMachine
        {
            public int State;
            public AsyncTaskMethodBuilder<long?> Builder;
            private HttpClient _client;
            private HttpResponseMessage _responce1;
            private HttpResponseMessage _responce2;
            private TaskAwaiter<HttpResponseMessage> _awaiter;

            void IAsyncStateMachine.MoveNext()
            {
                int num1 = State;
                long? contentLength;
                try
                {
                    TaskAwaiter<HttpResponseMessage> awaiter;
                    if (num1 != 0)
                    {
                        _client = new HttpClient();
                        awaiter = _client.GetAsync("http://apress.com").GetAwaiter();
                        if (!awaiter.IsCompleted)
                        {
                            State = 0;
                            _awaiter = awaiter;
                            AsyncStateMachine stateMachine = this;
                            Builder.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = _awaiter;
                        _awaiter = new TaskAwaiter<HttpResponseMessage>();
                        State = -1;
                    }
                    _responce2 = awaiter.GetResult();
                    _responce1 = _responce2;
                    _responce2 = null;
                    contentLength = _responce1.Content.Headers.ContentLength;
                }
                catch (Exception ex)
                {
                    State = -2;
                    Builder.SetException(ex);
                    return;
                }
                State = -2;
                Builder.SetResult(contentLength);
            }

            [DebuggerHidden]
            void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
            {
            }
        }
    }
}