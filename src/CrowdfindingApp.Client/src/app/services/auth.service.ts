

import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Settings } from './settings.service';
@Injectable()
export class AuthenticationService {
  constructor(public jwtHelper: JwtHelperService) { }
  public isAuthenticated(): boolean {
    const token = sessionStorage.getItem(Settings.accessToken);
    if (token !== null) {
      return !this.jwtHelper.isTokenExpired(token);
    } else {
      return false;
    }
  }
  public signOut(): void {
    sessionStorage.removeItem(Settings.accessToken);
  }
  public getToken(): string {
    return sessionStorage.getItem(Settings.accessToken);
  }
  public setToken(token: string): void {
    sessionStorage.setItem(Settings.accessToken, token);
  }
}
