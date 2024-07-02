import {LiveAnnouncer} from '@angular/cdk/a11y';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import {ChangeDetectionStrategy, Component, OnInit, inject, signal} from '@angular/core';
import {MatChipEditedEvent, MatChipInputEvent, MatChipsModule} from '@angular/material/chips';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import { JobService } from '../../../services/job/job.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { JobSearchRequest, JobType } from '../../../models/request/jobSearchrequest';
import { PageEvent } from '@angular/material/paginator';
import { Job, JobDTO } from '../../../models/response/job';

export interface KeySkills {
  name: string;
}
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  companyName! :string;
  location!: string;
  jobType! : number;
  pageSize = 10; 
  pageNumber = 1;
  jobs!: JobDTO;
  jobTypes!: JobType[];
  hasDataLoaded: boolean = false;
  isSelected: boolean = false;
  selectedJob!: Job;
  readonly addOnBlur = true;
  readonly separatorKeysCodes = [ENTER, COMMA] as const;
  readonly skills = signal<KeySkills[]>([]);
  readonly announcer = inject(LiveAnnouncer);

  constructor(private fb: FormBuilder, private jobService: JobService)
  {
  }
  ngOnInit(): void {
    this.initialiseData();
  }

  initialiseData(){
    this.jobService.getJobType().subscribe(res => {
      this.jobTypes = res;
    });
    this.searchForJobs();
  }

  updateJobtype(value:number){
    this.jobType = value;
  }

  viewInfo(job: Job){
    this.selectedJob = job;
    this.isSelected = true;
  }

  getJobTypeName(jobTypeId: number): string {
    const jobType = this.jobTypes.find(type => type.id === jobTypeId);
    return jobType ? jobType.name : 'Unknown';
  }

  onPageChange(event: PageEvent): void {
    console.log(event.pageIndex);
    this.pageSize = event.pageSize;
    this.pageNumber = event.pageIndex + 1;
    this.searchForJobs();
  }
  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

    // Add our fruit
    if (value) {
      this.skills.update(skills => [...skills, {name: value}]);
    }

    // Clear the input value
    event.chipInput!.clear();
  }

  remove(skill: KeySkills): void {
    this.skills.update(skills => {
      const index = skills.indexOf(skill);
      if (index < 0) {
        return skills;
      } 

      skills.splice(index, 1);
      this.announcer.announce(`Removed ${skill.name}`);
      return [...skills];
    });
  }

  edit(skill: KeySkills, event: MatChipEditedEvent) {
    const value = event.value.trim();

    // Remove fruit if it no longer has a name
    if (!value) {
      this.remove(skill);
      return;
    }

    // Edit existing fruit
    this.skills.update(skills => {
      const index = skills.indexOf(skill);
      if (index >= 0) {
        skills[index].name = value;
        return [...skills];
      }
      return skills;
    });
  }

  getValues() {
    const values = this.skills().map(skill => skill.name);
    console.log(values);
    return values;
  }

  applyJob(){
    this.jobService.addjobApplication(this.selectedJob.jobId, 1).subscribe(res =>{
      if(res == 'true' || res == true){
        var index = this.jobs.jobs.findIndex(x => x.jobId == this.selectedJob.jobId);
        this.jobs.jobs[index].hasApplied = true;
      }
    })
  }
  searchForJobs(){
    this.hasDataLoaded = false;
    const apiBody: JobSearchRequest = {
      companyName: this.companyName,
      keySkills: this.getValues(),
      location: this.location,
      jobType: this.jobType,
    };

    console.log(this.jobType)
    console.log(JSON.stringify(apiBody));

    this.jobService.getjobs(apiBody, this.pageNumber, this.pageSize).subscribe(res => {
      this.jobs = res;
      this.hasDataLoaded = true;
    });
  }

  constructAPIRequest(){
  }
}
