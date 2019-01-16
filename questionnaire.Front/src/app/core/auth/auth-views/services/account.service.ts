import { Injectable } from '@angular/core';

@Injectable()
export class AccountService {
  private _mail: string;
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
