﻿using ExportTool;
using Models;
using Services;
using Services.Filtres;
using Xunit;
using Xunit.Abstractions;

namespace ServiceTests
{
    public class ThreadAndTaskTests
    {
        private ITestOutputHelper _output;

        public ThreadAndTaskTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void MultithreadedAmountTransferTest()
        {
            //Arrange                
            var lockObject = new Object();


            var accountTest = new Account()
            {
                Amount = 0
            };

            //Act

            var firstThread = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    lock (lockObject)
                    {
                        accountTest.Amount += 100;
                        Thread.Sleep(100);
                        _output.WriteLine($"Поток {Thread.CurrentThread.Name} начислил 100. Текущий счет {accountTest.Amount}");
                    }
                }
            });

            firstThread.Name = "ThreadA";

            var secondThread = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    lock (lockObject)
                    {
                        accountTest.Amount += 100;
                        Thread.Sleep(100);
                        _output.WriteLine($"Поток {Thread.CurrentThread.Name} начислил 100. Текущий счет  {accountTest.Amount}");
                    }
                }
            });

            secondThread.Name = "ThreadB";

            firstThread.Start();
            secondThread.Start();
            Thread.Sleep(20000);

            //Assert
            Assert.Equal(accountTest.Amount, 2000);
        }

        [Fact]
        public void AddandReadClientTest()
        {
            //Arrange

            //очищаю файлы перед работой, чтобы не добавлять в базу уже сущестующих клиентов после предыдущих тестов
            File.WriteAllText(@"C:\\Users\\Hi-tech\\source\\repos\\.net-course-2022-chaban\\ExportTool\\ExportData\\ClientImport.csv", null);
            File.WriteAllText(@"C:\Users\Hi-tech\source\repos\.net-course-2022-chaban\ExportTool\ExportData\ClientExport.csv", null);

            var clientService = new ClientService();
            var filter = new ClientFilter() { PageSize = 1000 };

            var pathToDirectory = @"C:\Users\Hi-tech\source\repos\.net-course-2022-chaban\ExportTool\ExportData\";
            var exportService = new ExportService();
            var importService = new ExportService();

            var testDataGenerator = new TestDataGenerator();
            var generatorClient = testDataGenerator.CreateClientListGenerator();
            var clients = generatorClient.Generate(4);
            var rnd = new Random();

            var clientTest = new Client
            {
                FirstName = "MultithreadingImport",
                LastName = "Test",
                NumberOfPassport = rnd.Next(1000, 9999),
                SeriesOfPassport = rnd.Next(100000, 999999).ToString(),
                Phone = rnd.Next(10000000, 99999999).ToString(),
                DateOfBirth = new DateTime(2000, 3, 20, 22, 0, 0),
                BonusDiscount = 5,
                ClientId = Guid.NewGuid()
            };

            clients.Add(clientTest);
            exportService.WriteClientToCsv(clients, pathToDirectory, "ClientImport.csv");

            //Act
            var exportThread = new Thread(() =>
            {
                var exportClients = clientService.GetClients(filter);
                exportService.WriteClientToCsv(exportClients, pathToDirectory, "ClientExport.csv");
                Thread.Sleep(1000);
            });

            var importThread = new Thread(() =>
            {
                var importClients = importService.ReadClientFromCsv(pathToDirectory, "ClientImport.csv");

                foreach (Client c in importClients)
                {
                    clientService.AddClient(c);
                    Thread.Sleep(100);
                }
            });

            exportThread.Start();
            importThread.Start();
            Thread.Sleep(20000);

            //Assert                  
            Assert.NotNull(clientService.GetClient(clientTest.ClientId));
            Assert.NotEmpty(exportService.ReadClientFromCsv(pathToDirectory, "ClientExport.csv"));
        }
    }
}
