import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { AppConfig } from '../../../../app.config';
import {
  RegisteredUser,
  UnregisteredUser,
  UnregisteredUserModel
} from '../../../../models/user.model';

@Injectable()
export class UserService {
  constructor(private http: HttpClient, private config: AppConfig) {}

  importUsers(file): Observable<any> {
    return this.http
      .post<any>(this.config.apiUrl + '/importfile/import', file)
      .map(data => {
        return data;
      });
  }
  getAllUsers(): Observable<Array<UnregisteredUser | RegisteredUser>> {
    return this.http
      .get<any[]>(this.config.apiUrl + '/importfile/unregisteredUsers')
      .map(data => {
        return data;
      });
  }
  saveUnregisteredUser(user: UnregisteredUserModel) {
    console.log(user);
    return this.http
      .post<any>(this.config.apiUrl + '/importfile/unregisteredUsers', {
        Name: user.name,
        Surname: user.surname,
        Email: user.email,
        Course: user.course,
        TypeOfStudy: user.typeOfStudy,
        DateOfCompletion: user.completionDate
      })
      .map(data => {
        return data;
      });
  }
}
