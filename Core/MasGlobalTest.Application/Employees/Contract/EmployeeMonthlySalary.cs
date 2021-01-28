namespace MasGlobalTest.Application.Employees.Contract
{
    public class EmployeeMonthlySalary : Employee
    {
        private const int Months = 12;
        public override void CalculateSalary()
        {
            this.AnnualSalary = this.MonthlySalary * Months;
        }
    }
}
