import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';
import { emailPattern } from 'src/app/patterns/email.pattern';
import { AccountService } from '../services/account.service';

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

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService
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
          Validators.pattern(emailPattern)
        ])
      ]
    });
  }

  onSubmit(): void {}
}
