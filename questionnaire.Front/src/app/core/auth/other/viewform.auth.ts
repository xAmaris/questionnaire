import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';

/**
 * Guard check if hash has valid length.
 *
 * @export
 * @class ViewformGuard
 * @implements {CanActivate}
 * check if in url is appropriate hash length and depend on it return boolean.
 */
@Injectable()
export class ViewformGuard implements CanActivate {
  constructor(private router: Router) {}

  /**
   * Check if hash has valid length.
   *
   * @param {ActivatedRouteSnapshot} route
   * @param {RouterStateSnapshot} state
   * @returns {boolean} If true hash is valid
   * @memberof ViewformGuard
   */
  canActivate(route: ActivatedRouteSnapshot): boolean {
    const preview = route.params['preview'];
    const token = route.params['hash'];
    const id = Number(route.params['id']);
    if (token.length === 32 && !isNaN(id) && preview.length === 1) {
      return true;
    }
    // not logged in so redirect to login page with previous url
    this.router.navigate(['/auth/login']);
    return false;
  }
}
