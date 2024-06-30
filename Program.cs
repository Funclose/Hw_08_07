using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Xml.Linq;
namespace hw_08._07
{
    internal class Program
    {
        public class Currency()
        { 
            public string City { get; set; }
            public string Country { get; set; }
            public string Region { get; set; }
        }
        static async Task GetDataFromServer(string ip)
        {
            string url = $"http://ipwho.is/{ip}";
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                if (content.Length > 0)
                {

                    var data = JsonConvert.DeserializeObject<Currency>(content);

                    if (data != null)
                    {
                        Console.WriteLine($"City: {data.City}");
                        Console.WriteLine($"Country: {data.Country}");
                        Console.WriteLine($"region: {data.Region}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Currency.");
                    }
                    string file = @".\ip_data.json";
                    File.WriteAllText(file, content);
                    Console.WriteLine($"IP data has been written to {file}");
                }
            }
        }
            static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the IP address:");
            string ip = Console.ReadLine();
            await GetDataFromServer(ip);
        }
    }
}
