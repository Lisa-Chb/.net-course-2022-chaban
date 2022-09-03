using Bogus;
using Models;
using Services;
using System.Diagnostics;
using Xunit;

namespace ServiceTests
{
    public class EquivalenceTests
    {

        [Fact]

        public void GetHashCodeNecessityPositivTest()
        {
            //Arrange
            TestDataGenerator testDataGenerator = new TestDataGenerator();
            Faker<Client> generatorClient = testDataGenerator.CreateClientList();
            List<Client> clients = generatorClient.Generate(1000);

            //Создание клиента с двумя аккаунтами 
            Client testClient1 = new Client();
            testClient1.Phone = "77658346";
            testClient1.Age = 65;
            testClient1.FirstName = "Johny";
            testClient1.LastName = "Depp";          

            Account testAccount1 = new Account();
            Currency testCurrency1 = new Currency("RUB", 5637);
            testAccount1.Amount = 157000;         
            testAccount1.Currency = testCurrency1;      

            Account testAccount2 = new Account();
            Currency testCurrency2 = new Currency("USD", 7359);
            testAccount2.Amount = 957420;
            testAccount2.Currency = testCurrency2;

            //Добавление аккаунтов в список
            List<Account> testAccountList1 = new List<Account>();
            testAccountList1.Add(testAccount1);
            testAccountList1.Add(testAccount2);

            //Добавление клиента и списка в словарь
            Dictionary<Client, List <Account>> dictionary = testDataGenerator.CreateClientDictionaryWithAccount(clients);
            dictionary.Add(testClient1, testAccountList1);

            //Создание идентичного клиента 
            Client testClient2 = new Client();
            testClient2.Phone = "77658346";
            testClient2.Age = 65;
            testClient2.FirstName = "Johny";
            testClient2.LastName = "Depp";           

            //Act
            List <Account> finalAccout = dictionary[testClient2];

            //Assert
            Assert.Equal(finalAccout, testAccountList1);
        }

        [Fact]

        public void GetHashCodeNecessityPositiveTestWithEmployee()
        {
            //Arrange
            TestDataGenerator testDataGenerator = new TestDataGenerator();
            Faker<Employee> generatorEmployee = testDataGenerator.CreateEmployeeList();
            List<Employee> employes = generatorEmployee.Generate(1000);

            Employee testEmployee1 = new Employee();
            testEmployee1.FirstName = "Alise";
            testEmployee1.LastName = "Hansonn";
            testEmployee1.Age = 27;
            testEmployee1.Phone = "77465385";
            testEmployee1.Position = "Программист";

            employes.Add(testEmployee1);

            Employee testEmployee2 = new Employee();
            testEmployee2.FirstName = "Alise";
            testEmployee2.LastName = "Hansonn";
            testEmployee2.Age = 27;
            testEmployee2.Phone = "77465385";
            testEmployee2.Position = "Программист";

            //Act 
            bool testBool = employes.Contains(testEmployee2);

            //Assert
            Assert.True(testBool);

        }
    }
}