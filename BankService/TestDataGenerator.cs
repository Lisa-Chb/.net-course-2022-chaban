using Bogus;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TestDataGenerator
    {
        public static Dictionary<string, Client> DictionaryGenerator(List<Client> clients)
        {
            return clients.ToDictionary(k => k.Phone, k => k);

            var dictionary = new Dictionary<string, Client>();
            foreach (Client client in clients)
            {
                dictionary[client.Phone] = client;
            }
            return dictionary;
        }
        public static Faker<Client> ClientGenerator()
        {
            return new Faker<Client>("ru")
                .RuleFor(x => x.FirstName, f => f.Name.LastName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.Age, f => f.Random.Byte(18, 100))
                .RuleFor(x => x.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(x => x.AccountNumber, f => f.Random.Int(1000, 9999));
        }

        public static Faker<Employee> EmployeeGenerator()
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

        public static void printClients(List<Client> clients)
        {
            foreach (var client in clients)
            {
                Console.WriteLine($"Основная информация:\n{client.FirstName}\n{client.LastName}\n{client.Age}\n{client.Phone}\n{client.AccountNumber}");
            }
        }

        public static void PrintEmployeers(List<Employee> employees)
        {
            foreach (var employee in employees)
            {
                Console.WriteLine($"Основная информация:\n{employee.FirstName}" +
                                                      $"\n{employee.LastName}" +
                                                      $"\n{employee.Age}" +
                                                      $"\n{employee.Phone}" +
                                                      $"\n{employee.Position}");
            }
        }
    }
}
