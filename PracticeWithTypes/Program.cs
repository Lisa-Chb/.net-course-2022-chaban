using Services;
using Models;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Numerics;

public class Program
{
    public static void Main(string[] args)
    {
        DateTime dataTime = new DateTime(2000, 9, 23);
        Employee employee = new Employee();
        employee.Contract = UpdateContract("Lisa", "Chabah", dataTime, 2546);

        Currency currency = new Currency();
        currency.Name = "MDL";
        UpdateCurrency(ref currency);

        Employee owner = new Employee();
        BankService bankService = new BankService();
        owner.Salary = bankService.CalculateOwnerSalary(1, 2000, 1400);
     
        Client client = new Client();
        var employee1 = bankService.ClientToEmployee(client);
    }
  
    public static string UpdateContract(string firstName, string lastName, DateTime dateOfBirth, int seriesOfPassport)
    {
        string result = $"Имя сотрудника: {firstName} {lastName}. " +
                        $"Дата рождения: {dateOfBirth}. " +
                        $"Серия паспорта: {seriesOfPassport}";
        return result;
    }

    public static void UpdateCurrency(ref Currency currency)
    {
        currency.Name = "USD";     
        currency.Code++;
    }

}

