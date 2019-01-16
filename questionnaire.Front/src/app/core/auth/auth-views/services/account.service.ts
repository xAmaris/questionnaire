import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { UserProfile } from 'src/app/models/user.model';

@Injectable()
export class AccountService {
  private _mail: string;

  isLogged: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(undefined);
  role: BehaviorSubject<string> = new BehaviorSubject<string>(undefined);
  profileData: BehaviorSubject<UserProfile> = new BehaviorSubject<UserProfile>(
    undefined
  );

  // recovery password
  passMailData(mail: string) {
    this._mail = mail;
  }
  getMailData() {
    const mail = this._mail;
    this._mail = undefined;
    return mail;
  }
}
