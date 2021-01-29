import { Role } from "./role";

export class Employee {
  id: number;
  name: string;
  contractTypeName: string;
  role: Role;
  hourlySalary: number;
  monthlySalary: number;
  salary: number;
}
