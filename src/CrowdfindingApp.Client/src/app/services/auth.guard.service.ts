import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router, CanActivate } from '@angular/router';
import { AuthenticationService } from './auth.service';

@Injectable()
export class AuthenticationGuardService implements CanActivate {

  constructor(public jwtHelper: JwtHelperService,
    private router: Router,
    private authService: AuthenticationService) { }

  public canActivate(): boolean {
    if (this.authService.isAuthenticated()) {
      return true;
    } else {
      this.router.navigate(['']);
      return false;
    }
  }
}
