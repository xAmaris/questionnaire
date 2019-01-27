import { Component, Input, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';
import {
  ProfileDataStorage,
  UserProfile
} from '../../../auth/other/user.model';
import { AccountService } from '../../../auth/services/account.service';
import { SharedService } from '../../../services/shared.service';
@Component({
  selector: 'app-main-settings',
  templateUrl: './main-settings.component.html',
  styleUrls: ['./main-settings.component.scss']
})
export class MainSettingsComponent implements OnInit {
  created = false;
  loading = false;
  loader = true;
  private _userInfo: UserProfile;
  @Input()
  set userInfo(userInfo) {
    this._userInfo = userInfo;
    if (userInfo) {
      this.setValue();
    }
  }
  get userInfo() {
    return this._userInfo;
  }

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
  companyName: AbstractControl;
  location: AbstractControl;
  companyDescription: AbstractControl;

  // error handlers
  nameErrorStr: string;
  lastNameErrorStr: string;
  countryErrorStr: string;
  emailErrorStr: string;
  passwordErrorStr: string;
  // passwordConfirmErrorStr: string;
  albumIDErrorStr: string;
  phoneNumErrorStr: string;
  companyNameErrorStr: string;
  locationErrorStr: string;
  companyDescriptionErrorStr: string;
  registrationError = false;
  registrationErrorMessage: string[];

  profileType: string;

  // user object sent to API
  user: UserProfile;

  // tslint:disable-next-line:max-line-length
  emailPattern = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  namePattern = /^([a-zA-ZąęćłóśźżĄĘĆŁÓŚŹŻ\\'])*$/;
  surnamePattern = /^([a-zA-ZąęćłóśźżĄĘĆŁÓŚŹŻ]+[\s\-\\'])*[a-zA-ZąęćłóśźżĄĘĆŁÓŚŹŻ]+$/;

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private sharedService: SharedService
  ) {
    this.getProfileType();
  }

  ngOnInit() {
    this.formDeclaration();
  }
  formDeclaration() {
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
      phoneNum: ['', Validators.required]
    });
    // connecting controls with form inputs
    this.name = this.regForm.controls['name'];
    this.lastName = this.regForm.controls['lastName'];
    this.email = this.regForm.controls['email'];
    this.password = this.regForm.controls['password'];
    this.passwordConfirm = this.regForm.controls['passwordConfirm'];
    this.profileName = this.regForm.controls['profileName'];
    this.phoneNum = this.regForm.controls['phoneNum'];
    this.created = true;
  }
  setValue() {
    this.name.setValue(this._userInfo.firstName);
    this.lastName.setValue(this._userInfo.lastName);
    this.email.setValue(this._userInfo.email);
    this.phoneNum.setValue(this._userInfo.phoneNum);
    this.loader = false;
  }
  getProfileType() {
    this.profileType = JSON.parse(localStorage.getItem('currentUser')).role;
  }

  onSubmit(form: FormGroup): void {
    if (!form.valid) {
    } else {
      this.loading = true;
      this.accountService.updateProfile(this.createUser()).subscribe(() => {
        this.setLocalStorage();
      });
    }
  }
  setLocalStorage() {
    const user: ProfileDataStorage = JSON.parse(
      localStorage.getItem('currentUser')
    );
    this.createProfileDataStorage(user);
    localStorage.setItem('currentUser', JSON.stringify(user));
    this.setProfileData();
  }
  setProfileData() {
    this.accountService.setProfileData();
  }
  createUser() {
    const _user: UserProfile = {
      firstName: this.name.value,
      lastName: this.lastName.value,
      email: this.email.value,
      phoneNum: this.phoneNum.value
    };
    return _user;
  }
  createProfileDataStorage(user: ProfileDataStorage): void {
    user.name = this.name.value;
    user.surname = this.lastName.value;
    user.email = this.email.value;
    user.phoneNumber = this.phoneNum.value;
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
        case 'lastName':
          this.lastNameErrorStr = errorObj.errorStr;
          break;
        case 'email':
          this.emailErrorStr = errorObj.errorStr;
          break;
        case 'password':
          this.passwordErrorStr = errorObj.errorStr;
          break;
        case 'phoneNum':
          this.phoneNumErrorStr = errorObj.errorStr;
          break;
      }
      return true;
    }
  }
}
