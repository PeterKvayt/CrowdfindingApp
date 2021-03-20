import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Base } from '../Base';
import { UserService } from 'src/app/services/user.service';
import { Title } from '@angular/platform-browser';
import { ReplyMessage } from 'src/app/models/replies/common/ReplyMessage';
import { UserInfo } from 'src/app/models/replies/users/UserInfo';
import { AuthenticationService } from 'src/app/services/auth.service';
import { TabElement } from 'src/app/components/tab/Tabelement';
import { ProjectCard } from 'src/app/components/project-card/ProjectCard';
import { ProjectService } from 'src/app/services/project.service';
import { ProjectSearchRequestMessage } from 'src/app/models/requests/projects/ProjectSearchRequestMessage';
import { ProjectFilterInfo } from 'src/app/models/replies/projects/ProjectFilterInfo';
import { ProjectStatusEnum } from 'src/app/models/enums/ProjectStatus';
import { Routes } from 'src/app/models/immutable/Routes';
import { PagingInfo } from 'src/app/models/common/PagingInfo';
import { PagedReplyMessage } from 'src/app/models/replies/common/PagedReplyMessage';
import { PagingControl } from 'src/app/components/paging/PagingControl';

@Component({
  selector: 'app-profile',

  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent extends Base implements OnInit {
  public myProjectsTab = new TabElement('Мои проекты', true);
  public supportedTab = new TabElement('Поддрежал', false);
  public draftsTab = new TabElement('Черновики', false);
  public myProjectsPaging: PagingControl<ProjectCard>;
  public supportedPaging: PagingControl<ProjectCard>;
  public draftsPaging: PagingControl<ProjectCard>;
  constructor(
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public userService: UserService,
    private titleService: Title,
    private authService: AuthenticationService,
    private projectService: ProjectService
  ) {
    super(router, activatedRoute);
  }

  public userInfo: UserInfo;

  public ngOnInit(): void {
    this.titleService.setTitle('Профиль');
    this.setUserInfo();
    this.fetchMyProjects();
    this.fetchSupportedProjects();
    this.fetchDraftProjects();
  }

  private setUserInfo(): void {
    this.showLoader = true;
    this.subscriptions.add(
      this.userService.getUserInfo().subscribe(
        (reply: ReplyMessage<UserInfo>) => {
          this.userInfo = reply.value;
          this.showLoader = false;
        },
        () => { this.showLoader = false; }
      )
    );
  }

  public onSignOutClick(): void {
    this.authService.signOut();
    this.redirect(Routes.signIn);
  }

  public onTabClick(tab: TabElement): void {
    if (tab.isActive) { return; }
    this.myProjectsTab.isActive = false;
    this.supportedTab.isActive = false;
    this.draftsTab.isActive = false;
    tab.isActive = true;
  }

  fetchDraftProjects() {
    const filter: ProjectFilterInfo = { status: [ ProjectStatusEnum.Draft, ProjectStatusEnum.Moderation]};
    const request: ProjectSearchRequestMessage = {
      filter: filter,
      paging: new PagingInfo(1, 6)
     };
    this.subscriptions.add(
      this.projectService.cards(request).subscribe(
        (reply: PagedReplyMessage<ProjectCard[]>) => {
          this.draftsPaging = {
            paging: reply.paging,
            collection: reply.value,
            filter: filter
          };
        }
      )
    );
  }

  fetchSupportedProjects() {
    const filter: ProjectFilterInfo = { status: [ ProjectStatusEnum.Draft, ProjectStatusEnum.Moderation]};
    // const request: ProjectSearchRequestMessage = {
    //   filter: filter,
    //   paging: new PagingInfo(1, 6)
    //  };
    // this.subscriptions.add(
    //   this.projectService.cards(request).subscribe(
    //     (reply: PagedReplyMessage<ProjectCard[]>) => {
    //       this.supportedPaging = {
    //         paging: reply.paging,
    //         collection: reply.value,
    //         filter: filter
    //       };
    //     }
    //   )
    // );
  }

  fetchMyProjects() {
    const filter: ProjectFilterInfo = { status: [ ProjectStatusEnum.Draft, ProjectStatusEnum.Moderation]};
    // const request: ProjectSearchRequestMessage = {
    //   filter: filter,
    //   paging: new PagingInfo(1, 6)
    //  };
    // this.subscriptions.add(
    //   this.projectService.cards(request).subscribe(
    //     (reply: PagedReplyMessage<ProjectCard[]>) => {
    //       this.myProjectsPaging = {
    //         paging: reply.paging,
    //         collection: reply.value,
    //         filter: filter
    //       };
    //     }
    //   )
    // );
  }

  onCardEditClick(card: ProjectCard) {
    console.log(card);
    this.redirect(Routes.projectEdit + '/' + card.id);
  }

  onCardDeleteClick(card: ProjectCard, collection: ProjectCard[]) {
    this.subscriptions.add(
      this.projectService.delete(card.id).subscribe(
        () => { collection.remove(card); }
      )
    );
  }

  isEditable(card: ProjectCard): boolean {
    return card.status === ProjectStatusEnum.Draft;
  }
}
