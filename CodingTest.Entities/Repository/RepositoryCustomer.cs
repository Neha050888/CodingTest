using CodingTest.Entities.Interface;
using CodingTest.Entities.Models;
using CodingTest.Entities.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest.Entities.Repository
{
    public class RepositoryCustomer : IRepository<Customer>
    {
        public async Task<Customer> SaveToFile(Customer customer, string file)
        {
            string customerId = CodingTestUtility.GenerateUniqueCode();
            string fileName = customerId + "_" + customer.Email + ".txt";
            string filePath = file + $@"\{fileName}";
            customer.CustomerId = customerId;
            customer.FileName = fileName;

            using (var sw = new StreamWriter(filePath))
            {
                String json = JsonConvert.SerializeObject(customer);
                await sw.WriteAsync(json);
            }
            return customer;
        }
    }
}
