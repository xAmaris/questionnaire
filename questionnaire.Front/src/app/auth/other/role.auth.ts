import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { SharedService } from '../../services/shared.service';
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
    const currentRole: string = JSON.parse(localStorage.getItem('currentUser'))
      .role;
    if (currentRole === expectedRole) {
      this.accountService.setRoleSubject(expectedRole);
      return true;
    } else {
      if (this.accountService.isLogged.value === true) {
        console.log(currentRole);
        this.sharedService.routeSwitch(currentRole);
      } else {
        this.router.navigateByUrl('auth/login');
      }
      return false;
    }
  }
}
