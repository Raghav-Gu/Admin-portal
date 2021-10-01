import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl,Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { EmployeeService } from 'src/app/Shared/employee.service';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import { Employee } from 'src/app/Model/employee';
import { ToastrService } from 'ngx-toastr';
import { Confirmpwd } from 'src/app/Model/confirmpwd';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  closeResult = '';  
  checks:any;
  exform:FormGroup; 
  constructor(public service: EmployeeService, public router: Router,private modalService: NgbModal,private Toastr:ToastrService) { }
  
  ngOnInit(): void {
    debugger;
    
    this.checks=false;
    const regex = new RegExp(
      /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$%^&*\(\)_\+\-\={}<>,\.\|""'~`:;\\?\/\[\] ]).{6,}$/
    );
    this.exform=new FormGroup({
      'Firstname':new FormControl(null,Validators.required),
      'lastname':new FormControl(null,Validators.required),
      'mobile':new FormControl(null,[Validators.required,Validators.pattern('^\\s*(?:\\+?(\\d{1,3}))?[-. (]*(\\d{3})[-. )]*(\\d{3})[-. ]*(\\d{4})(?: *x(\\d+))?\\s*$')]),
      'email':new FormControl(null,[Validators.required,Validators.email]),
      'Password':new FormControl(null,[Validators.required,Validators.minLength(10),Validators.pattern(regex)]),
      'Confirmpassword':new FormControl(null,[Validators.required,Validators.minLength(10),Validators.pattern(regex)]),
      
    })
    
  }
  disabledchecked=true;
  disabled = true;
  check = true
  onChange(){
    debugger
    if(this.exform.valid==true){
      this.disabledchecked=false;
    }
    if(this.exform.valid==false){
    this.disabledchecked=true;
    }
  }
  
  
  oncheck(e:any){
    debugger;
    if(e.target.checked==true && this.exform.valid==true){
      localStorage.setItem("checks",e.target.checked);
      this.disabled=false;
      
    }
    else{
        this.disabled=true;
    }
  }
  onClick() {
    debugger;
    var employee=new Employee;
    return this.service.PostEmpDetail(employee.FirstName,employee.LastName,employee.MobileNo,employee.Email,employee.Password,employee.ConfirmPassword).subscribe(res => {
      this.Toastr.success('','Registration Successfully Please Check your Email');
    }, err => { this.Toastr.error('','Registartion not Successfully'); })
    return this.service.Verificationapi();

  }
}
