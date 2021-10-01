import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/Model/employee';
import { EmployeeService } from 'src/app/Shared/employee.service';

@Component({
  selector: 'app-get-user',
  templateUrl: './get-user.component.html',
  styleUrls: ['./get-user.component.css']
})
export class GetUserComponent implements OnInit {

  constructor(public service:EmployeeService) { }

  ngOnInit(): void {
    debugger;
    var emp=new Employee;
    var dta=this.service.ViewUser(emp.id);
    console.log(dta);
  }

}
