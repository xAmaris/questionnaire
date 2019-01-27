import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';
import { SharedService } from '../../../../../../../services/shared.service';

@Component({
  selector: 'app-add-user-tab',
  templateUrl: './add-user-tab.component.html',
  styleUrls: ['./add-user-tab.component.scss']
})
export class AddUserTabComponent implements OnInit {
  dialogForm: FormGroup;
  @Input()
  loader: boolean;
  @Output()
  submit: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
  name: AbstractControl;
  surname: AbstractControl;
  email: AbstractControl;
  course: AbstractControl;
  typeOfStudy: AbstractControl;
  dateOfCompletion: AbstractControl;

  // error strs
  nameErrorStr: string;
  surnameErrorStr: string;
  emailErrorStr: string;
  courseErrorStr: string;
  typeOfStudyErrorStr: string;
  dateOfCompletionErrorStr: string;

  // tslint:disable-next-line:max-line-length
  emailPattern = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  namePattern = /^([a-zA-ZąęćłóśźżĄĘĆŁÓŚŹŻ\\']){0,}$/;
  surnamePattern = /^([a-zA-ZąęćłóśźżĄĘĆŁÓŚŹŻ]+[\s\-\\'])*[a-zA-ZąęćłóśźżĄĘĆŁÓŚŹŻ]+$/;
  constructor(private fb: FormBuilder, private sharedService: SharedService) {}

  ngOnInit() {
    this.setForm();
  }
  setForm() {
    this.dialogForm = this.fb.group({
      name: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern(this.namePattern)
        ])
      ],
      surname: [
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
      course: [
        '',
        Validators.compose([Validators.required, Validators.minLength(3)])
      ],
      typeOfStudy: [
        '',
        Validators.compose([Validators.required, Validators.minLength(3)])
      ],
      dateOfCompletion: ['', Validators.compose([Validators.required])]
    });
    this.name = this.dialogForm.controls['name'];
    this.surname = this.dialogForm.controls['surname'];
    this.email = this.dialogForm.controls['email'];
    this.course = this.dialogForm.controls['course'];
    this.typeOfStudy = this.dialogForm.controls['typeOfStudy'];
    this.dateOfCompletion = this.dialogForm.controls['dateOfCompletion'];
  }
  onSubmit(dialog) {
    this.submit.emit(dialog);
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
        case 'surname':
          this.surnameErrorStr = errorObj.errorStr;
          break;
        case 'email':
          this.emailErrorStr = errorObj.errorStr;
          break;
        case 'course':
          this.courseErrorStr = errorObj.errorStr;
          break;
        case 'typeOfStudy':
          this.typeOfStudyErrorStr = errorObj.errorStr;
          break;
        case 'dateOfCompletion':
          this.dateOfCompletionErrorStr = errorObj.errorStr;
          break;
      }
      return true;
    }
  }
}
