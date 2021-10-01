
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Login } from 'src/app/Model/login';
import { EmployeeService } from 'src/app/Shared/employee.service';

@Component({
  selector: 'app-employee-login',
  templateUrl: './employee-login.component.html',
  styleUrls: ['./employee-login.component.css']
})
export class EmployeeLoginComponent implements OnInit {
exform:FormGroup;
  constructor(public service: EmployeeService, public router: Router,public toastr:ToastrService) { }

  ngOnInit(): void {
    this.exform=new FormGroup({
      'email':new FormControl(null,[Validators.required,Validators.email]),
      'Password':new FormControl(null,[Validators.required,Validators.minLength(10)])
    })
    var data=JSON.parse(localStorage.res);
    if(data.token){
      this.router.navigateByUrl('/DashBoard');   
    }
  }
 
  onclick() {
    var employee = new Login();
    debugger
    
    this.service.LoginEmpDetail(employee.Email, employee.Password,employee.Rememberme).subscribe(res => {
      localStorage.setItem("res",JSON.stringify(res));
      this.router.navigateByUrl("/DashBoard"); 
      this.toastr.success('','Login was Successfully');
           
    }, err => {this.toastr.error('','Login Invalid') })
  }
  onnavigate() {
    //this.router.navigateByUrl('/')
  }
  onanothernavigate(){
    this.router.navigateByUrl('/Reset')
  }
  
}
