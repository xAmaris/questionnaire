import { Component, Input, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  NgForm,
  Validators
} from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../../../auth/services/account.service';
import { SharedService } from '../../../services/shared.service';

@Component({
  selector: 'app-profile-settings',
  templateUrl: './profile-settings.component.html',
  styleUrls: ['./profile-settings.component.scss']
})
export class ProfileSettingsComponent implements OnInit {
  @Input()
  userInfo;
  panelOpenState = false;
  registrationError = false;
  // declare form
  regForm: FormGroup;
  degree: AbstractControl;
  profileType: string;
  majorDisabled = undefined;
  // loader
  loading = false;
  // user object sent to API
  user: any = {};

  majorsDropdown: any = [];

  degrees = [
    { value: 'finances', viewValue: 'Finanse' },
    { value: 'management', viewValue: 'Zarządzanie' },
    { value: 'IT', viewValue: 'Informatyka' }
  ];
  majorsIT = [
    {
      value: 'web-apps',
      viewValue: 'Programowanie aplikacji mobilnych i webowych'
    },
    {
      value: 'gamedev',
      viewValue: 'Projektowanie i produkcja gier komputerowych'
    },
    {
      value: 'network-administration',
      viewValue: 'Administrowanie sieciami komputerowymi'
    },
    {
      value: 'BI',
      viewValue: 'Projektowanie i budowa systemów Business Intelligence'
    },
    {
      value: 'systems-implementation',
      viewValue: 'Wdrażanie systemów informatycznych wspomagających zarządzanie'
    }
  ];
  majorsManagement = [
    {
      value: 'marketing',
      viewValue: 'M@rketing i reklama w Sieci'
    },
    {
      value: 'logistic-management',
      viewValue: 'Zarządzanie logistyką zakupów i sprzedaży'
    },
    {
      value: 'sale-management',
      viewValue: 'Zarządzanie sprzedażą i relacjami z klientem'
    },
    {
      value: 'business-psychology',
      viewValue: 'Psychologia w biznesie'
    },
    {
      value: 'personnel-management',
      viewValue: 'Zarządzanie personelem i psychologia pracy'
    }
  ];
  majorsFinances = [
    {
      value: 'accountancy',
      viewValue: 'Rachunkowość w praktyce firm i instytucji'
    },
    {
      value: 'controlling',
      viewValue: 'Controlling i rachunkowość zarządcza '
    },
    {
      value: 'wages',
      viewValue: 'Kadry i płace w praktyce firmy '
    }
  ];

  studyModes = [
    { value: 'full-time', viewValue: 'Stacjonarne' },
    { value: 'extramural', viewValue: 'Niestacjonarne' }
  ];
  studyYears = [{ value: 1 }, { value: 2 }, { value: 3 }, { value: 4 }];
  studyYears2 = [{ value: 1 }, { value: 2 }, { value: 3 }];

  initialSemesters = [
    { value: 'summer', viewValue: 'Letni' },
    { value: 'winter', viewValue: 'Zimowy' }
  ];
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private accountService: AccountService,
    private sharedService: SharedService
  ) {
    this.getProfileType();
  }

  ngOnInit() {
    this.setMajorDropdown(this.userInfo.degree);
    // form declaration
    this.regForm = this.fb.group({
      degree: [this.userInfo.degree, Validators.compose([Validators.required])],
      major: [
        { value: this.userInfo.major, disabled: this.majorDisabled },
        Validators.compose([Validators.required])
      ],
      mode: [this.userInfo.mode, Validators.compose([Validators.required])],
      year: [this.userInfo.year, Validators.required],
      initialSemester: [this.userInfo.initialSemester, Validators.required]
    });
    // console.log(this.userInfo.degree);
    this.degree = this.regForm.controls['degree'];
    // input: [{ value: this.userInfo.major, disabled: this.disabled }];
  }

  isMajorDisabled() {
    // console.log(this.majorsDropdown);
    this.majorDisabled = this.majorsDropdown.length === 0 ? true : false;
  }
  setMajorDropdown(degree) {
    switch (degree) {
      case 'finances':
        this.majorsDropdown = this.majorsFinances;
        break;
      case 'management':
        this.majorsDropdown = this.majorsManagement;
        break;
      case 'IT':
        this.majorsDropdown = this.majorsIT;
        // console.log(this.majorsDropdown);
        break;
    }
  }

  getProfileType() {
    this.profileType = JSON.parse(localStorage.getItem('currentUser')).role;
    // console.log(this.profileType);
  }

  setAdditionalControls() {
    if (this.profileType === 'student') {
    } else if (this.profileType === 'employer') {
      // this.regForm.addControl(
      //   'companyName',
      //   new FormControl(this.userInfo.companyName, Validators.required)
      // );
      // this.regForm.addControl(
      //   'location',
      //   new FormControl(this.userInfo.location, Validators.required)
      // );
      // this.regForm.addControl(
      //   'companyDescription',
      //   new FormControl(this.userInfo.companyDescription)
      // );
      // this.companyName = this.regForm.controls['companyName'];
      // this.location = this.regForm.controls['location'];
      // this.companyDescription = this.regForm.controls['companyDescription'];
    }
  }
  onSubmit(form: NgForm): void {
    if (!form.valid) {
      console.log('not valid');
    } else {
      this.loading = true;
      this.createUser();
      console.log(this.user);
      this.accountService.updateProfile(this.user).subscribe(
        data => {
          console.log(data);
        },
        error => {
          console.log(error);
        }
      );
    }
  }

  createUser(): void {
    this.user.id = this.userInfo.id;

    // switch (this.profileName.value) {
    //   case 'Student':
    //     this.user.albumID = this.albumID.value;
    //     break;
    //   case 'Employer':
    //     this.user.companyName = this.companyName.value;
    //     this.user.location = this.location.value;
    //     this.user.companyDescription = this.companyDescription.value;
    //     break;
    // }
  }

  inputError(control: AbstractControl): boolean {
    // get error message and control name in string
    const errorObj = this.sharedService.inputError(control);

    // assign error to input
    if (errorObj) {
      switch (
        errorObj.controlName
        // case 'name':
        //   this.nameErrorStr = errorObj.errorStr;
        //   break;
        // case 'last name':
        //   this.lastNameErrorStr = errorObj.errorStr;
        //   break;
        // case 'email':
        //   this.emailErrorStr = errorObj.errorStr;
        //   break;
        // case 'password':
        //   this.passwordErrorStr = errorObj.errorStr;
        //   break;
        // case 'albumID':
        //   this.albumIDErrorStr = errorObj.errorStr;
        //   break;
        // case 'phone number':
        //   this.phoneNumErrorStr = errorObj.errorStr;
        //   break;
      ) {
      }
      return true;
    }
  }

  onFocus(control: AbstractControl): void {
    // hide possible errors
    // if (control.touched) {
    control.markAsUntouched();
    // }
    this.registrationError = false;
  }

  onBlur(control: AbstractControl): void {
    // hide possible errors
    if (control.dirty === false) {
      control.markAsUntouched();
      this.registrationError = false;
    }
  }
}
