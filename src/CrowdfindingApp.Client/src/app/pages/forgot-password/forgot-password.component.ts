import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { TextInput } from 'src/app/components/inputs/text-input/TextInput';
import { AccountService } from 'src/app/services/account.service';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent extends Base implements OnInit {

  public emailInput: TextInput = { label: 'Email' };

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
  }

  public onPasswordRecoveryClick(): void {
    this.subscriptions.add(
      this.accountService.forgotPassword(this.emailInput.value).subscribe(
        () => { this.redirect('sign-in'); },
        error => { this.handleError(error); }
      )
    )
  }
}