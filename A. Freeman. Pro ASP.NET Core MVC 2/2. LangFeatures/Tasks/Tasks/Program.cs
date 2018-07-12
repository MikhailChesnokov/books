using System;

namespace Tasks
{
    using System.Net.Http;
    using System.Threading.Tasks;



    class Program
    {
        static void Main()
        {
            //Console.WriteLine(GetPageLength().Result);
            Console.WriteLine(GetPageLengthAsync().Result);
        }

        //static Task<long?> GetPageLength()
        //{
        //    var client = new HttpClient();

        //    Task<HttpResponseMessage> httpTask = client.GetAsync("http://apress.com");

        //    return httpTask.ContinueWith((Task<HttpResponseMessage> message) => message.Result.Content.Headers.ContentLength);
        //}

        static async Task<long?> GetPageLengthAsync()
        {
            var client = new HttpClient();

            HttpResponseMessage message = await client.GetAsync("http://apress.com");

            return message.Content.Headers.ContentLength;
        }
    }
}
