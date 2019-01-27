import { Injectable } from '@angular/core';
import { AbstractControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable()
export class SharedService {
  // button clicked Subjects
  toggleSidebar: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(true);
  // sendButton: Subject<boolean> = new Subject<boolean>();
  showButton: Subject<boolean> = new Subject<boolean>();
  // showing elements Subjects
  showBack: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  showToggle: Subject<boolean> = new Subject<boolean>();
  showSurveyDialog: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
    false
  );
  showCreator: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  showSend: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  showAdminMenu: Subject<boolean> = new Subject<boolean>();
  showPreview: Subject<boolean> = new Subject<boolean>();
  showUserInfo: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  // input error variable
  controlArray: string[];

  constructor(private router: Router) {}

  // button clicked actions

  showSurveyButton(x): void {
    this.showButton.next(x);
  }

  // showing elements
  showBackButton(x): void {
    this.showBack.next(x);
  }

  showToggleButton(x): void {
    this.showToggle.next(x);
  }

  showCreatorButton(x): void {
    this.showCreator.next(x);
  }

  showSendButton(x): void {
    this.showSend.next(x);
  }

  showAdminMain(x): void {
    this.showAdminMenu.next(x);
  }
  showPreviewDiv(x): void {
    this.showPreview.next(x);
  }
  showSendSurveyDialog(x): void {
    this.showSurveyDialog.next(x);
  }

  showUser(x): void {
    this.showUserInfo.next(x);
  }

  routeSwitch(role): void {
    switch (role) {
      case 'student':
        this.router.navigateByUrl('/app/student');
        break;
      case 'employer':
        this.router.navigateByUrl('/app/employer');
        break;
      case 'graduate':
        this.router.navigateByUrl('/app/graduate');
        break;
      case 'careerOffice':
        this.router.navigateByUrl('/app/admin/d/survey/(s:a//m:a)');
        break;
    }
  }

  inputError(control: AbstractControl) {
    // retrieve controls names into array to show errors for user
    if (control.touched === true || control.dirty === true) {
      const parent = control['_parent'];
      if (
        parent instanceof FormGroup &&
        control.errors !== null &&
        control.touched
      ) {
        let controlName: string;

        const controls = parent.controls;
        if (!this.controlArray) {
          this.controlArray = Object.keys(controls);
        }
        const length = this.controlArray.length;

        for (let i = 0; i < length; i++) {
          if (control === controls[this.controlArray[i]]) {
            controlName = this.controlArray[i];
            break;
          }
        }
        const translatedControlName = this.controlNameAdjustSwitch(controlName);
        return this.setErrorString(control, controlName, translatedControlName);
      }
    }
  }
  controlNameAdjustSwitch(controlName) {
    switch (controlName) {
      case 'name':
        controlName = 'imię';
        break;
      case 'surname':
      case 'lastName':
        controlName = 'nazwisko';
        break;
      case 'phoneNum':
        controlName = 'numer telefonu';
        break;
      case 'companyName':
        controlName = 'nazwa firmy';
        break;
      case 'password':
        controlName = 'hasło';
        break;
      case 'oldPassword':
        controlName = 'stare hasło';
        break;
      case 'newPassword':
        controlName = 'nowe hasło';
        break;
      case 'email':
        controlName = 'email';
        break;
      case 'course':
        controlName = 'kurs';
        break;
      case 'typeOfStudy':
        controlName = 'typ studiów';
        break;
      case 'dateOfCompletion':
        controlName = 'data zakończenia';
        break;
    }
    return controlName;
  }
  setErrorString(
    control: AbstractControl,
    controlName: string,
    translatedControlName: string
  ) {
    let errorObj: {
      controlName: string;
      errorStr: string;
    };
    let errorStr: string;
    if (control.value !== undefined && control.value.length === 0) {
      errorStr = 'Wpisz ' + translatedControlName;
    } else {
      if (controlName === 'password' || controlName === 'newPassword') {
        errorStr =
          // tslint:disable-next-line:max-line-length
          'Użyj co najmniej ośmiu znaków, w tym jednocześnie liter, cyfr i symboli: !#$%&?';
      } else {
        errorStr = translatedControlName;
      }
    }
    errorObj = {
      errorStr,
      controlName
    };
    return errorObj;
  }
  deleteControlArray() {
    this.controlArray = undefined;
  }

  public toggleSideNav(x) {
    this.toggleSidebar.next(x);
  }
}
