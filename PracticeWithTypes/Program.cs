using Services;
using Models;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Numerics;
using Bogus;

public class Program
{
    public static void Main(string[] args)
    {
        TestDataGenerator testDataGenerator = new TestDataGenerator();
        Faker<Employee> generatorEmployee = testDataGenerator.CreateEmployeeListGenerator();
        List<Employee> employees = generatorEmployee.Generate(1000);

        Faker<Client> generatorClient = testDataGenerator.CreateClientListGenerator();
        List<Client> clients = generatorClient.Generate(1000);
        Client testClient = new Client();
        testClient.Phone = "077863694";
        clients.Add(testClient);

        Dictionary<string, Client> clientsDict = testDataGenerator.CreateClientDictionaryWithPhone(clients);

        Stopwatch stopwatch = new Stopwatch();

        //Задание а
        for (int i = 0; i <= 3; i++)
        {
            stopwatch.Start();
            Client client6 = clients.FirstOrDefault(p => p.Phone == "077863694");
            stopwatch.Stop();
            PrintTime(stopwatch, "ClientSearchlist");
        }

        //Задание б      
        for (int i = 0; i <= 3; i++)
        {
            stopwatch.Reset();
            stopwatch.Start();
            Client client7 = clientsDict["077863694"];
            stopwatch.Stop();
            PrintTime(stopwatch, "ClientSearchDictionary");
        }
        //Задание в
        for (int i = 0; i <= 3; i++)
        {
            stopwatch.Reset();
            stopwatch.Start();
            List<Client> clients3 = clients.FindAll(p => p.Age <= 45);
            stopwatch.Stop();
            PrintTime(stopwatch, "ClientSearchByAge");
        }

        //Задание г
        for (int i = 0; i <= 3; i++)
        {
            stopwatch.Reset();
            stopwatch.Start();
            Employee client4 = employees.MinBy(a => a.Salary);
            stopwatch.Stop();
            PrintTime(stopwatch, "SearchMinSalary");
        }

        //Задание д 1
        for (int i = 0; i <= 3; i++)
        {
            stopwatch.Reset();
            stopwatch.Start();
            Client lastClient = clientsDict.FirstOrDefault(p => p.Key == "077863694").Value;
            stopwatch.Stop();
            PrintTime(stopwatch, "LastClientWithFirstOrDefault");
        }

        //Задание д 2
        for (int i = 0; i <= 3; i++)
        {
            stopwatch.Reset();
            stopwatch.Start();
            Client lastClient2 = clientsDict["077863694"];
            stopwatch.Stop();
            PrintTime(stopwatch, "LastClientWithKey");
        }

    }
    public static void PrintTime(Stopwatch stopwatch, string nameOfMethod)
    {
        Console.WriteLine($" Runtime {nameOfMethod}" + " " + stopwatch.ElapsedTicks);
    }
}



