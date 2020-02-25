import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { SharedService } from '../../../shared/services/shared.service';
import { AccountService } from '../services/account.service';

@Injectable()
export class RoleGuard implements CanActivate {
  constructor(
    private accountService: AccountService,
    private sharedService: SharedService,
    private router: Router
  ) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const expectedRole: string = route.data.expectedRole;
    const currentUser = JSON.parse(localStorage.getItem('currentUser'));
    if (!currentUser) {
      return false;
    }
    const role: string = currentUser.role;
    if (role === expectedRole) {
      this.accountService.setRoleSubject(expectedRole);
      return true;
    } else {
      if (this.accountService.isLogged.value === true) {
        this.sharedService.routeSwitch(role);
      } else {
        this.router.navigateByUrl('auth/login');
      }
      return false;
    }
  }
}
