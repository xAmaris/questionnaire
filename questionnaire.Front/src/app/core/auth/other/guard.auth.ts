import { Injectable } from '@angular/core';
import { CanLoad, Router } from '@angular/router';
import { AccountService } from '../services/account.service';

@Injectable()
export class AuthGuard implements CanLoad {
  constructor(private router: Router, private accountService: AccountService) {}

  canLoad() {
    if (localStorage.getItem('currentUser')) {
      // logged in so return true
      this.accountService.isLoggedNext(true);
      return true;
    }

    // not logged in so redirect to login page with previous url
    this.accountService.isLoggedNext(false);
    this.router.navigateByUrl('/auth/login');
    return false;
  }
}
