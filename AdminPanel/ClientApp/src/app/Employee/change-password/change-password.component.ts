import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Confirmpwd } from 'src/app/Model/confirmpwd';
import { EmployeeService } from 'src/app/Shared/employee.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  constructor(public service:EmployeeService,public route: ActivatedRoute,public router:Router,public toastr:ToastrService) { }

  ngOnInit(): void {
    var data=localStorage.res;
    if(data==undefined){
      this.router.navigateByUrl("/Login");
    }
  }
  
  onclick(){
    debugger;
     var emp=new Confirmpwd;
     var data=JSON.parse(localStorage.res);
     var id="";
    id=data.id;
  this.service.ConfirmPassword(id,emp.oldPassword,emp.NewPassword,emp.ConfirmPassword).subscribe(res=>{
    this.toastr.success("","Password is updated");
    emp.oldPassword='';
  })
}
onProfile(){
  debugger
  var data=JSON.parse(localStorage.res);
    this.router.navigate([`/Profile/${data.id}`]);
}
onChangePassword(){
  debugger
  var data=JSON.parse(localStorage.res);
    this.router.navigate([`/Change/${data.id}`]);
}
onLogout(){
    
  localStorage.removeItem('token');
  localStorage.clear();
  this.router.navigateByUrl('/Login');
}
}
