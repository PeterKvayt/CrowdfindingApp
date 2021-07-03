import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router, CanActivate, ActivatedRouteSnapshot } from '@angular/router';
import { AuthenticationService } from './auth.service';

@Injectable()
export class AuthenticationGuardService implements CanActivate {

  constructor(public jwtHelper: JwtHelperService,
    private router: Router,
    private authService: AuthenticationService) { }

  public canActivate(snapshot: ActivatedRouteSnapshot): boolean {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['']);
      return false;
    }
    if (!snapshot.data || !snapshot.data.role) {
      return true;
    }
    if (this.authService.isInRole(snapshot.data.role)) {
      return true;
    }
    this.router.navigate(['']);
    return false;
  }
}
