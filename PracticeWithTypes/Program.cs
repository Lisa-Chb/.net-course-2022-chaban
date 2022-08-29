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
        Faker<Employee> generatorEmployee = testDataGenerator.EmployeeGenerator();
        List<Employee> employees = generatorEmployee.Generate(1000);

        Faker<Client> generatorClient = testDataGenerator.ClientGenerator();
        List<Client> clients = generatorClient.Generate(1000);
        Client testClient = new Client();
        testClient.Phone = "077863694";
        clients.Add(testClient);

        Dictionary<string, Client> clientsDict = testDataGenerator.DictionaryGenerator(clients);

        Stopwatch stopwatch = new Stopwatch();
        Program program = new Program();

        //Задание а
        stopwatch.Start();
       Client client6 =  program.ClientSearchList(clients, "077863694");
        stopwatch.Stop();

        program.PrintTime(stopwatch, "ClientSearchlist");

        //Задание б      
        stopwatch.Start();
        Client client7 = program.ClientSearchDictionary(clientsDict, "077863694");
        stopwatch.Stop();
        program.PrintTime(stopwatch, "ClientSearchDictionary");

        //Задание в
        stopwatch.Start();
       List<Client> clients3 = program.ClientSearchByAge(clients, 45);
        stopwatch.Stop();
        program.PrintTime(stopwatch, "ClientSearchByAge");

        //Задание г
        stopwatch.Start();
       Employee client4 =  program.SearchMinSalary(employees);
        stopwatch.Stop();
        program.PrintTime(stopwatch, "SearchMinSalary");

        //Задание д 1
        stopwatch.Start();
        Client lastClient = program.LastClientWithFirstOrDefault(clientsDict, "077863694");
        stopwatch.Stop();
        program.PrintTime(stopwatch, "LastClientWithFirstOrDefault");

        //Задание д 2
        stopwatch.Start();
        Client lastClient2 = program.LastClientWithKey(clientsDict, "077863694");
        stopwatch.Stop();
        program.PrintTime(stopwatch, "LastClientWithKey");

    }

    public void PrintTime(Stopwatch stopwatch, string nameOfMethod)
    {
        Console.WriteLine($" Runtime {nameOfMethod}" + " " + stopwatch.Elapsed);
    }

    public Client ClientSearchDictionary(Dictionary<string, Client> clientsDictionary, string phone)
    {
        return clientsDictionary[phone];
    }

    public Client ClientSearchList(List<Client> clients, string phone)
    {
        var client = clients.FirstOrDefault(p => p.Phone == phone);
        return client;
    }

    public List<Client> ClientSearchByAge(List<Client> clients, int age)
    {
        var clientByAge = clients.FindAll(p => p.Age <= age);
        return clientByAge;
    }

    public Employee SearchMinSalary(List<Employee> employees)
    {
       double minSalary = employees.Min(a => a.Salary);
       return employees.FirstOrDefault(a => a.Salary == minSalary);
        //var employee = employees.FirstOrDefault(p => p.Salary == p.Min();
        
    }

    public Client LastClientWithFirstOrDefault(Dictionary<string, Client> clients, string phone)
    {
        var lastClient = clients.FirstOrDefault(p => p.Key == phone).Value;
        return lastClient;
    }

    public Client LastClientWithKey(Dictionary<string, Client> clients, string phone)
    {
        return clients[phone];
    }
}



