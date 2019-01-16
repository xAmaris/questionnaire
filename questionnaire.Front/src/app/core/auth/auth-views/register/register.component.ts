import { Component, OnInit } from '@angular/core';
import {
  Validators,
  FormGroup,
  FormBuilder,
  AbstractControl
} from '@angular/forms';
import { namePattern } from 'src/app/patterns/name.pattern';
import { surnamePattern } from 'src/app/patterns/surname.pattern';
import { emailPattern } from 'src/app/patterns/email.pattern';
import { passwordPattern } from 'src/app/patterns/password.pattern';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  regForm: FormGroup;
  // loader
  loading = false;
  // profiles tooltip
  profiles = [
    { value: 'Student', icon: 'pen', message: 'Student' },
    { value: 'Employee', icon: 'briefcase', message: 'Pracownik' }
  ];
  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    this.setForm();
  }
  setForm() {
    // form declaration
    this.regForm = this.fb.group({
      name: [
        '',
        Validators.compose([
          Validators.required
          // Validators.pattern(namePattern)
        ])
      ],
      lastName: [
        '',
        Validators.compose([
          Validators.required
          // Validators.pattern(surnamePattern)
        ])
      ],
      email: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern(emailPattern)
        ])
      ],
      password: [
        '',
        Validators.compose([
          Validators.required
          // Validators.pattern(passwordPattern)
        ])
      ],
      passwordConfirm: [
        '',
        Validators.compose([Validators.required, this.matchPassword])
      ],
      profileName: ['Student', Validators.required],
      albumID: ['', Validators.required],
      phoneNum: ['', Validators.required],
      companyName: ['', Validators.required],
      location: [''],
      companyDescription: ['']
    });
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
  clearPasswordConfirm(): void {
    // clear confirm password input after changing password input
    this.regForm.controls.passwordConfirm.setValue('');
    this.regForm.controls.passwordConfirm.markAsUntouched();
  }
  onSubmit() {}
}
