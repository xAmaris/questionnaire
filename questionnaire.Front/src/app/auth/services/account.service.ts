import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';
import { AppConfig } from '../../app.config';
import { UserProfile } from '../other/user.model';

@Injectable()
export class AccountService {
  mail: string;
  isLogged: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(undefined);
  role: BehaviorSubject<string> = new BehaviorSubject<string>(undefined);
  profileData: BehaviorSubject<UserProfile> = new BehaviorSubject<UserProfile>(
    undefined
  );
  constructor(private http: HttpClient, private config: AppConfig) {}

  isLoggedNext(x) {
    this.isLogged.next(x);
  }
  setRoleSubject(x) {
    this.role.next(x);
  }

  setProfileData() {
    const user = JSON.parse(localStorage.getItem('currentUser'));
    const _profileData = {
      firstName: user.name,
      lastName: user.surname,
      email: user.email,
      phoneNum: user.phoneNumber
    };
    this.profileData.next(_profileData);
  }
  // create new user

  createStudent(user) {
    return this.http.post(this.config.apiUrl + '/auth/students', {
      IndexNumber: user.albumID,
      Email: user.email,
      Password: user.password,
      Name: user.firstName,
      Surname: user.lastName,
      PhoneNumber: user.phoneNum
    });
  }
  createGraduate(user) {
    return this.http.post(this.config.apiUrl + '/auth/graduates', {
      Email: user.email,
      Password: user.password,
      Name: user.firstName,
      Surname: user.lastName,
      PhoneNumber: user.phoneNum
    });
  }
  createEmployer(user) {
    return this.http.post(this.config.apiUrl + '/auth/employers', {
      IndexNumber: user.albumID,
      Email: user.email,
      Password: user.password,
      Name: user.firstName,
      Surname: user.lastName,
      PhoneNumber: user.phoneNum,
      CompanyName: user.companyName,
      Location: user.location,
      CompanyDescription: user.companyDescription
    });
  }

  createNewAdmin(user: UserProfile): Observable<any> {
    return this.http
      .post<any>(this.config.apiUrl + '/auth/careerOffices', {
        Name: user.firstName,
        Surname: user.lastName,
        Email: user.email,
        PhoneNumber: user.phoneNum
      })
      .map(data => {
        return data;
      });
  }
  // change password
  changePassword(OldPassword, NewPassword) {
    return this.http.post(this.config.apiUrl + '/auth/changePassword', {
      OldPassword,
      NewPassword
    });
  }

  // restore password
  sendRestorePasswordEmail(Email) {
    return this.http.post(this.config.apiUrl + '/auth/restorePassword', {
      Email
    });
  }

  changePasswordByRestoringPassword(Token, NewPassword) {
    return this.http.post(
      this.config.apiUrl + '/auth/changePasswordByRestoringPassword',
      {
        Token,
        NewPassword
      }
    );
  }

  // update user data

  updateProfile(user) {
    return this.http.put(this.config.apiUrl + '/accountupdate/accounts', {
      Name: user.firstName,
      Surname: user.lastName,
      Email: user.email,
      PhoneNumber: user.phoneNum,
      CompanyName: user.companyName,
      Location: user.location,
      CompanyDescription: user.companyDescription
    });
  }

  // recovery password
  passMailData(mail) {
    this.mail = mail;
  }
  getMailData() {
    const mail = this.mail;
    this.mail = undefined;
    return mail;
  }
  setLoginErrorString(status: number): string {
    switch (status) {
      case 0:
      case 500:
        return 'Błąd połączenia!';
      case 400:
        return 'Nieprawidłowy mail lub hasło';
    }
  }
}
