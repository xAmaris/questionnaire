import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  // button clicked Subjects
  toggleSidebar: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(true);
  showButton: Subject<boolean> = new Subject<boolean>();
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

  constructor() {}

  // button clicked actions

  showSurveyButton(x: any): void {
    this.showButton.next(x);
  }

  // showing elements
  showBackButton(x: any): void {
    this.showBack.next(x);
  }

  showToggleButton(x: any): void {
    this.showToggle.next(x);
  }

  showCreatorButton(x: any): void {
    this.showCreator.next(x);
  }

  showSendButton(x: any): void {
    this.showSend.next(x);
  }

  showAdminMain(x: any): void {
    this.showAdminMenu.next(x);
  }
  showPreviewDiv(x: any): void {
    this.showPreview.next(x);
  }
  showSendSurveyDialog(x: any): void {
    this.showSurveyDialog.next(x);
  }

  showUser(x: any): void {
    this.showUserInfo.next(x);
  }

  public toggleSideNav() {
    this.toggleSidebar.next(undefined);
  }
}
