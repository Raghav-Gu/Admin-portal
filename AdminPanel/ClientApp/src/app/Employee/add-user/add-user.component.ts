import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Employee } from 'src/app/Model/employee';
import { Login } from 'src/app/Model/login';
import { EmployeeService } from 'src/app/Shared/employee.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {

  constructor(public router:Router,public service:EmployeeService,public route: ActivatedRoute,public toaster:ToastrService) { }

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
    var emp=new Employee;
   return this.service.AddUsers().subscribe(res=>{
     this.router.navigateByUrl("/Users");
     this.service.formdata.FirstName="";
     this.service.formdata.LastName="";
     this.service.formdata.MobileNo="";
     this.service.formdata.Email="";
     this.service.formdata.Password="";
     this.service.formdata.ConfirmPassword="";
     this.service.formdata.userName="";
this.toaster.success('','Users Added Successfully');
   },err=>{
     console.log(err);
   })
  }
  onLogout(){
    
    localStorage.removeItem('token');
    localStorage.clear();
    this.router.navigateByUrl('/Login');
  }

}
