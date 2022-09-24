﻿using Bogus;
using Models;
using ModelsDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TestDataGenerator
    {
        public Faker<Currency> CreateCurrency()
        {
            return new Faker<Currency>()
                 .RuleFor(x => x.CurrencyId, f => Guid.NewGuid())
                 .RuleFor(x => x.Code, f => f.Random.Int(1000000, 9999999))
                 .RuleFor(x => x.Name, f => f.Random.ArrayElement(new[]
                 {
                     "USD",
                     "RUB"
                 }));
        }
        public Faker<Account> CreateAccount()
        {
            Faker<Currency> generatorCurrency = CreateCurrency();
            Currency currency = generatorCurrency.Generate(1)[0];

            return new Faker<Account>()
                 .RuleFor(x => x.Currency, f => currency)
                 .RuleFor(x => x.Amount, f => f.Random.Int(1000000, 9999999))
                 .RuleFor(x => x.AccountId, f => Guid.NewGuid());
        }

        public Dictionary<Client, List<Account>> CreateClientDictionaryWithAccount(List<Client> clients)
        {
            Faker<Account> generatorAccount = CreateAccount();
            List<Account> accounts = generatorAccount.Generate(1000);

            var newDictionary = new Dictionary<Client, List <Account>>();

            foreach (Client client in clients)
            {
                var valueAccountsList = new List<Account>()
                  { accounts[new Random().Next(accounts.Count)],
                    accounts[new Random().Next(accounts.Count)],
                    accounts[new Random().Next(accounts.Count)]
                   };
                newDictionary[client] = valueAccountsList;
            };

            return newDictionary;
        }

        public Dictionary<string, Client> CreateClientDictionaryWithPhone(List<Client> clients)
        {
            var dictionary = new Dictionary<string, Client>();

            foreach (Client client in clients)
            {
                dictionary[client.Phone] = client;
            }
            return dictionary;
        }
        public Faker<Client> CreateClientListGenerator()
        {
            Faker<Account> generatorAccount = CreateAccount();
            List<Account> accounts = generatorAccount.Generate(2);         

            return new Faker<Client>("ru")
                .RuleFor(x => x.ClientId, f => Guid.NewGuid())
                .RuleFor(x => x.FirstName, f => f.Name.LastName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())                
                .RuleFor(x => x.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(x => x.DateOfBirth, f => f.Date.BetweenDateOnly(new DateOnly(1940, 1, 1), new DateOnly(2003, 1, 1)).ToDateTime(TimeOnly.MinValue).ToUniversalTime())
                .RuleFor(x => x.NumberOfPassport, f => f.Random.Int(10000, 99999))
                .RuleFor(x => x.SeriesOfPassport, f => f.Random.Int(100000, 999999).ToString());
        }
        
        

        public Faker<Employee> CreateEmployeeListGenerator()
        {
            return new Faker<Employee>("ru")
                .RuleFor(x => x.FirstName, f => f.Name.LastName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(x => x.DateOfBirth, f => f.Date.BetweenDateOnly(new DateOnly(1940, 1, 1), new DateOnly(2003, 1, 1)).ToDateTime(TimeOnly.MinValue).ToUniversalTime())
                .RuleFor(x => x.Salary, f => f.Random.Int(5000, 20000))
                .RuleFor(x => x.NumberOfPassport, f => f.Random.Int(10000, 99999))
                .RuleFor(x => x.SeriesOfPassport, f => f.Random.Int(100000, 999999).ToString())
                .RuleFor(x => x.Contract, f => "Принят на работу")
                .RuleFor(x => x.Position, f => f.Random.ArrayElement(new[] 
                {
                            "Программист",
                            "Менеджер",
                            "Бизнес-аналитик",
                            "Дизайнер",
                            "Тестировщик" 
                }));
        }
    }
}
