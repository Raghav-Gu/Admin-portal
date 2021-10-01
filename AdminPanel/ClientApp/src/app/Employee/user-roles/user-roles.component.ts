import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from 'src/app/Model/login';
import { EmployeeService } from 'src/app/Shared/employee.service';

@Component({
  selector: 'app-user-roles',
  templateUrl: './user-roles.component.html',
  styleUrls: ['./user-roles.component.css']
})
export class UserRolesComponent implements OnInit {

  constructor(public service:EmployeeService,public router:Router) { }

  ngOnInit(): void {
    debugger;
    var data=JSON.parse(localStorage.res);
    if(data==undefined){
      this.router.navigateByUrl("/Login");
    }
    this.service.GetAllUser();
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
