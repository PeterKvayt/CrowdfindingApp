import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Base } from '../Base';
import { UserService } from 'src/app/services/user.service';
import { Title } from '@angular/platform-browser';
import { ReplyMessage } from 'src/app/models/replies/ReplyMessage';
import { UserInfo } from 'src/app/models/replies/user/UserInfo';
import { AuthenticationService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-profile',

  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent extends Base implements OnInit {
  constructor(
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public userService: UserService,
    private titleService: Title,
    private authService: AuthenticationService
  ) {
    super(router, activatedRoute);
  }

  public userInfo: UserInfo;

  public ngOnInit(): void {
    this.titleService.setTitle('Профиль');
    this.setUserInfo();
  }

  private setUserInfo(): void {
    this.subscriptions.add(
      this.userService.getUserInfo().subscribe(
        (reply: ReplyMessage<UserInfo>) => {
          this.userInfo = reply.value;
        }
      )
    );
  }

  public onSignOutClick(): void {
    this.authService.signOut();
    this.redirect('sign-in');
  }
}
