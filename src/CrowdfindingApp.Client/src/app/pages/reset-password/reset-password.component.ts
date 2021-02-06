import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { PasswordInput } from 'src/app/components/inputs/password-input/PasswordInut';
import { UserService } from 'src/app/services/user.service';
import { Title } from '@angular/platform-browser';
import { ResetPasswordRequestMessage } from 'src/app/models/requests/user/ResetPasswordRequestMessage';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent extends Base implements OnInit  {

  public newPasswordInput: PasswordInput = { label: 'Новый пароль' };
  public confirmPasswordInput: PasswordInput = { label: 'Подтвердите пароль' };
  private token: string;
  constructor(
    public router: Router,
    public activatedRoute: ActivatedRoute,
    private accountService: UserService,
    private titleService: Title
    ) {
      super(router, activatedRoute);
  }

  public ngOnInit(): void {
    this.titleService.setTitle('Восстановление доступа');
    this.setToken();
  }

  public onResetPasswordClick(): void {
    const model: ResetPasswordRequestMessage = {
      password: this.newPasswordInput.value,
      confirmPassword: this.confirmPasswordInput.value,
      token: this.token
    };
    this.subscriptions.add(
      this.accountService.resetPassword(model).subscribe(
        () => { this.redirect('sign-in'); }
      )
    )
  }

  private setToken(): void {
    this.subscriptions.add(
      this.activatedRoute.queryParams.subscribe(
        params => { this.token = params.token; }
      )
    )
  }
}
