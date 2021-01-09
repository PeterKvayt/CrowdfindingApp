import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { PasswordInput } from 'src/app/components/inputs/password-input/PasswordInut';
import { ChangePasswordViewModel } from 'src/app/view-models/account/ChangePasswordViewModel';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-profile-security',
  templateUrl: './profile-security.component.html',
  styleUrls: ['./profile-security.component.css']
})
export class ProfileSecurityComponent extends Base implements OnInit {
  public currentPasswordInput: PasswordInput = { label: 'Текущий пароль' };
  public newPasswordInput: PasswordInput = { label: 'Новый пароль' };
  public confirmNewPasswordInput: PasswordInput = { label: 'Подтвердите пароль' };
  constructor(
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public accountService: AccountService,
    private titleService: Title
  ) { super(router, activatedRoute); }

  public ngOnInit(): void {
    this.titleService.setTitle('Безопасность');
  }

  public onPasswordChangeClick(): void {
    const model: ChangePasswordViewModel = {
      currentPassword: this.currentPasswordInput.value,
      newPassword: this.newPasswordInput.value,
      confirmNewPassword: this.confirmNewPasswordInput.value
    };

      this.subscriptions.add(
        this.accountService.changePassword(model).subscribe(
          () => {},
          error => { this.handleError(error); }
        )
      )
  }
}
