import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { PasswordInput } from 'src/app/components/inputs/password-input/PasswordInut';
import { Title } from '@angular/platform-browser';
import { ChangePasswordRequestMessage } from 'src/app/models/requests/user/ChangePasswordRequestMessage';

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
    public accountService: UserService,
    private titleService: Title
  ) { super(router, activatedRoute); }

  public ngOnInit(): void {
    this.titleService.setTitle('Безопасность');
  }

  public onPasswordChangeClick(): void {
    const model: ChangePasswordRequestMessage = {
      oldPassword: this.currentPasswordInput.value,
      newPassword: this.newPasswordInput.value,
      confirmPassword: this.confirmNewPasswordInput.value
    };

      this.subscriptions.add(
        this.accountService.changePassword(model).subscribe()
      );
  }
}
