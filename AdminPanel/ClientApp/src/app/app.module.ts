import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EmployeeLoginComponent } from './Employee/employee-login/employee-login.component';
import { HttpClientModule } from '@angular/common/http';
import { EmployeeService } from './Shared/employee.service';
import {  ReactiveFormsModule,FormsModule } from '@angular/forms';
import { EmployeeForgetPasswordComponent } from './Employee/employee-forget-password/employee-forget-password.component';
import { RegistrationComponent } from './Employee/registration/registration.component';
import { DashboardComponent } from './Employee/dashboard/dashboard.component';
import { CommonModule } from '@angular/common';
import { ForgetPassword2Component } from './Employee/forget-password2/forget-password2.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { ChangePasswordComponent } from './Employee/change-password/change-password.component';
import { ProfileComponent } from './Employee/profile/profile.component';
import { UserManagementComponent } from './Employee/user-management/user-management.component';
import { UserRolesComponent } from './Employee/user-roles/user-roles.component';
import { AddUserComponent } from './Employee/add-user/add-user.component';
import { GetUserComponent } from './Employee/get-user/get-user.component';
import { UsersdetailsComponent } from './Employee/usersdetails/usersdetails.component';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
let appRoutes: Routes = [
  { path: '', component: RegistrationComponent },
  { path: 'Login', component: EmployeeLoginComponent },
  { path: '', component: EmployeeForgetPasswordComponent },

];
@NgModule({
  declarations: [
    AppComponent,
    EmployeeLoginComponent,
    EmployeeForgetPasswordComponent,
    RegistrationComponent,
    DashboardComponent,
    
    ForgetPassword2Component,
         ChangePasswordComponent,
         ProfileComponent,
         UserManagementComponent,
         UserRolesComponent,
         AddUserComponent,
         GetUserComponent,
         UsersdetailsComponent,

  ],
  imports: [
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 3500,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }),
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    Ng2SearchPipeModule,
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true } // <-- debugging purposes only
    )
  ],
  providers: [EmployeeService],
  bootstrap: [AppComponent]
})
export class AppModule { }


