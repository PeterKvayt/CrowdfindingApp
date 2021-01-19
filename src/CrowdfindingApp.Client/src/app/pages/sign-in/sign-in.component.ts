import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Base } from '../Base';
import { TextInput } from 'src/app/components/inputs/text-input/TextInput';
import { PasswordInput } from 'src/app/components/inputs/password-input/PasswordInut';
import { AuthenticationService } from 'src/app/services/auth.service';
import { Title } from '@angular/platform-browser';
import { GetTokenRequestMessage } from 'src/app/models/requests/user/GetTokenRequestMessage';
import { TokenInfo } from 'src/app/models/replies/user/TokenInfo';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent extends Base implements OnInit {
  
  public emailInput: TextInput = { label: 'Email', placeholder: 'example@mail.by' };
  public passwordInput: PasswordInput = { label: 'Пароль', placeholder: '$uperP4$$vv0rD'};

  public succesSignIn = true;

  constructor(
    private accountService: UserService,
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
    const user: GetTokenRequestMessage = {
      email: this.emailInput.value,
      password: this.passwordInput.value
    };
    this.subscriptions.add(
      this.accountService.signIn(user).subscribe(
        (response: TokenInfo) => {
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
