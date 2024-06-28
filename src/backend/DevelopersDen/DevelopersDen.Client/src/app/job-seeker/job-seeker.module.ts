import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JobSeekerComponent } from './job-seeker.component';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { HeaderComponent } from './components/protected/header/header.component';

import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatChipsModule } from '@angular/material/chips';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatRippleModule } from '@angular/material/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatDividerModule } from '@angular/material/divider';
import { MatRadioModule } from '@angular/material/radio';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProfileComponent } from './components/protected/profile/profile.component';
import { HomeComponent } from './components/protected/home/home.component';
import { JobApplicationsComponent } from './components/protected/job-applications/job-applications.component';
import { LoginCompletedComponent } from './components/login-completed/login-completed.component';
import { KeySkillsComponent } from './components/key-skills/key-skills.component';

@NgModule({
  declarations: [
    JobSeekerComponent,
    LoginComponent,
    RegisterComponent,
    HeaderComponent,
    ProfileComponent,
    HomeComponent,
    JobApplicationsComponent,
    LoginCompletedComponent,
    KeySkillsComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    MatIconModule,
    MatCardModule,
    MatMenuModule,
    MatButtonModule,
    MatToolbarModule,
    MatInputModule,
    MatFormFieldModule,
    MatStepperModule,
    MatChipsModule,
    MatSelectModule,
    MatPaginatorModule,
    MatRippleModule,
    MatGridListModule,
    MatDividerModule,
    MatRadioModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatNativeDateModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers:[MatDatepickerModule,MatNativeDateModule]
})
export class JobSeekerModule { }
