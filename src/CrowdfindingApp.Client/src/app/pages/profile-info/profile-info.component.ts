import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { TextInput } from 'src/app/components/inputs/text-input/TextInput';
import { UserService } from 'src/app/services/user.service';
import { Title } from '@angular/platform-browser';
import { UpdateUserRequestMessage } from 'src/app/models/requests/user/UpdateUserRequestMessage';
import { ReplyMessage } from 'src/app/models/replies/common/ReplyMessage';
import { UserInfo } from 'src/app/models/replies/user/UserInfo';

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
    public accountService: UserService,
    private titleService: Title
  ) { super(router, activatedRoute); }

  public ngOnInit(): void {
    this.setOnInitUserInfos();
    this.titleService.setTitle('Профиль инфо');
  }

  public onUpdateUserInfoClick(): void {
    // const model: UpdateUserRequestMessage = new UpdateUserRequestMessage(this.emailInput.value, this.nameInput.value);
    // this.subscriptions.add(
    //   this.accountService.updateUserInfo(model).subscribe(
    //     () => { },
    //     error => {
    //       console.log(error); 
    //       // this.handleError(error); 
    //     }
    //   )
    // )
  }

  private setOnInitUserInfos(): void {
    this.subscriptions.add(
      this.accountService.getUserInfo().subscribe(
        (response: ReplyMessage<UserInfo>) => {
          // this.nameInput.value = response.name;
          // this.emailInput.value = response.email;
        }
      )
    )
  }
}
