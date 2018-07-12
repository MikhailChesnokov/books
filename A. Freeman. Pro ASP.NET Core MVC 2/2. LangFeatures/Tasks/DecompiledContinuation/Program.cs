// Decompiled with JetBrains decompiler
// Type: Tasks.Program
// Assembly: Tasks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 05DC883E-11FF-47E3-8D7D-B11BD7B86EED
// Assembly location: C:\Users\MIKHAIL\Documents\GitHub\books\A. Freeman. Pro ASP.NET Core MVC 2\2. LangFeatures\Tasks\Tasks\bin\Debug\netcoreapp2.1\Tasks.dll
// Compiler-generated code is shown

using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DecompiledContinuation
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine((object)Program.GetPageLength().Result);
        }

        private static Task<long?> GetPageLength()
        {
            // ISSUE: method pointer
            return new HttpClient().GetAsync("http://apress.com").ContinueWith<long?>(Program.c.TaskFunc ?? (Program.c.TaskFunc = new Func<Task<HttpResponseMessage>, long?>(Program.c.C.ContinueWith)));
        }

        public Program() : base()
        {
        }

        [CompilerGenerated]
        [Serializable]
        private sealed class c
        {
            public static readonly Program.c C;
            public static Func<Task<HttpResponseMessage>, long?> TaskFunc;

            static c()
            {
                Program.c.C = new Program.c();
            }

            public c() : base()
            {
            }

            internal long? ContinueWith(Task<HttpResponseMessage> message)
            {
                return message.Result.Content.Headers.ContentLength;
            }
        }
    }
}
