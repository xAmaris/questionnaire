import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { AppConfig } from '../../app.config';
import { User } from '../other/user.model';
import { AccountService } from './account.service';

/**
 * Service for authentication - log in and log out.
 *
 * @export
 * @class AuthenticationService
 */
@Injectable()
export class AuthenticationService {
  profileData: BehaviorSubject<User> = new BehaviorSubject<
  User
>(undefined);
  constructor(
    private http: HttpClient,
    private config: AppConfig,
    private userService: AccountService
  ) { }

  /**
   * Sign in user - make request using values email and password
   *
   * @param {string} Email Email input value
   * @param {string} Password Password input value
   * @returns Result from request to api
   * @memberof AuthenticationService
   */
  login(Email: string, Password: string) {
    return this.http
      .post<any>(this.config.apiUrl + '/auth/login', { Email, Password })
      .map(user => {
        // login successful if there's a jwt token in the response
        if (user && user.loginData.token) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.userService.isLoggedNext(true);
        }
        return user;
      });
  }

  /**
   * Remove user from local storage to log user out
   *
   * @memberof AuthenticationService
   */
  logout() {
    // remove user from local storage to log user out
    this.userService.isLoggedNext(false);
    localStorage.removeItem('currentUser');
  }

  activateAccount(token: string) {
    const string = '/auth/activation/' + token;
    return this.http.get(this.config.apiUrl + string);
  }
  setProfileData() {
    // return this.http
    // .get<User>(this.config.apiUrl + '/surveytemplate/' + id)
    // .map(data => {
    //   return data;
    // });
  }
}
