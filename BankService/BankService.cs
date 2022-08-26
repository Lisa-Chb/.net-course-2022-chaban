﻿using Models;

namespace Services
{
    public class BankService
    {
        public static int CalculateOwnerSalary(int ownerCount, float bankProfit, float bankExpenses)
        {
            return Convert.ToInt32((bankProfit - bankExpenses) / ownerCount);
        }

        public static Employee ClientToEmployee(Client client)
        {
            Employee employee = new Employee()
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                Age = client.Age,
                SeriesOfPassport = client.SeriesOfPassport,
                Phone = client.Phone,
                DateOfBirth = client.DateOfBirth
            };

            return employee;
        }
    }
}