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
            Faker<Client> generatorClient = testDataGenerator.CreateClientListGenerator();
            List<Client> clients = generatorClient.Generate(1000);

            //Создание клиента с двумя аккаунтами 
            Client testClientInTheDict = new Client();
            testClientInTheDict.Phone = "77658346";
            testClientInTheDict.DateOfBirth = new DateTime(year: 1998, 5, 5);
            testClientInTheDict.FirstName = "Johny";
            testClientInTheDict.LastName = "Depp";          

            Account firstAccountInTheDict = new Account();
            Currency testCurrency_RUB = new Currency("RUB", 5637);
            firstAccountInTheDict.Amount = 157000;         
            firstAccountInTheDict.Currency = testCurrency_RUB;      

            Account secondAccountInTheDict = new Account();
            Currency testCurrency_USD = new Currency("USD", 7359);
            secondAccountInTheDict.Amount = 957420;
            secondAccountInTheDict.Currency = testCurrency_USD;

            //Добавление аккаунтов в список
            List<Account> testAccountList = new List<Account>();
            testAccountList.AddRange(new[] {firstAccountInTheDict, secondAccountInTheDict});

            //Добавление клиента и списка в словарь
            Dictionary<Client, List <Account>> dictionary = testDataGenerator.CreateClientDictionaryWithAccount(clients);
            dictionary.Add(testClientInTheDict, testAccountList);

            //Создание идентичного клиента 
            Client testClientOutOfTheDict = new Client();
            testClientOutOfTheDict.Phone = "77658346";
            testClientOutOfTheDict.DateOfBirth = new DateTime(year: 1998, 5, 5);
            testClientOutOfTheDict.FirstName = "Johny";
            testClientOutOfTheDict.LastName = "Depp";           

            //Act
            List <Account> expectedAccountList = dictionary[testClientOutOfTheDict];

            //Assert
            Assert.Equal(expectedAccountList, testAccountList);
        }

        [Fact]

        public void GetHashCodeNecessityPositiveTestWithEmployee()
        {
            //Arrange
            TestDataGenerator testDataGenerator = new TestDataGenerator();
            Faker<Employee> generatorEmployee = testDataGenerator.CreateEmployeeListGenerator();
            List<Employee> employes = generatorEmployee.Generate(1000);

            Employee employeeInTheList = new Employee();
            employeeInTheList.FirstName = "Alise";
            employeeInTheList.LastName = "Hansonn";
            employeeInTheList.DateOfBirth = new DateTime(year: 1998, 6, 5);
            employeeInTheList.Phone = "77465385";
            employeeInTheList.Position = "Программист";

            employes.Add(employeeInTheList);

            Employee employeeOutOfTheList = new Employee();
            employeeOutOfTheList.FirstName = "Alise";
            employeeOutOfTheList.LastName = "Hansonn";
            employeeOutOfTheList.DateOfBirth = new DateTime(year: 1998, 6, 5);
            employeeOutOfTheList.Phone = "77465385";
            employeeOutOfTheList.Position = "Программист";

            //Act 
            bool testBool = employes.Contains(employeeOutOfTheList);

            //Assert
            Assert.True(testBool);

        }
    }
}