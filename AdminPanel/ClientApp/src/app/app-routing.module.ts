import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeLoginComponent } from "../app/Employee/employee-login/employee-login.component";
import { RegistrationComponent } from "../app/Employee/registration/registration.component";
import { EmployeeForgetPasswordComponent } from "../app/Employee/employee-forget-password/employee-forget-password.component";
import { ChangePasswordComponent } from "../app/Employee/change-password/change-password.component";
import { DashboardComponent } from './Employee/dashboard/dashboard.component';
import { ForgetPassword2Component } from "./Employee/forget-password2/forget-password2.component";
import { ProfileComponent } from "./Employee/profile/profile.component";
import { UserManagementComponent } from './Employee/user-management/user-management.component';
import { UserRolesComponent } from './Employee/user-roles/user-roles.component';
import { AddUserComponent } from './Employee/add-user/add-user.component';
import { UsersdetailsComponent } from './Employee/usersdetails/usersdetails.component';
const routes: Routes = [
  { path: 'Login', component: EmployeeLoginComponent},
  { path: '', component: EmployeeLoginComponent},
  { path: 'Register', component: RegistrationComponent},
  { path: 'Forget', component: EmployeeForgetPasswordComponent },
  { path: 'Forget2', component: ForgetPassword2Component },
  {path:'Change/:id',component:ChangePasswordComponent},
  { path: 'DashBoard', component: DashboardComponent },
  { path: 'Profile/:id', component: ProfileComponent },
  {path:'Users',component:UserManagementComponent},
  {path:'UsersRole',component:UserRolesComponent},
  {path:'AddUser',component:AddUserComponent},
  {path:'Usersdetail/:id',component:UsersdetailsComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
