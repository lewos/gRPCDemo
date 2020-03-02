using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.FirstName = "firstName1";
                output.LastName = "lastName1";

            }
            else if (request.UserId == 2)
            {
                output.FirstName = "firstName2";
                output.LastName = "lastName2";
            }
            else 
            {
                output.FirstName = "firstNameOther";
                output.LastName = "lastNameOther";
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, 
            IServerStreamWriter<CustomerModel> responseStream, 
            ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    Age = 5,
                    EmailAdress = "algo@gmail.com",
                    FirstName = "leo",
                    IsAlive = true,
                    LastName = "carmi"
                },
                new CustomerModel
                {
                    Age = 4,
                    EmailAdress = "grpc@gmail.com",
                    FirstName = "nico",
                    IsAlive = true,
                    LastName = "carmi"
                },
                new CustomerModel
                {
                    Age = 7,
                    EmailAdress = "creo@gmail.com",
                    FirstName = "jose",
                    IsAlive = true,
                    LastName = "carmi"
                }
            };

            foreach(var c in customers) 
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(c);
            }
        }
    }
}
