using MasGlobalTest.Application.Employees.Contract;

namespace MasGlobalTest.Application.Employees
{
    public class EmployeeFactory
    {
        public Employee CreateContract(string typeContract)
        {
            switch (typeContract)
            {
                case "HourlySalaryEmployee":
                    return new EmployeeHourlySalary();
                case "MonthlySalaryEmployee":
                    return new EmployeeMonthlySalary();
                default:
                    return null;
            }
        }
    }
}