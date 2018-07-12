// Decompiled with JetBrains decompiler
// Type: Tasks.Program
// Assembly: Tasks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 966558BD-A81B-47DB-9702-EFA8E778244F
// Assembly location: C:\Users\MIKHAIL\Documents\GitHub\books\A. Freeman. Pro ASP.NET Core MVC 2\2. LangFeatures\Tasks\Tasks\bin\Debug\netcoreapp2.1\Tasks.dll
// Compiler-generated code is shown

using System;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DecompiledAsync
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine((object)Program.GetPageLengthAsync().Result);
        }

        [AsyncStateMachine(typeof(Program.AsyncStateMachine))]
        [DebuggerStepThrough]
        private static Task<long?> GetPageLengthAsync()
        {
            Program.AsyncStateMachine stateMachine = new Program.AsyncStateMachine();
            stateMachine.Builder = AsyncTaskMethodBuilder<long?>.Create();
            stateMachine.State = -1;
            stateMachine.Builder.Start<Program.AsyncStateMachine>(ref stateMachine);
            return stateMachine.Builder.Task;
        }

        public Program() : base()
        {
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

            public AsyncStateMachine() : base()
            {
            }

            void IAsyncStateMachine.MoveNext()
            {
                int num1 = this.State;
                long? contentLength;
                try
                {
                    TaskAwaiter<HttpResponseMessage> awaiter;
                    int num2;
                    if (num1 != 0)
                    {
                        this._client = new HttpClient();
                        awaiter = this._client.GetAsync("http://apress.com").GetAwaiter();
                        if (!awaiter.IsCompleted)
                        {
                            this.State = num2 = 0;
                            this._awaiter = awaiter;
                            Program.AsyncStateMachine stateMachine = this;
                            this.Builder.AwaitUnsafeOnCompleted<TaskAwaiter<HttpResponseMessage>, Program.AsyncStateMachine > (ref awaiter, ref stateMachine);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this._awaiter;
                        this._awaiter = new TaskAwaiter<HttpResponseMessage>();
                        this.State = num2 = -1;
                    }
                    this._responce2 = awaiter.GetResult();
                    this._responce1 = this._responce2;
                    this._responce2 = (HttpResponseMessage)null;
                    contentLength = this._responce1.Content.Headers.ContentLength;
                }
                catch (Exception ex)
                {
                    this.State = -2;
                    this.Builder.SetException(ex);
                    return;
                }
                this.State = -2;
                this.Builder.SetResult(contentLength);
            }

            [DebuggerHidden]
            void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
            {
            }
        }
    }
}