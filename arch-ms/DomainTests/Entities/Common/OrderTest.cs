using Bogus;
using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace DomainTests.Entities.Common
{
    public class OrderTest
    {   
        [Fact]
        public async Task testBogus()
        {


            Randomizer.Seed = new Random(1209837);

            var billingDetails = new Faker<BillingDetails>()
              .RuleFor(detail => detail.CustumerName, faker => faker.Person.FullName)
              .RuleFor(detail => detail.Email, faker => faker.Person.Email)
              .RuleFor(detail => detail.Phone, faker => faker.Person.Phone)
              .RuleFor(detail => detail.AddressLine, faker => faker.Address.StreetAddress())
              .RuleFor(detail => detail.City, faker => faker.Address.City())
              .RuleFor(detail => detail.Country, faker => faker.Address.Country())
              .RuleFor(detail => detail.PostCode, faker => faker.Address.ZipCode());

            var orderFaker = new Faker<Order>()
               .RuleFor(order => order.Id, Guid.NewGuid())
               .RuleFor(order => order.Currency, faker => faker.Finance.Currency().Symbol)
               .RuleFor(order => order.Price, faker => faker.Finance.Amount(5, 100))
               .RuleFor(order => order.BillingDetails, faker => billingDetails);

            foreach (var order in orderFaker.GenerateForever())
            {
                var text1 = JsonSerializer.Serialize(order);
                Console.WriteLine(text1);
                await Task.Delay(1000);

            }

           




            var text = JsonSerializer.Serialize(orderFaker.Generate());
   
        }
    }
}
