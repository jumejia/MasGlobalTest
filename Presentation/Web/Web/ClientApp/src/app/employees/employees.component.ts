import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Employee } from '../models/Employee';
import { EmployeeService } from '../services/employee.service';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html'
})
export class EmployeesComponent implements OnInit {
  employees: Array<Employee> = new Array<Employee>();
  employeeForm: FormGroup;
  loading = false;
  submitted = false;

  constructor(private formBuilder: FormBuilder, private employeeService: EmployeeService) { }

  ngOnInit(): void {   
    this.employeeForm = this.formBuilder.group({
      txtEmployeeId: ['']
    });
  }

  numbersOnly(event): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }

    return true;
  }

  get form() { return this.employeeForm.controls; }

  getEmployees(id: number) {

    if (id === null) {
      this.employeeService.getEmployees().subscribe(data => {
        this.employees = null;
        if (data) {
          this.employees = data;
          console.log(this.employees);
          this.loading = false;
        }
      }, error => {
          alert(error.error.message);
          this.loading = false;
      });
    }
    else {
      this.employeeService.getEmployee(id).subscribe(data => {
        this.employees = null;
        if (data) {
          this.employees = [data];
          console.log(this.employees);
          this.loading = false;
        }
      }, error => {
          alert(error.error.message);
          this.loading = false;
      });
    }
  }

  onSubmit() {
    this.submitted = true;

    if (this.employeeForm.invalid) {
      return;
    }
    this.loading = true;

    this.getEmployees(this.form.txtEmployeeId.value === "" ? null : this.form.txtEmployeeId.value);
  }
}
