import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { TextInput } from 'src/app/components/inputs/text-input/TextInput';
import { UserViewModel } from 'src/app/view-models/account/UserViewModel';
import { AccountService } from 'src/app/services/account.service';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-profile-info',
  templateUrl: './profile-info.component.html',
  styleUrls: ['./profile-info.component.css']
})
export class ProfileInfoComponent extends Base implements OnInit {
  public emailInput: TextInput = { label: 'Email' };
  public nameInput: TextInput = { label: 'Имя' };
  constructor(
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public accountService: AccountService,
    private titleService: Title
  ) { super(router, activatedRoute); }

  public ngOnInit(): void {
    this.setOnInitUserInfos();
    this.titleService.setTitle('Профиль инфо');
  }

  public onUpdateUserInfoClick(): void {
    const model: UserViewModel = new UserViewModel(this.emailInput.value, this.nameInput.value);
    this.subscriptions.add(
      this.accountService.updateUserInfo(model).subscribe(
        () => { },
        error => {
          console.log(error); 
          // this.handleError(error); 
        }
      )
    )
  }

  private setOnInitUserInfos(): void {
    this.subscriptions.add(
      this.accountService.getUserInfo().subscribe(
        (response: UserViewModel) => {
          this.nameInput.value = response.name;
          this.emailInput.value = response.email;
        },
        error => { this.handleError(error); }
      )
    )
  }
}
