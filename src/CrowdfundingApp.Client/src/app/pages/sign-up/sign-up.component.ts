import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Base } from '../Base';
import { TextInput } from 'src/app/components/inputs/text-input/TextInput';
import { PasswordInput } from 'src/app/components/inputs/password-input/PasswordInut';
import { Title } from '@angular/platform-browser';
import { RegisterRequestMessage } from 'src/app/models/requests/users/RegisterRequestMessage';
import { Routes } from 'src/app/models/immutable/Routes';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent extends Base implements OnInit {
  public signInRoute = Routes.signIn;
  public emailInput: TextInput = { label: 'Email', placeholder: 'email' };
  public passwordInput: PasswordInput = { label: 'Пароль', placeholder: 'пароль' };
  public confirmPasswordInput: PasswordInput = { label: 'Подтвердите пароль', placeholder: 'пароль' };

  constructor(
    private accountService: UserService,
    public router: Router,
    public activatedRoute: ActivatedRoute,
    private titleService: Title
  ) {
    super(router, activatedRoute);
  }

  public ngOnInit(): void {
    this.titleService.setTitle('Регистрация');
  }

  public onSignUpClick(): void {
    this.showLoader = true;
    const request = new RegisterRequestMessage(this.emailInput.value, this.passwordInput.value, this.confirmPasswordInput.value);
    this.subscriptions.add(
      this.accountService.signUp(request)
        .subscribe(
          x => { this.redirect(Routes.signIn); },
         () => { this.showLoader = false; }
        )
    );
  }
}
