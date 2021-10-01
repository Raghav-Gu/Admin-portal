import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Employee } from 'src/app/Model/employee';
import { EmployeeService } from 'src/app/Shared/employee.service';

@Component({
  selector: 'app-employee-forget-password',
  templateUrl: './employee-forget-password.component.html',
  styleUrls: ['./employee-forget-password.component.css']
})
export class EmployeeForgetPasswordComponent implements OnInit {

  constructor(public service:EmployeeService,public router: Router,public toastr:ToastrService) { }

  ngOnInit(): void {
  }
  onSubmit() {
    
      var emp=new Employee
      
      return this.service.ForgetPassword(emp.Email).subscribe(res => {
        this.toastr.success('Email sent in your Mail Id','');
      }, err => { console.log(err) })
    
  }
}
