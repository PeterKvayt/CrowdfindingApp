import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Base } from '../Base';
import { UserService } from 'src/app/services/user.service';
import { Title } from '@angular/platform-browser';
import { ReplyMessage } from 'src/app/models/replies/common/ReplyMessage';
import { UserInfo } from 'src/app/models/replies/users/UserInfo';
import { AuthenticationService } from 'src/app/services/auth.service';
import { TabElement } from 'src/app/components/tab/Tabelement';

@Component({
  selector: 'app-profile',

  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent extends Base implements OnInit {
  
  public myProjectsTab = new TabElement('Мои проекты', true);
  public supportedTab = new TabElement('Поддрежал', false);
  public draftsTab = new TabElement('Черновики', false);
  
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

  public onTabClick(tab: TabElement): void {
    if (tab.isActive) { return; }
    this.myProjectsTab.isActive = false;
    this.supportedTab.isActive = false;
    this.draftsTab.isActive = false;
    tab.isActive = true;
  }
}
