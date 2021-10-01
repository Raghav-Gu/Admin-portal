import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Login } from 'src/app/Model/login';
import { EmployeeService } from 'src/app/Shared/employee.service';

@Component({
  selector: 'app-usersdetails',
  templateUrl: './usersdetails.component.html',
  styleUrls: ['./usersdetails.component.css']
})
export class UsersdetailsComponent implements OnInit {

  user:Login;

  constructor(public router:Router,public service:EmployeeService,public route: ActivatedRoute,public toaster:ToastrService) { }

  ngOnInit(): void {
    debugger;
    var id=JSON.parse(localStorage.UserId);
     this.service.ViewUser(id);
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
    debugger;
    var UserId= JSON.parse(localStorage.UserId);
   return this.service.EditUser(UserId).subscribe(res=>{
      this.toaster.success('','Updated Succeessfully')
    },err=>{console.log(err)});
  }
  onLogout(){
    
    localStorage.removeItem('token');
    localStorage.clear();
    this.router.navigateByUrl('/Login');
  }

}
