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
import { Roles } from 'src/app/models/immutable/Roles';
import { GenericLookupItem } from 'src/app/models/common/GenericLookupItem';
import { FileService } from 'src/app/services/file.service';
import { OrderInfo } from 'src/app/models/replies/orders/OrderInfo';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-profile',

  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent extends Base implements OnInit {
  public isMyProfile = false;
  public currentUserId: string;
  public myProjectsTab = new TabElement('Мои проекты', true);
  public supportedTab = new TabElement('Поддержал', false);
  public draftsTab = new TabElement('Черновики', false);
  public ordersTab = new TabElement('Заказы', false);
  public myProjectsPaging: PagingControl<ProjectCard>;
  public supportedPaging: PagingControl<ProjectCard>;
  public draftsPaging: PagingControl<ProjectCard>;
  public orders: OrderInfo[] = []
  constructor(
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public userService: UserService,
    private titleService: Title,
    public authService: AuthenticationService,
    private projectService: ProjectService,
    public fileService: FileService,
    private orderService: OrderService
  ) {
    super(router, activatedRoute);
  }

  public projectRoute = Routes.project + '/';
  public userInfo: UserInfo;
  private toAdmin = 'Сделать администратором';
  private toDefaultUser = 'Сделать пользователем';
  public roleEditTitle = new GenericLookupItem<string, string>();

  public ngOnInit(): void {
    this.titleService.setTitle('Профиль');
    this.currentUserId = this.activatedRoute.snapshot.paramMap.get('userId');
    this.setIsMyProfile();
    this.setUserInfo();
    if (this.isMyProfile) {
      this.fetchMyProjects();
      this.fetchSupportedProjects();
      this.fetchDraftProjects();
      this.setOrders();
    } else {
      this.fetchPublicProjects();
    }
  }

  setOrders(): void {
    this.showLoader = true;
    this.subscriptions.add(
      this.orderService.getOrders().subscribe(
        (reply: ReplyMessage<OrderInfo[]>) => {
          this.orders = reply.value;
          this.showLoader = false;
        },
        () => { this.showLoader = false; }
      )
    );
  }

  setIsMyProfile() {
    if (this.currentUserId) {
      this.isMyProfile = this.currentUserId === this.authService.getMyId();
    } else {
      this.isMyProfile = true;
    }
  }

  private setUserInfo(): void {
    this.showLoader = true;
    const userId = this.isMyProfile ? this.authService.getMyId() : this.currentUserId;
    this.subscriptions.add(
      this.userService.getById(userId).subscribe(
        (reply: ReplyMessage<UserInfo>) => {
          this.userInfo = reply.value;
          this.editRoleTitle(this.userInfo.role);
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
    this.ordersTab.isActive = false;
    tab.isActive = true;
  }

  fetchDraftProjects() {
    const filter: ProjectFilterInfo = { status: [ProjectStatusEnum.Draft, ProjectStatusEnum.Moderation] };
    const request: ProjectSearchRequestMessage = {
      filter: filter,
      paging: new PagingInfo(1, 6)
    };
    this.subscriptions.add(
      this.projectService.ownerProjects(request).subscribe(
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
    this.subscriptions.add(
      this.projectService.getMySupportedProjects(new PagingInfo(1, 6)).subscribe(
        (reply: PagedReplyMessage<ProjectCard[]>) => {
          this.supportedPaging = {
            paging: reply.paging,
            collection: reply.value
          };
        }
      )
    );
  }

  fetchMyProjects() {
    const filter: ProjectFilterInfo = { status: [ProjectStatusEnum.Active] };
    const request: ProjectSearchRequestMessage = {
      filter: filter,
      paging: new PagingInfo(1, 6)
    };
    this.subscriptions.add(
      this.projectService.ownerProjects(request).subscribe(
        (reply: PagedReplyMessage<ProjectCard[]>) => {
          this.myProjectsPaging = {
            paging: reply.paging,
            collection: reply.value,
            filter: filter
          };
        }
      )
    );
  }

  fetchPublicProjects() {
    const filter: ProjectFilterInfo = { ownerId: [this.currentUserId] };
    const request: ProjectSearchRequestMessage = {
      filter: filter,
      paging: new PagingInfo(1, 6)
    };
    this.subscriptions.add(
      this.projectService.openedProjects(request).subscribe(
        (reply: PagedReplyMessage<ProjectCard[]>) => {
          this.myProjectsPaging = {
            paging: reply.paging,
            collection: reply.value,
            filter: filter
          };
        }
      )
    );
  }

  onCardEditClick(card: ProjectCard) {
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

  onToModerationClick() {
    this.redirect(Routes.moderationList);
  }

  editRoleTitle(roleName: string) {
    if (roleName === Roles.defaultUser) {
      this.roleEditTitle.key = Roles.admin;
      this.roleEditTitle.value = this.toAdmin;
    } else {
      this.roleEditTitle.key = Roles.defaultUser;
      this.roleEditTitle.value = this.toDefaultUser;
    }
  }

  onRoleEditClick() {
    this.subscriptions.add(
      this.userService.editRole(this.currentUserId, this.roleEditTitle.key).subscribe(
        () => {
          this.editRoleTitle(this.roleEditTitle.key);
        }
      )
    );
  }

  onEditSettingsClick() {
    this.redirect(Routes.profileSettings);
  }

  getProfileName(): string {
    if (this.userInfo) {
      return this.userInfo.name ? this.userInfo.name + ' ' + this.userInfo.surname : this.userInfo.email;
    }
    return '';
  }
}
