import {LiveAnnouncer} from '@angular/cdk/a11y';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import {ChangeDetectionStrategy, Component, inject, signal} from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { KeySkills } from '../home/home.component';
import { MatChipEditedEvent, MatChipInputEvent } from '@angular/material/chips';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
  profileForm: FormGroup;
  initialData: any;  // This will hold the data to display and edit
  display: FormControl = new FormControl("", Validators.required);

  //mat-chip
  readonly addOnBlur = true;
  readonly separatorKeysCodes = [ENTER, COMMA] as const;
  readonly skills = signal<KeySkills[]>([]);
  readonly announcer = inject(LiveAnnouncer);
  
  constructor(private fb: FormBuilder) {
    this.profileForm = this.fb.group({
      workHistory: this.fb.array([]),
      resume: [null, Validators.required],
      keySkills: ['', Validators.required],
      summary: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.initialData = this.getInitialData();
    this.initializeForm(this.initialData);
  }

  get workHistory(): FormArray {
    return this.profileForm.get('workHistory') as FormArray;
  }

  getInitialData(): any {
    // This function will return the initial data. Replace with actual data source.
    return {
      workHistory: [
        {
          companyName: 'Company A',
          designation: 'Developer',
          workDescription: 'Developing applications',
          startDate: '2020-01-01',
          endDate: '2022-01-01',
          isCurrent: false
        }
      ],
      resume: null,
      keySkills: 'Angular, TypeScript, JavaScript',
      summary: 'Experienced developer with a background in building applications'
    };
  }

  initializeForm(data: any): void {
    data.workHistory.forEach((work: any) => {
      const workGroup = this.fb.group({
        companyName: [work.companyName, Validators.required],
        designation: [work.designation, Validators.required],
        workDescription: [work.workDescription, Validators.required],
        startDate: [work.startDate, Validators.required],
        endDate: [work.endDate, Validators.required],
        isCurrent: [work.isCurrent]
      });
      this.workHistory.push(workGroup);
    });

    this.profileForm.patchValue({
      keySkills: data.keySkills,
      summary: data.summary
    });
  }

  addWorkHistory(): void {
    const workGroup = this.fb.group({
      companyName: ['', Validators.required],
      designation: ['', Validators.required],
      workDescription: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      isCurrent: [false]
    });
    this.workHistory.push(workGroup);
  }

  removeWorkHistory(index: number): void {
    this.workHistory.removeAt(index);
  }

  onFileChange(event: any): void {
    const file = event.target.files[0];
    console.log(file.name);
    this.display.patchValue(`${file.name}`);
    this.profileForm.patchValue({
      resume: file
    });
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
  
  onSubmit(): void {
    if (this.profileForm.valid) {
      console.log(this.profileForm.value);
      // Handle form submission logic, such as sending data to the server
    }
  }
}
