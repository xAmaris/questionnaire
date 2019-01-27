import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';
import { Router } from '@angular/router';
import { SharedService } from '../../../services/shared.service';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-password-recovery',
  templateUrl: './password-recovery.component.html',
  styleUrls: ['./password-recovery.component.scss']
})
export class PasswordRecoveryComponent implements OnInit {
  // loader
  loading = false;

  // string mail from login component
  mail: string;

  // declare form
  passForm: FormGroup;
  email: AbstractControl;
  emailErrorStr: string;
  // tslint:disable-next-line:max-line-length
  emailPattern = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private sharedService: SharedService,
    private router: Router
  ) {}

  ngOnInit() {
    // get mail string
    this.mail = this.accountService.getMailData();

    // form declaring
    this.passForm = this.fb.group({
      email: [
        this.mail,
        Validators.compose([
          Validators.required,
          Validators.pattern(this.emailPattern)
        ])
      ]
    });

    // connecting controls with form inputs
    this.email = this.passForm.controls['email'];
  }

  onSubmit(): void {
    this.loading = true;
    console.log(this.email.value);
    this.accountService.sendRestorePasswordEmail(this.email.value).subscribe(
      data => {
        this.router.navigateByUrl('auth/login');
      },
      error => {
        console.log(error);
        this.loading = false;
      }
    );
  }

  inputError(control: AbstractControl): boolean {
    // get error message and control name in string
    const errorObj = this.sharedService.inputError(control);

    // assign error to input
    if (errorObj) {
      switch (errorObj.controlName) {
        case 'email':
          this.emailErrorStr = errorObj.errorStr;
          break;
      }
      return true;
    }
  }
}
