import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';
import { AppConfig } from '../../../../app.config';
import {
  RegisteredUser,
  UnregisteredUser,
  UnregisteredUserModel
} from '../../../../models/user.model';
import { UserProfile } from './../../../../auth/other/user.model';

@Injectable()
export class UserService {
  savedUnregisteredUsers: BehaviorSubject<
    UnregisteredUser[]
  > = new BehaviorSubject<UnregisteredUser[]>(undefined);
  constructor(private http: HttpClient, private config: AppConfig) {}

  saveUsersFromApi(): void {
    this.getAllUsers().subscribe(data => {
      this.savedUnregisteredUsers.next(data);
    });
  }
  importUsers(file): Observable<any> {
    return this.http
      .post<any>(this.config.apiUrl + '/importfile/import', file)
      .map(data => {
        return data;
      });
  }
  getAllUsers(): Observable<UnregisteredUser[]> {
    return this.http
      .get<any[]>(this.config.apiUrl + '/importfile/unregisteredUsers')
      .map(data => {
        return data;
      });
  }
  saveUnregisteredUser(user: UnregisteredUserModel): Observable<any> {
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

  deleteUserById(id: number): Observable<any> {
    return this.http
      .delete<any>(this.config.apiUrl + '/importfile/unregisteredUsers/' + id)
      .map(data => {
        return data;
      });
  }
  updateUserById(id: number, user: UnregisteredUserModel): Observable<any> {
    return this.http
      .put<any>(this.config.apiUrl + '/importfile/unregisteredUsers/' + id, {
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
