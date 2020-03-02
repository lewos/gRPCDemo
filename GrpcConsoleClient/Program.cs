using Grpc.Core;
using Grpc.Net.Client;
using GrpcService;
using System;
using System.Threading.Tasks;

namespace GrpcConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //// configuracion del canal, escuchando al servidor de grpc
            //var channel = GrpcChannel.ForAddress("https://localhost:5001");

            //var client = new Greeter.GreeterClient(channel);

            //var input = new HelloRequest { Name="Leo" };

            //var reply = await client.SayHelloAsync(input);

            //Console.WriteLine(reply.Message);

            //-----------------------------------------------------
            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var client = new Customer.CustomerClient(channel);

            var input = new CustomerLookupModel { UserId = 2 };

            var reply = await client.GetCustomerInfoAsync(input);

            Console.WriteLine($"{reply.FirstName}, {reply.LastName}");
            Console.WriteLine("---------------");

            //-----------------------------------------------------
            using (var call = client.GetNewCustomers(new NewCustomerRequest())) 
            {
                while (await call.ResponseStream.MoveNext()) 
                {
                    var currentCustomer = call.ResponseStream.Current;

                    Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName}");

                }
            }


            Console.ReadLine();
        }
    }
}
