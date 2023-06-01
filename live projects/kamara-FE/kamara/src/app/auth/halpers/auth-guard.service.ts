import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, CanActivateChild, Router, RouterStateSnapshot,} from '@angular/router';
import {RouteConfig} from '@shared/config/route-config';
import {AuthService} from '@auth/service/auth.service';

@Injectable({providedIn: 'root'})
export class AuthGuard implements CanActivate, CanActivateChild {
  routeConfig: RouteConfig;

  constructor(
    private router: Router,
    private authService: AuthService
  ) {
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    return this.validateData(route, state);
  }

  canActivateChild(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    return this.validateData(route, state);
  }

  private validateData(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    const currentUser = this.authService.User;
    if (currentUser) {
      // logged in so return true
      return true;
    }

    // not logged in so redirect to login page with the return url
    this.router.navigate([RouteConfig.auth_login], {
      queryParams: {returnUrl: state.url},
    });
    return false;
  }
}
