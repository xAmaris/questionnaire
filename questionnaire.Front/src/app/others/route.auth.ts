import { Injectable } from '@angular/core';
import { CanActivate, NavigationEnd, Router } from '@angular/router';

@Injectable()
export class RouteGuard implements CanActivate {
  bool = false;
  constructor(private router: Router) {
    this.router.events.subscribe(val => {
      if (val instanceof NavigationEnd) {
        if (val.url !== val.urlAfterRedirects) {
          this.bool = true;
        }
      }
    });
  }

  canActivate() {
    if (this.bool === true) {
      return true;
    }
    this.router.navigateByUrl('app/admin/create');
    return false;
  }
}
