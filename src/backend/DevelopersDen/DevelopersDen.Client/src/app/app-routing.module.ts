import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { JobSeekerComponent } from './job-seeker/job-seeker.component';
import { LoginComponent } from './job-seeker/components/login/login.component';
import { RegisterComponent } from './job-seeker/components/register/register.component';
import { HeaderComponent } from './job-seeker/components/protected/header/header.component';
import { ProfileComponent } from './job-seeker/components/protected/profile/profile.component';
import { HomeComponent } from './job-seeker/components/protected/home/home.component';
import { JobApplicationsComponent } from './job-seeker/components/protected/job-applications/job-applications.component';
import { LoginCompletedComponent } from './job-seeker/components/login-completed/login-completed.component';

const routes: Routes = [
  { path: 'jobSeeker', component: JobSeekerComponent, children: [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'login-completed', component: LoginCompletedComponent },
    { path: '', component: HeaderComponent, children: [
      { path: 'profile', component: ProfileComponent },
      { path: 'home', component: HomeComponent },
      { path: 'my-applications', component: JobApplicationsComponent }
    ] }
  ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
