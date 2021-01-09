import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { SignInModel } from 'src/app/models/SignInModel';
import { ISignInResponse } from 'src/app/interfaces/ISignInResponse';
import { Router, ActivatedRoute } from '@angular/router';
import { Base } from '../Base';
import { TextInput } from 'src/app/components/inputs/text-input/TextInput';
import { PasswordInput } from 'src/app/components/inputs/password-input/PasswordInut';
import { ErrorModel } from 'src/app/models/ErrorModel';
import { AuthenticationService } from 'src/app/services/auth.service';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent extends Base implements OnInit {
  
  public emailInput: TextInput = { label: 'Email' };
  public passwordInput: PasswordInput = { label: 'Пароль'};

  public succesSignIn = true;

  constructor(
    private accountService: AccountService,
    public router: Router,
    public activatedRoute: ActivatedRoute,
    private authService: AuthenticationService,
    private titleService: Title
  ) { super(router, activatedRoute); }

  public ngOnInit(): void {
    if (this.authService.isAuthenticated()) {
      this.redirect('profile');
    }
    this.titleService.setTitle('Вход');
  }

  public onSignInClick(): void {
    const user: SignInModel = {
      email: this.emailInput.value,
      password: this.passwordInput.value
    };
    this.subscriptions.add(
      this.accountService.signIn(user).subscribe(
        (response: ISignInResponse) => {
          this.authService.setToken(response.token);
          this.redirect('profile');
        },
        (error) => { 
          console.log(error);
          // if (error.statusCode !== 400) {
          //   this.handleError(error);
          // } else {
          //   this.succesSignIn = false;
          // }
        }
      )
    );
  }
}
