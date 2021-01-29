using MasGlobalTest.Application.Employees;
using MasGlobalTest.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Test
{
    [TestClass]
    public class EmployeeTest
    {
        private EmployeeFactory employeeFactory;
        
        [TestInitialize]
        public void SetUp()
        {
            employeeFactory = new EmployeeFactory();
        }

        [TestMethod]
        public void MonthlyEmployee()
        {
            Employee employee = employeeFactory.CreateContract("MonthlySalaryEmployee");
            employee.ContractTypeName = "MonthlySalaryEmployee";
            employee.HourlySalary = 200;
            employee.Id = 1;
            employee.MonthlySalary = 1000;
            employee.Role = new Role() { RoleId = 1, RoleName = "Administrator" };
            employee.CalculateSalary();

            Assert.AreEqual(employee.AnnualSalary, 12000);
        }

        [TestMethod]
        public void HourlyEmployee()
        {
            Employee employee = employeeFactory.CreateContract("HourlySalaryEmployee");
            employee.ContractTypeName = "HourlySalaryEmployee";
            employee.HourlySalary = 200;
            employee.Id = 2;
            employee.MonthlySalary = 1000;
            employee.Role = new Role() { RoleId = 2, RoleName = "Contractor" };
            employee.CalculateSalary();

            Assert.AreEqual(employee.AnnualSalary, 288000);
        }
    }
}
