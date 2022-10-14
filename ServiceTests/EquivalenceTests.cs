using Models;
using Services;
using Xunit;

namespace ServiceTests
{
    public class EquivalenceTests
    {
        [Fact]
        public void GetHashCodeNecessityPositivTest()
        {
            //Arrange
            var testDataGenerator = new TestDataGenerator();
            var generatorClient = testDataGenerator.CreateClientListGenerator();
            var clients = generatorClient.Generate(1000);

            //Создание клиента с двумя аккаунтами 
            var testClientInTheDict = new Client();
            testClientInTheDict.Phone = "77658346";
            testClientInTheDict.DateOfBirth = new DateTime(year: 1998, 5, 5);
            testClientInTheDict.FirstName = "Johny";
            testClientInTheDict.LastName = "Depp";          

            var firstAccountInTheDict = new Account();
            firstAccountInTheDict.Amount = 157000;         
              
            var secondAccountInTheDict = new Account();
            secondAccountInTheDict.Amount = 957420;
         
            //Добавление аккаунтов в список
            var testAccountList = new List<Account>();
            testAccountList.AddRange(new[] {firstAccountInTheDict, secondAccountInTheDict});

            //Добавление клиента и списка в словарь
            var dictionary = testDataGenerator.CreateClientDictionaryWithAccount(clients);
            dictionary.Add(testClientInTheDict, testAccountList);

            //Создание идентичного клиента 
            var testClientOutOfTheDict = new Client();
            testClientOutOfTheDict.Phone = "77658346";
            testClientOutOfTheDict.DateOfBirth = new DateTime(year: 1998, 5, 5);
            testClientOutOfTheDict.FirstName = "Johny";
            testClientOutOfTheDict.LastName = "Depp";           

            //Act
            var expectedAccountList = dictionary[testClientOutOfTheDict];

            //Assert
            Assert.Equal(expectedAccountList, testAccountList);
        }

        [Fact]
        public void GetHashCodeNecessityPositiveTestWithEmployee()
        {
            //Arrange
            var testDataGenerator = new TestDataGenerator();
            var generatorEmployee = testDataGenerator.CreateEmployeeListGenerator();
            var employes = generatorEmployee.Generate(1000);

            var employeeInTheList = new Employee();
            employeeInTheList.FirstName = "Alise";
            employeeInTheList.LastName = "Hansonn";
            employeeInTheList.DateOfBirth = new DateTime(year: 1998, 6, 5);
            employeeInTheList.Phone = "77465385";
            employeeInTheList.Position = "Программист";

            employes.Add(employeeInTheList);

            var employeeOutOfTheList = new Employee();
            employeeOutOfTheList.FirstName = "Alise";
            employeeOutOfTheList.LastName = "Hansonn";
            employeeOutOfTheList.DateOfBirth = new DateTime(year: 1998, 6, 5);
            employeeOutOfTheList.Phone = "77465385";
            employeeOutOfTheList.Position = "Программист";

            //Act 
            var testBool = employes.Contains(employeeOutOfTheList);

            //Assert
            Assert.True(testBool);
        }
    }
}