import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  NgForm,
  Validators
} from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { SharedService } from '../../../shared/services/shared.service';
import { Admin, Student } from '../other/user.model';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit, OnDestroy {
  // declare form
  regForm: FormGroup;
  name: AbstractControl;
  lastName: AbstractControl;
  email: AbstractControl;
  password: AbstractControl;
  passwordConfirm: AbstractControl;
  profileName: AbstractControl;
  albumID: AbstractControl;
  phoneNum: AbstractControl;

  // error handlers
  nameErrorStr: string;
  lastNameErrorStr: string;
  emailErrorStr: string;
  passwordErrorStr: string;
  passwordConfirmErrorStr: string;
  albumIDErrorStr: string;
  phoneNumErrorStr: string;
  registrationError = false;
  registrationErrorMessage: string[];

  defaultProfile = 'Student';
  // user object sent to API
  user: Student | Admin;
  // loader
  loading = false;
  // profiles tooltip
  profiles = [
    { value: 'Student', icon: 'pen', message: 'Student' },
    { value: 'Admin', icon: 'briefcase', message: 'Admin' }
  ];

  // tslint:disable-next-line:max-line-length
  emailPattern = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}/;
  namePattern = /^([a-zA-ZąęćłóśźżĄĘĆŁÓŚŹŻ\\']){0,}$/;
  surnamePattern = /^([a-zA-ZąęćłóśźżĄĘĆŁÓŚŹŻ]+[\s\-\\'])*[a-zA-ZąęćłóśźżĄĘĆŁÓŚŹŻ]+$/;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private accountService: AccountService,
    private sharedService: SharedService,
    private titleService: Title
  ) {}

  ngOnDestroy() {
    this.sharedService.deleteControlArray();
  }
  ngOnInit() {
    this.titleService.setTitle('Rejestracja');
    // form declaration
    this.regForm = this.fb.group({
      name: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern(this.namePattern)
        ])
      ],
      lastName: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern(this.surnamePattern)
        ])
      ],
      email: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern(this.emailPattern)
        ])
      ],
      password: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern(this.passwordPattern)
        ])
      ],
      passwordConfirm: [
        '',
        Validators.compose([Validators.required, this.matchPassword])
      ],
      profileName: ['Student', Validators.required],
      albumID: ['', Validators.required],
      phoneNum: ['', Validators.required]
    });

    // connecting controls with form inputs
    this.name = this.regForm.controls.name;
    this.lastName = this.regForm.controls.lastName;
    this.email = this.regForm.controls.email;
    this.password = this.regForm.controls.password;
    this.passwordConfirm = this.regForm.controls.passwordConfirm;
    this.profileName = this.regForm.controls.profileName;
    this.albumID = this.regForm.controls.albumID;
    this.phoneNum = this.regForm.controls.phoneNum;
    this.hide(this.defaultProfile);
  }

  hide(profile) {
    this.setAllAsUntouched();
    if (profile === 'Admin') {
      this.albumID.clearValidators();
      this.albumID.updateValueAndValidity();
    }
  }
  show(profile) {
    if (profile === 'Student') {
      this.albumID.setValidators([Validators.required]);
      this.albumID.updateValueAndValidity();
    }
  }

  onSubmit(form: NgForm): void {

    if (!form.valid) {
      // showing possible errors
      this.setAllAsTouched();
    } else {
      this.loading = true;
      this.createUser();
      // create new user
      switch (this.profileName.value) {
        case 'Student':
          this.accountService.createStudent(this.user).subscribe(
            data => {
              this.router.navigateByUrl('/auth/login');
            },
            error => {
              this.loading = false;
              this.registrationError = true;
              // set error message from api to loginErrorMessage
              this.registrationErrorMessage = error;
            }
          );
          break;
        case 'Admin':
          this.accountService.createNewAdmin(this.user).subscribe(
            data => {
              this.router.navigateByUrl('/auth/login');
            },
            error => {
              this.loading = false;
              this.registrationError = true;
              // set error message from api to loginErrorMessage
              this.registrationErrorMessage = error.error;
            }
          );
          break;
      }
    }
  }

  createUser(): void {
    switch (this.profileName.value) {
      case 'Student':
        this.user = new Student();
        (this.user as Student).albumID = this.albumID.value;
        break;
      case 'Admin':
        this.user = new Admin();
    }
    this.user.firstName = this.name.value;
    this.user.lastName = this.lastName.value;
    this.user.email = this.email.value;
    this.user.password = this.password.value;
    this.user.profileName = this.profileName.value;
    const phoneNumString: string = this.phoneNum.value;
    this.user.phoneNum = phoneNumString.startsWith('+')
      ? phoneNumString
      : '+48' + phoneNumString;
  }

  setAllAsTouched(): void {
    this.name.markAsTouched();
    this.lastName.markAsTouched();
    this.email.markAsTouched();
    this.password.markAsTouched();
    this.passwordConfirm.markAsTouched();
    this.albumID.markAsTouched();
    this.phoneNum.markAsTouched();
  }
  setAllAsUntouched(): void {
    this.name.markAsUntouched();
    this.lastName.markAsUntouched();
    this.email.markAsUntouched();
    this.password.markAsUntouched();
    this.passwordConfirm.markAsUntouched();
    this.albumID.markAsUntouched();
    this.phoneNum.markAsUntouched();
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

  clearPasswordConfirm(): void {
    // clear confirm password input after changing password input
    this.passwordConfirm.setValue('');
    this.passwordConfirm.markAsUntouched();
  }

  matchPassword(control: AbstractControl): { [s: string]: boolean } {
    // check if inputs have same values
    if (control.parent !== undefined) {
      const password = control.parent.get('password').value;
      const passwordConfirm = control.parent.get('passwordConfirm').value;
      if (password !== passwordConfirm) {
        return { noMatch: true };
      }
    }
  }

  inputError(control: AbstractControl): boolean {
    // get error message and control name in string
    const errorObj = this.sharedService.inputError(control);

    // assign error to input
    if (errorObj) {
      switch (errorObj.controlName) {
        case 'name':
          this.nameErrorStr = errorObj.errorStr;
          break;
        case 'last name':
          this.lastNameErrorStr = errorObj.errorStr;
          break;
        case 'email':
          this.emailErrorStr = errorObj.errorStr;
          break;
        case 'password':
          this.passwordErrorStr = errorObj.errorStr;
          break;
        case 'albumID':
          this.albumIDErrorStr = errorObj.errorStr;
          break;
        case 'phone number':
          this.phoneNumErrorStr = errorObj.errorStr;
          break;
      }
      return true;
    }
  }

  passwordNoMatch(): boolean {
    if (this.passwordConfirm.errors) {
      if (this.passwordConfirm.errors.noMatch === undefined) {
        this.passwordConfirmErrorStr = 'Passwords do not match';
        return true;
      }
    } else {
      return false;
    }
  }
}
