import { Component, OnInit, Pipe } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Confirmpwd } from 'src/app/Model/confirmpwd';
import { GetAll } from 'src/app/Model/get-all';
import { Login } from 'src/app/Model/login';
import { EmployeeService } from 'src/app/Shared/employee.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {
  searchText: any;
  showpanel:boolean=false;
  checks:any;
  constructor(public service:EmployeeService,public router:Router,public toaster:ToastrService) { }
  
  ngOnInit(): void {
    debugger;
    var datas=JSON.parse(localStorage.res);
    var emp=new Confirmpwd;
    emp.isactive=datas.isActive;
    var data=localStorage.res;
    debugger;
    this.checks=emp.isactive;
    if(data==undefined){
      this.router.navigateByUrl("/Login");
    }
    this.service.GetAllUser();
  }
  onshow():void{
    this.showpanel = !this.showpanel;
  }
 
 
  bulk(e:any){
    debugger;
      if(e.target.checked==true){
        
          this.checks=true;
          var emp=new Confirmpwd;
          var datacheck=this.checks.toString();
          emp.isactive=this.checks;
          localStorage.setItem("isActive",datacheck)
          var data=JSON.parse(localStorage.res);
          this.service.IsActive(data.id,emp.isactive).subscribe(res=>{
            console.log('kaise ho',res);
          },err=>{
            console.log(err);
          })
      }
      else{
        this.checks=false;
          var emp=new Confirmpwd;
          emp.isactive=this.checks;
          var data=JSON.parse(localStorage.res);
          this.service.IsActive(data.id,emp.isactive).subscribe(res=>{
            console.log('kaise ho',res);
          },err=>{
            console.log(err);
          })
      }
  }
  onDelete(id:string){
this.service.onDelete(id).subscribe(res=>{
this.toaster.success('','Deleted User Succeessfully');
},err=>{
  console.log(err);
})
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
  GoUser(){
    this.router.navigateByUrl("/AddUser")
  }
  onEdit(id:string){
    debugger;
    var UserId=id;
    localStorage.setItem("UserId",JSON.stringify(UserId));
    var data=localStorage.getItem("UserId");
    this.router.navigate([`/Usersdetail/${UserId}`]);
  }

}
