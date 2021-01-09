import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { SignUpModel } from 'src/app/models/SignUpModel';
import { Router, ActivatedRoute } from '@angular/router';
import { Base } from '../Base';
import { TextInput } from 'src/app/components/inputs/text-input/TextInput';
import { PasswordInput } from 'src/app/components/inputs/password-input/PasswordInut';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent extends Base implements OnInit {

  public emailInput: TextInput = { label: 'Email' };
  public passwordInput: PasswordInput = { label: 'Пароль' };
  public confirmPasswordInput: PasswordInput = { label: 'Подтвердите пароль' };

  constructor(
    private accountService: AccountService,
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

    var user: SignUpModel = new SignUpModel(this.emailInput.value, this.passwordInput.value, this.confirmPasswordInput.value);
    
    this.subscriptions.add(
      this.accountService.signUp(user)
        .subscribe(
          () => { this.redirect('sign-in'); },
          error => { this.handleError(error); }
        )
    );
  }
}
