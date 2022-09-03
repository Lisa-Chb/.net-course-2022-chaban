using Bogus;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TestDataGenerator
    {
        public static Faker<Account> CreateAccount()
        {
            return new Faker<Account>()
                 .RuleFor(x => x.Amount, f => f.Random.Int(100000, 999999))
                 .RuleFor(x => x.Currency, f => new Currency(f.Random.ListItem(new List<string>
                 {
                    "RUB",
                    "USD",
                    "EUR",
                    "MDL",
                    "UAH"
                 }), f.Random.Int(1000, 9999)));
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
        public Faker<Client> CreateClientList()
        {
            return new Faker<Client>("ru")
                .RuleFor(x => x.FirstName, f => f.Name.LastName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.Age, f => f.Random.Byte(18, 100))
                .RuleFor(x => x.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(x => x.AccountNumber, f => f.Random.Int(1000, 9999));
        }

        public Faker<Employee> CreateEmployeeList()
        {
            return new Faker<Employee>("ru")
                .RuleFor(x => x.FirstName, f => f.Name.LastName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.Age, f => f.Random.Byte(18, 100))
                .RuleFor(x => x.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(x => x.Salary, f => f.Random.Int(5000, 20000))
                .RuleFor(x => x.Position, f => f.Random.ListItem(new List<string> {
                            "Программист",
                            "Менеджер",
                            "Бизнес-аналитик",
                            "Дизайнер",
                            "Тестировщик" }));

        }
    }
}
