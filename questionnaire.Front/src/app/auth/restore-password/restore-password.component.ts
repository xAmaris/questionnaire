import { Component} from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';
import { Router } from '@angular/router';
import { SharedService } from '../../services/shared.service';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-restore-password',
  templateUrl: './restore-password.component.html',
  styleUrls: ['./restore-password.component.scss']
})
export class RestorePasswordComponent {
  loader = false;
  passwordForm: FormGroup;
  passwordErrorStr: string;
  password: AbstractControl;
  token: string;
  href: string[];
  passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}/;
  constructor(
    private router: Router,
    private fb: FormBuilder,
    private accountService: AccountService,
    private sharedService: SharedService
  ) {
    this.passwordForm = this.fb.group({
      password: [
        '',
        [Validators.required, Validators.pattern(this.passwordPattern)]
      ]
    });
    this.password = this.passwordForm.controls['password'];
    this.href = this.router.url.split('/');
    this.token = this.href[this.href.length - 1];
  }

  onSubmit(form) {
    if (form.valid) {
      this.loader = true;
      this.accountService
        .changePasswordByRestoringPassword(this.token, this.password.value)
        .subscribe(
          data => {
            this.loader = false;
            this.router.navigateByUrl('auth/login');
          },
          error => {
            this.loader = false;
          }
        );
    }
  }
  inputError(control: AbstractControl): boolean {
    // get error message and control name in string
    const errorObj = this.sharedService.inputError(control);

    // assign error to input
    if (errorObj) {
      switch (errorObj.controlName) {
        case 'password':
          this.passwordErrorStr = errorObj.errorStr;
          break;
      }
      return true;
    }
  }
}
