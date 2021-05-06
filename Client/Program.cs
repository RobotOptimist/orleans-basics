using Orleans;
using Orleans.Configuration;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using GrainInterfaces;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                using (var client = await ConnectClient())
                {
                    await DoClientWork(client);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" \n Something went wrong in the client omg {ex.Message}");
                Console.WriteLine("Make sure the silo the client is trying to connect to is running");
                Console.WriteLine("\n Press any key to exit");
                Console.ReadKey();
                throw;
            }
        }

        private static async Task<IClusterClient> ConnectClient()
        {
            IClusterClient client;
            client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "OrleansBasics";
                })
                .ConfigureLogging(logging => logging.AddConsole())
                .Build();
            await client.Connect();
            Console.WriteLine("Client successfully connected to silo host \n");
            return client;
        }

        private static async Task DoClientWork(IClusterClient client)
        {
            var friend = client.GetGrain<IHello>(0);
            var response = await friend.SayHello("Hi there Hello Grain!");
            Console.WriteLine($"\n\n {response} \n\n");
        }
    }
}
