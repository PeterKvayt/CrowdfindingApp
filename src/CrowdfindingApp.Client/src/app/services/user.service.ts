import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { GetTokenRequestMessage } from '../models/requests/users/GetTokenRequestMessage';
import { ResetPasswordRequestMessage } from '../models/requests/users/ResetPasswordRequestMessage';
import { RegisterRequestMessage } from '../models/requests/users/RegisterRequestMessage';
import { UpdateUserRequestMessage } from '../models/requests/users/UpdateUserRequestMessage';
import { ChangePasswordRequestMessage } from '../models/requests/users/ChangePasswordRequestMessage';

@Injectable()
export class UserService {

  constructor(
    private http: HttpService
  ) { }

  private controller = 'Users/';

  public forgotPassword(email: string) {
    return this.http.get(this.controller + 'forgot-password/' + email);
  }

  public resetPassword(model: ResetPasswordRequestMessage) {
    return this.http.post<ResetPasswordRequestMessage>(this.controller + 'reset-password', model);
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

  public getById(id: string) {
    return this.http.get(this.controller + id);
  }

  public updateUserInfo(model: UpdateUserRequestMessage) {
    return this.http.put<UpdateUserRequestMessage>(this.controller + 'user-info', model);
  }

  public changePassword(model: ChangePasswordRequestMessage) {
    return this.http.put<ChangePasswordRequestMessage>(this.controller + 'change-password', model);
  }
}