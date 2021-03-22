import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { ProjectService } from 'src/app/services/project.service';
import { ReplyMessage } from 'src/app/models/replies/common/ReplyMessage';
import { FileService } from 'src/app/services/file.service';
import { ProjectInfoView } from 'src/app/models/replies/projects/ProjectInfoView';
import { Routes } from 'src/app/models/immutable/Routes';
import { UserInfo } from 'src/app/models/replies/users/UserInfo';
import { UserService } from 'src/app/services/user.service';
import { RewardInfo } from 'src/app/models/replies/rewards/RewardInfo';
import { RewardCard } from 'src/app/components/reward-card/RewardCard';

@Component({
  selector: 'app-project-page',
  templateUrl: './project-page.component.html',
  styleUrls: ['./project-page.component.css']
})
export class ProjectPageComponent extends Base implements OnInit {

  constructor(
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public projectService: ProjectService,
    public userService: UserService,
    public fileService: FileService
  ) {
    super(router, activatedRoute);
  }

  public profileRoute = Routes.profile;
  public view: ProjectInfoView;
  public user: UserInfo;

  ngOnInit () {
    const projectId = this.activatedRoute.snapshot.paramMap.get('projectId');
    if (projectId) { this.setProjectInfo(projectId); }
    this.setUserInfo();
  }

  setUserInfo(): void {
    this.showLoader = true;
    this.subscriptions.add(
      this.userService.getUserInfo().subscribe(
        (reply: ReplyMessage<UserInfo>) => {
          this.user = reply.value;
        }
      )
    );
    this.showLoader = false;
  }

  setProjectInfo(projectId: string) {
    this.showLoader = true;
    this.subscriptions.add(
      this.projectService.getViewById(projectId).subscribe(
        (reply: ReplyMessage<ProjectInfoView>) => {
          this.view = reply.value;
        }
      )
    );
    this.showLoader = false;
  }

  getNickName(): string {
    if (this.user.name && this.user.middleName) {
      return this.user.name + ' ' + this.user.middleName;
    } else {
      return this.user.email;
    }
  }

  getRewardCardFromInfo(reward: RewardInfo): RewardCard {
    return new RewardCard(
      reward.title,
      reward.price,
      reward.description,
      reward.image,
      reward.deliveryType,
      reward.deliveryDate,
      reward.limit
    );
  }
}
