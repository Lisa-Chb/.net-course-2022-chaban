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
        public void RateUpdaterTest()
        {
            var cancellationtTokenSource = new CancellationTokenSource();
            var token = cancellationtTokenSource.Token;

            var service = new ClientService();
            var filter = new ClientFilter() { PageSize = 100 };

            var rateUpdater = new RateUpdater();
            rateUpdater.StartAccrual(token, service, filter).Wait(20000);

            cancellationtTokenSource.Cancel();
        }

        [Fact]
        public void CaschDispenserTest()
        {
            //Arrange
            var dicpenser = new CashDispenserService();

            var testDataGenerator = new TestDataGenerator();
            var generatorClient = testDataGenerator.CreateClientListGenerator();
            var clients = generatorClient.Generate(10);
            var service = new ClientService();

            var accounts = new List<Account>();
            
            foreach (Client client in clients)
            {
                service.AddClient(client);
                var account = new Account() { AccountId = Guid.NewGuid(), Amount = 250, Clientid = client.ClientId, CurrencyCode = 840 };
                accounts.Add(account);
                service.AddAccount(account);
            }

            //Act
            var taskCollection = new List<Task>();

            for (int i = 0; i <= 9; i++)
            {
                taskCollection.Add(dicpenser.CashingOut(accounts[i].AccountId));             
            }

            foreach(var task in taskCollection)
            {
               task.Wait();
            }

            //Assert
            var serviseToGetAccount = new ClientService();
            var testAccount = serviseToGetAccount.GetAccount(accounts.FirstOrDefault().AccountId);
            Assert.Equal(testAccount.Amount, 150);
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
                        Thread.Sleep(10000);
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
                        Thread.Sleep(10000);
                        _output.WriteLine($"Поток {Thread.CurrentThread.Name} начислил 100. Текущий счет  {accountTest.Amount}");
                    }
                }
            });

            secondThread.Name = "ThreadB";

            firstThread.Start();
            secondThread.Start();
            Thread.Sleep(20000);

            //Assert
            Assert.Equal(accountTest.Amount, 200);
        }

        [Fact]
        public void AddandReadClientTest()
        {
            //Arrange
            //очищаю файлы перед работой, чтобы не добавлять в базу уже сущестующих клиентов после предыдущих тестов
            File.WriteAllText(@"C:\\Users\\Hi-tech\\source\\repos\\.net-course-2022-chaban\\ExportTool\\ExportData\\ClientImport.csv", null);
            File.WriteAllText(@"C:\Users\Hi-tech\source\repos\.net-course-2022-chaban\ExportTool\ExportData\ClientExport.csv", null);

            var clientServiceExportThread = new ClientService();
            var clientServiceImportThread = new ClientService();
            var filter = new ClientFilter() { PageSize = 1000 };

            var pathToDirectory = @"C:\Users\Hi-tech\source\repos\.net-course-2022-chaban\ExportTool\ExportData\";
            var exportService = new ExportService();

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

                var exportClients = clientServiceExportThread.GetClients(filter);
                exportService.WriteClientToCsv(exportClients, pathToDirectory, "ClientExport.csv");

            });

            var importThread = new Thread(() =>
            {

                var importClients = exportService.ReadClientFromCsv(pathToDirectory, "ClientImport.csv");

                foreach (Client c in importClients)
                {
                    clientServiceImportThread.AddClient(c);
                    Thread.Sleep(100);
                }

            });

            exportThread.Start();
            importThread.Start();
            Thread.Sleep(20000);

            //Assert                  
            Assert.NotNull(clientServiceImportThread.GetClient(clientTest.ClientId));
            Assert.NotEmpty(exportService.ReadClientFromCsv(pathToDirectory, "ClientExport.csv"));
        }
    }
}
