namespace MasGlobalTest.Application.Employees.Contract
{
    public class EmployeeHourlySalary : Employee
    {
        private const int Hours = 120;
        private const int Months = 12;

        public override void CalculateSalary()
        {
            this.AnnualSalary = Hours * this.HourlySalary * Months;
        }

    }
}
