import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { PasswordInput } from 'src/app/components/inputs/password-input/PasswordInut';
import { AccountService } from 'src/app/services/account.service';
import { ResetPasswordViewModel } from 'src/app/view-models/account/ResetPasswordViewModel';
import { Title } from '@angular/platform-browser';

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
    private accountService: AccountService,
    private titleService: Title
    ) {
      super(router, activatedRoute);
  }

  public ngOnInit(): void {
    this.titleService.setTitle('Восстановление доступа');
    this.setToken();
  }

  public onResetPasswordClick(): void {
    const model: ResetPasswordViewModel = {
      newPassword: this.newPasswordInput.value,
      confirmNewPassword: this.confirmPasswordInput.value,
      token: this.token
    };
    this.subscriptions.add(
      this.accountService.resetPassword(model).subscribe(
        () => { this.redirect('sign-in'); },
        error => { this.handleError(error); }
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
