import { Component, OnInit } from '@angular/core';
import { PagingControl } from 'src/app/components/paging/PagingControl';
import { ProjectCard } from 'src/app/components/project-card/ProjectCard';
import { ProjectFilterInfo } from 'src/app/models/replies/projects/ProjectFilterInfo';
import { ProjectStatusEnum } from 'src/app/models/enums/ProjectStatus';
import { Base } from '../Base';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectSearchRequestMessage } from 'src/app/models/requests/projects/ProjectSearchRequestMessage';
import { ProjectService } from 'src/app/services/project.service';
import { PagedReplyMessage } from 'src/app/models/replies/common/PagedReplyMessage';
import { Routes } from 'src/app/models/immutable/Routes';
import { PagingInfo } from 'src/app/models/common/PagingInfo';

@Component({
  selector: 'app-moderation-list',
  templateUrl: './moderation-list.component.html',
  styleUrls: ['./moderation-list.component.css']
})
export class ModerationListComponent extends Base implements OnInit {

  constructor(
    public router: Router,
    public activatedRoute: ActivatedRoute,
    private projectService: ProjectService
  ) {
    super(router, activatedRoute);
  }

  public pagingControl: PagingControl<ProjectCard>;

  ngOnInit() {
    this.getProjects();
  }

  onCardEditClick(card: ProjectCard) {
    this.redirect(Routes.projectEdit + '/' + card.id);
  }

  onCardDeleteClick(card: ProjectCard) {
    this.subscriptions.add(
      this.projectService.delete(card.id).subscribe(
        () => { this.pagingControl.collection.remove(card); }
      )
    );
  }

  getProjects() {
    this.showLoader = true;
    const filter: ProjectFilterInfo = { status: [ ProjectStatusEnum.Moderation]};
    const request: ProjectSearchRequestMessage = {
      filter: filter,
      paging: new PagingInfo(1, 6)
     };
    this.subscriptions.add(
      this.projectService.search(request).subscribe(
        (reply: PagedReplyMessage<ProjectCard[]>) => {
          this.pagingControl = {
            paging: reply.paging,
            collection: reply.value,
            filter: filter
          };
        }
      )
    );
    this.showLoader = false;
  }
}
