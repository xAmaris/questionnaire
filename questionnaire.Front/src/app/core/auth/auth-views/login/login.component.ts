import { AccountService } from './../services/account.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { emailPattern } from 'src/app/patterns/email.pattern';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  logForm: FormGroup;

  loginErrorMessage: string;
  emailErrorStr: string;

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService
  ) {}

  ngOnInit() {
    this.setForm();
  }
  setForm() {
    this.logForm = this.fb.group({
      email: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern(emailPattern)
        ])
      ],
      password: ['', Validators.compose([Validators.required])]
    });
  }
  onSubmit() {}
  setRecoveryRoute() {
    this.accountService.passMailData(this.logForm.controls.email.value);
  }
}
