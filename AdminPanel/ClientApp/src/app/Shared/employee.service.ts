import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Employee } from '../Model/employee';
import { Login } from '../Model/login';
import { FormGroup } from '@angular/forms';
import { Confirmpwd } from '../Model/confirmpwd';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { GetAll } from '../Model/get-all';
@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  jwtHelper = new JwtHelperService();
  exform: FormGroup;
  reqHeader = new HttpHeaders({
    "Content-Type": "application/json",
  });
  constructor(private http: HttpClient,public router:Router) { }
  readonly baseurl = 'http://localhost:20913/api';
 
  formdata: Employee = new Employee();
  FormData:Confirmpwd=new Confirmpwd
  formData: Login = new Login();
  List:Login;
  listdata:GetAll[];
  PostEmpDetail(FirstName: string, LastName: string, MobileNo: string, Email: string, Password: string, ConfirmPassword: string) {

    return this.http.post(this.baseurl + '/Account/register', this.formdata, { headers: this.reqHeader });
  }
  Verificationapi(){
    return this.http.get(this.baseurl+'/Account/confirmEmail');
  }
  LoginEmpDetail(Email: string, Password: string, Rememberme: boolean) {
    debugger;
    var data = { Email: this.formData.email, Password: this.formData.Password, Rememberme: this.formData.Rememberme }
    return this.http.post(this.baseurl + '/Account/Login', data, { headers: this.reqHeader });
  }
  loggedIn(){
    debugger;
    var data=JSON.parse(localStorage.res);
    const token = data.token;
    if(!token){
    return this.jwtHelper.isTokenExpired(token?.toString());
    }
    if(token){
    return !this.jwtHelper.isTokenExpired(token?.toString());
  }
  return;
  }
  LoginsEmpDetail(Email: string) {
    
    var data = { Email: Email}
    return this.http.post(this.baseurl + '/Account/Login', data, { headers: this.reqHeader })
  }
  ForgetPassword(Email: string) {
    var data = { Email: this.formData.Email }
    return this.http.post(this.baseurl + '/Account/forgotpassword', data);
  }
  ResetPassword(uid: string, Password: string, ConfirmPassword: string) {

    var data = { uid: uid, Password: this.formdata.Password, ConfirmPassword: this.formdata.ConfirmPassword }
    return this.http.post(this.baseurl + '/Account/ForgotPasswordLink', data);
  }
  ConfirmPassword(id: string, OldPassword: string, NewPassword: string, ConfirmPassword: string) {
    
    var data = { id: id, OldPassword: this.formdata.oldPassword,NewPassword:this.formdata.NewPassword, ConfirmPassword: this.formdata.ConfirmPassword }
    return this.http.post(this.baseurl+'/Account/ChangePassword',data);
  }
  PostUserDetail(id:string,firstName:string,lastName:string,mobileNo:string){
    
    var datadetail=JSON.parse(localStorage.res);
    id=datadetail.id;
    var data={id:id,firstName:this.List.firstName,lastName:this.List.lastName,mobileNo:this.List.mobileNo}
    return this.http.post(this.baseurl+'/Account/UpdateUser',data);
  }
  GetUserDetail(){
    debugger;
    var datadetail=JSON.parse(localStorage.res);
    this.formData.firstName=datadetail.firstName;
    this.formData.lastName=datadetail.lastName;
    return this.http.get(this.baseurl+'/Account/GetUser/'+datadetail.id).toPromise().then(res=> this.List=res as Login);
  }
  GetAllUser(){
    debugger;
    this.http.get(this.baseurl+'/UserManagment/GetUserList').toPromise()
    .then(res=>
      {
        debugger
        this.listdata=res as GetAll[]
      });
  }
  onDelete(id:string){
    debugger;
    return this.http.delete(`${this.baseurl}/UserManagment/Deleteuser/${id}`);
    
  }
  EditUser(id:string){
    var data = {id:id, firstname: this.List.firstName, lastname: this.List.lastName,MobileNo:this.List.mobileNo,email:this.List.email,username:this.List.username }
    return this.http.put(this.baseurl+'/UserManagment/EditUser',data);
  }
  ViewUser(id:string){
    debugger;
    return this.http.get(`${this.baseurl}/UserManagment/EditUser/${id}`).toPromise().then(res=> this.List=res as Login);
    
  }
  AddUsers(){
    debugger;
    var data = { firstname: this.formdata.FirstName, lastname: this.formdata.LastName,MobileNo:this.formdata.MobileNo,email:this.formdata.Email, password: this.formdata.Password,confirmpassword:this.formdata.ConfirmPassword,username:this.formdata.userName }
    return this.http.post(this.baseurl+'/UserManagment/AddUser',data)
  }
  IsActive(Id:string,isactive:boolean){
    debugger;
    var data={Id:Id,isactive:isactive}
    return this.http.post(this.baseurl+'/UserManagment/UserActive',data)
  }
}
