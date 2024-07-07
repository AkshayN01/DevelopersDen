import {LiveAnnouncer} from '@angular/cdk/a11y';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import {ChangeDetectionStrategy, Component, inject, signal} from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { KeySkills } from '../home/home.component';
import { MatChipEditedEvent, MatChipInputEvent } from '@angular/material/chips';
import { JobSeekerProfileDTO } from '../../../models/response/profile';
import { ProfileService } from '../../../services/profile/profile.service';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { GenericService } from '../../../../shared/services/generic/generic.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../../environments/environment';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
  hasProfileData: boolean = false;
  profileForm: FormGroup;
  initialData!: JobSeekerProfileDTO;  // This will hold the data to display and edit
  display: FormControl = new FormControl("", Validators.required);
  isEditing: boolean = false;
  isFirstVisit: boolean = false;


  //mat-chip
  readonly addOnBlur = true;
  readonly separatorKeysCodes = [ENTER, COMMA] as const;
  readonly skills = signal<KeySkills[]>([]);
  readonly announcer = inject(LiveAnnouncer);
  
  constructor(private fb: FormBuilder, private profileService: ProfileService, private sanitizer: DomSanitizer, private genericService: GenericService, private http: HttpClient) {
    this.profileForm = this.fb.group({
      workHistory: this.fb.array([], this.workHistoryIsCurrentValidator()),
      resume: [null],
      keySkills: this.fb.array([]),
      summary: ['']
    });
  }

  setConditionalValidator() {
    const conditionalField1 = this.profileForm.get('resume');
    const conditionalField2 = this.profileForm.get('summary');
    if (!this.isEditing) {
      conditionalField1?.setValidators([Validators.required]);
      conditionalField2?.setValidators([Validators.required]);
    } else {
      conditionalField1?.clearValidators();
      conditionalField2?.clearValidators();
    }
    conditionalField1?.updateValueAndValidity();
    conditionalField2?.updateValueAndValidity();
  }

  ngOnInit(): void {
    this.getInitialData();
  }

  get workHistory(): FormArray {
    return this.profileForm.get('workHistory') as FormArray;
  }

  enableEditing = (): void => {
    this.isEditing = true;
    console.log("Hit")
  }
  getInitialData(): void {
    this.hasProfileData = false;
    this.profileService.getProfile().subscribe(data => {
      this.initialData = data;
      console.log(data);
      this.hasProfileData = true;
      if(data != null){
        this.initializeForm(this.initialData);
      }
      else{
        this.isEditing = true;
        this.isFirstVisit = true;
      }
    }, (err) => {
      this.hasProfileData = true;
        this.isEditing = true;
        this.isFirstVisit = true;
    });
  }

  initializeForm(data: JobSeekerProfileDTO): void {
    console.log(data.keySkills)
    if(data.workExperience.length != 0){
    data.workExperience.forEach((work: any) => {
      if(work.isCurrent == "0")
        work.isCurrent == false;
      if(work.isCurrent == "1")
        work.isCurrent = true;
      const workGroup = this.fb.group({
        companyName: [work.companyName, Validators.required],
        designation: [work.designation, Validators.required],
        workDescription: [work.workDescription, Validators.required],
        startDate: [work.startDate, Validators.required],
        endDate: [work.endDate],
        isCurrent: [work.isCurrent]
      }, { validators: this.endDateRequiredValidator });
      this.workHistory.push(workGroup);
    });
  }
    this.profileForm.patchValue({
      // keySkills: this.fb.array(data.keySkills),
      summary: data.summary,
      resume: this.getBlob()
    });
    this.profileForm.setControl('keySkills', this.fb.array(data.keySkills));
    data.keySkills.forEach(val => {
      this.skills.update(skills => [...skills, {name: val}]);
    });
    console.log(this.profileForm.get('keySkills'))
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

  endDateRequiredValidator: ValidatorFn = (control: AbstractControl): { [key: string]: any } | null => {
    const isCurrent = control.get('isCurrent')?.value;
    const endDate = control.get('endDate')?.value;

    if (!isCurrent && !endDate) {
      return { endDateRequired: true };
    }
    return null;
  };

  workHistoryIsCurrentValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const workHistory = control as FormArray;
      const isCurrentCount = workHistory.controls
        .filter(group => group.get('isCurrent')?.value)
        .length;

      if (isCurrentCount > 1) {
        return { uniqueIsCurrent: true };
      }
      return null;
    };
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

  get keySkills(): FormArray {
    return this.profileForm.get('keySkills') as FormArray;
  }

  addSkill(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    // Add our skill
    if ((value || '').trim()) {
      this.keySkills.push(this.fb.control(value.trim()));
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }
  }

  removeSkill(index: number): void {
    if (index >= 0) {
      this.keySkills.removeAt(index);
    }
  }


  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

    // Add our fruit
    if (value) {
      this.skills.update(skills => [...skills, {name: value}]);
    }

    // Clear the input value
    event.chipInput!.clear();

    console.log(this.skills);
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

  getResumeUrl(): SafeUrl {
    const blob = this.getBlob();
    const url = window.URL.createObjectURL(blob);
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }

  getBlob(): Blob{
    // var data = this.initialData.resume.data;
    const byteCharacters = atob(this.initialData.resume.data); // Decode base64 to binary
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
      byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    return new Blob([byteArray], { type: this.initialData.resume.contentType });
  }
  downloadFile() {

    this.profileService.downloadFile().subscribe(blob => {
      console.log(blob);  
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = 'downloadedFile.pdf';
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
      window.URL.revokeObjectURL(url);
    }, error => {
      console.error('Download error:', error);
    });
  }

  cancelEdit(){
    this.isEditing = false;
  }

  onSubmit(): void {
    if (this.profileForm.valid) {
      console.log(this.profileForm.get('keySkills')?.value);
      // Handle form submission logic, such as sending data to the server
      this.constructRequestData();
      // this.profileService.saveProfile(requestBody).subscribe(data => {
      //   console.log(data);
      // })
    }
  }

  constructRequestData(): void{

    const formData = new FormData();

    //resume
    formData.append('resume', this.profileForm.get('resume')?.value);

    //summary
    formData.append('summary', this.profileForm.get('summary')?.value)

    //key skills
    var keySkillsData = this.profileForm.get('keySkills')?.value;

    keySkillsData.forEach((item:any, index:number) => {
      formData.append(`keySkills[${index}]`, item);
    })

    //work history
    var workHistoryData = this.profileForm.get('workHistory')?.value;

    workHistoryData.forEach((item:any, index:number) => {
      formData.append(`workExperience[${index}].companyName`, item.companyName);
      formData.append(`workExperience[${index}].designation`, item.designation);
      formData.append(`workExperience[${index}].workDescription`, item.workDescription);
      formData.append(`workExperience[${index}].startDate`, this.genericService.convertDate(item.startDate));
      formData.append(`workExperience[${index}].endDate`, this.genericService.convertDate(item.endDate));
      formData.append(`workExperience[${index}].isCurrent`, item.isCurrent == 0 ? false : item.isCurrent == 1 ? true : item.isCurrent);
    });
    console.log(this.isEditing);
    if(this.isEditing && !this.isFirstVisit){
      this.http.put(environment.jobSeeker.apiUrl+'update-profile', formData).subscribe(response => {
        // if(response == true){
          this.genericService.openSnackBar('Successfully updated profile');
          this.getInitialData();
          this.isEditing = false;
        // }
      }, error => {
        this.genericService.openSnackBar('Upload Error');
        console.error('Upload error:', error);
      });
    }
    else{
      this.http.post(environment.jobSeeker.apiUrl+'add-profile', formData).subscribe(response => {
        this.genericService.openSnackBar('Successfully added profile');
        this.getInitialData();
        this.isEditing = false;
        console.log('Upload successful:', response);
      }, error => {
        this.genericService.openSnackBar('Upload Error');
        console.error('Upload error:', error);
      });
    }
    
  }
}
