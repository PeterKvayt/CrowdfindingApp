

import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Settings } from './settings.service';
import { Roles } from '../models/immutable/Roles';
@Injectable()
export class AuthenticationService {
  constructor(public jwtHelper: JwtHelperService) { }
  public isAuthenticated(): boolean {
    const token = this.getToken();
    if (token) {
      return !this.jwtHelper.isTokenExpired(token);
    }

    return false;

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
  public isInRole(role: string): boolean {
    const token = this.getToken();
    if (!this.isAuthenticated()) { return false; }
    const decodedToken = this.jwtHelper.decodeToken(token);
    if (decodedToken.roles) {
      return decodedToken.roles === role;
    } else {
      return false;
    }
  }
  public isAdmin(): boolean {
    return this.isInRole(Roles.admin);
  }
  public getMyId(): string {
    const token = this.getToken();
    return this.jwtHelper.decodeToken(token).user_id;
  }
}
