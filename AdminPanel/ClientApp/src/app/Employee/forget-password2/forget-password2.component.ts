import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Employee } from 'src/app/Model/employee';
import { EmployeeService } from 'src/app/Shared/employee.service';

@Component({
  selector: 'app-forget-password2',
  templateUrl: './forget-password2.component.html',
  styleUrls: ['./forget-password2.component.css']
})
export class ForgetPassword2Component implements OnInit {

  constructor(public service:EmployeeService,public route: ActivatedRoute,public router: Router,public toastr:ToastrService) { }

  ngOnInit(): void {
  }
  onClick(){
    debugger;
    var uid="";
    var emp=new Employee();
    this.route.queryParams.subscribe(params => {
      uid = params.uid;
    });
    return this.service.ResetPassword(uid,emp.Password,emp.ConfirmPassword).subscribe(res=>{
      this.toastr.success('','Reset Password Successfully Done');
      this.router.navigateByUrl('/Login')
    },err=>{console.log('not done');})
  }
}
