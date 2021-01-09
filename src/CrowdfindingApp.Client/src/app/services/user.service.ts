import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { GetTokenRequestMessage } from '../models/requests/user/GetTokenRequestMessage';
import { RegisterRequestMessage } from '../models/requests/user/RegisterRequestMessage';

@Injectable()
export class UserService {

  constructor(
    private http: HttpService
  ) { }

  private controller = 'Accounts/';

  public forgotPassword(email: string) {
    return this.http.get(this.controller + 'forgot-password/' + email);
  }

  public resetPassword(model: ) {
    return this.http.post<>(this.controller + 'reset-password', model);
  }

  public signUp(model: RegisterRequestMessage) {
    return this.http.post<RegisterRequestMessage>(this.controller + 'register', model);
  }

  public signIn(model: GetTokenRequestMessage) {
    return this.http.post<GetTokenRequestMessage>(this.controller + 'token', model);
  }

  public getUserInfo() {
    return this.http.get(this.controller + 'user-info');
  }

  public updateUserInfo(model: ) {
    return this.http.put<>(this.controller + 'user-info', model);
  }

  public changePassword(model: ) {
    return this.http.put<>(this.controller + 'change-password', model);
  }
}