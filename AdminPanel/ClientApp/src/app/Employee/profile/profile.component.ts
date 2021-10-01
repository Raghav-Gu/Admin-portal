import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Login } from 'src/app/Model/login';
import { EmployeeService } from 'src/app/Shared/employee.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user:Login;

  constructor(public router:Router,public service:EmployeeService,public route: ActivatedRoute) { }

  ngOnInit(): void {
    debugger;
    
    var res=this.service.GetUserDetail();
    var data=localStorage.res;
    if(data==undefined){
      this.router.navigateByUrl("/Login");
    }
  }
  onProfile(){
    var data=JSON.parse(localStorage.res);
    this.router.navigate([`/Profile/${data.id}`]);
  }
  onClick(){
    debugger;
    var data=JSON.parse(localStorage.res);
    this.router.navigate([`/Change/${data.id}`]);
  }
  onClickPost(){
    var emp=new Login;
   return this.service.PostUserDetail(emp.id,emp.firstName,emp.lastName,emp.mobileNo).subscribe(res=>{
      console.log('true');
    },err=>{console.log(err)});
  }
  onLogout(){
    
    localStorage.removeItem('token');
    localStorage.clear();
    this.router.navigateByUrl('/Login');
  }
}
