import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from '../models/Employee';

@Injectable({
  providedIn: 'root',
})

export class EmployeeService {

  constructor(private http: HttpClient) { }

  baseUrl: string = 'http://localhost:57775';

  getEmployees(): Observable<Array<Employee>> {
    return this.http.get<Array<Employee>>(this.baseUrl + '/api/employees');
  }

  getEmployee(id: number): Observable<Employee> {
    return this.http.get<Employee>(this.baseUrl + '/api/employees/' + id);
  }

}
