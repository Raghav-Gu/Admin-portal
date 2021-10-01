import { query } from '@angular/animations';
import { stringify } from '@angular/compiler/src/util';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { param } from 'jquery';
import { ToastrService } from 'ngx-toastr';
import { Employee } from 'src/app/Model/employee';
import { GetEmployee } from 'src/app/Model/get-employee';
import { Login } from 'src/app/Model/login';
import { EmployeeService } from 'src/app/Shared/employee.service';
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  constructor(public service: EmployeeService,public router:Router,public toastr:ToastrService) { }
  listemployee: GetEmployee[];
  ngOnInit(): void {
    debugger;
    var data=localStorage.res;
    if(data==undefined){
      this.router.navigateByUrl("/Login");
    }
    this.service.GetUserDetail();
    var datas=JSON.parse(localStorage.res);
   
  }
  User(){
    debugger;
    

    this.router.navigateByUrl("/Users");
  }
  onClick(){
    debugger;
    var emp=new Login;
    var data=JSON.parse(localStorage.res);
    this.service.GetUserDetail();
      this.router.navigate([`/Profile/${data.id}`]);
     
  }
  onPasswordClick(){
    var data=JSON.parse(localStorage.res);
    this.router.navigate([`/Change/${data.id}`]);
    
  }
  onLogout(){
    
    localStorage.removeItem('token');
    localStorage.clear();
    this.router.navigateByUrl('/Login');
  }
}
