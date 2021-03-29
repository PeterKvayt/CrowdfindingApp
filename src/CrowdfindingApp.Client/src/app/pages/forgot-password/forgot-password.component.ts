import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { TextInput } from 'src/app/components/inputs/text-input/TextInput';
import { UserService } from 'src/app/services/user.service';
import { Title } from '@angular/platform-browser';
import { Routes } from 'src/app/models/immutable/Routes';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent extends Base implements OnInit {

  public emailInput: TextInput = { label: 'Email' };
  public showMessage = false;

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
  }

  public onPasswordRecoveryClick(): void {
    this.showLoader = true;
    this.subscriptions.add(
      this.accountService.forgotPassword(this.emailInput.value).subscribe(
        () => {
          this.showLoader = false;
          this.showMessage = true;
          // this.redirect(Routes.signIn);
        },
        () => { this.showLoader = false; }
      )
    )
  }
}
