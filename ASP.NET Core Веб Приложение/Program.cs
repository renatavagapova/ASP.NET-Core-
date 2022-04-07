using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ASP.NET_Core_Веб_Приложение
{
    internal class Program
    {
        private static readonly CancellationTokenSource cts = new CancellationTokenSource();
        private static readonly HttpClient client = new HttpClient();
        public static async Task Main(string[] args)
        {
            try
            {
                await PostPars(@"https://jsonplaceholder.typicode.com/posts/", 4, 13);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally
            {
                cts.Dispose();
            }
        }

        static async Task PostPars(string url, int min, int max)
        {
            string responseBody = "";

            for (int i = min; i <= max; i++)
            {
                var response = await client.GetAsync(url + i.ToString());
                response.EnsureSuccessStatusCode();

                responseBody += "\n";
                responseBody += "\n";
                responseBody += await response.Content.ReadAsStringAsync();
            }

            Console.WriteLine(responseBody);

            using (FileStream fs = new FileStream($"result.txt", FileMode.OpenOrCreate))
            {
                byte[] ar = System.Text.Encoding.UTF8.GetBytes(responseBody);
                fs.Write(ar, 0, ar.Length);
                Console.WriteLine("Текст записан в файл 'result.txt'");
            }
        }
    }
}
