namespace MasGlobalTest.Domain
{
    public class Role
    {      
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public object RoleDescription { get; set; }

        public Role() { }

        public Role(int roleId, string roleName, object roleDescription)
        {
            RoleId = roleId;
            RoleName = roleName;
            RoleDescription = roleDescription;
        }

        public Role(EmployeeDto employeeDto)
        {
            RoleId = employeeDto.RoleId;
            RoleName = employeeDto.RoleName;
            RoleDescription = employeeDto.RoleDescription;                 
        }
    }
}
