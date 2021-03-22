import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { ProjectService } from 'src/app/services/project.service';
import { Title } from '@angular/platform-browser';
import { PagingControl } from 'src/app/components/paging/PagingControl';
import { ProjectCard } from 'src/app/components/project-card/ProjectCard';
import { ProjectSearchRequestMessage } from 'src/app/models/requests/projects/ProjectSearchRequestMessage';
import { PagingInfo } from 'src/app/models/common/PagingInfo';
import { ProjectFilterInfo } from 'src/app/models/replies/projects/ProjectFilterInfo';
import { PagedReplyMessage } from 'src/app/models/replies/common/PagedReplyMessage';
import { LookupItem } from 'src/app/models/common/LookupItem';
import { ReplyMessage } from 'src/app/models/replies/common/ReplyMessage';

@Component({
  selector: 'app-all-projects',
  templateUrl: './all-projects.component.html',
  styleUrls: ['./all-projects.component.css']
})
export class AllProjectsComponent extends Base implements OnInit {

  constructor(
    public router: Router,
    private projectService: ProjectService,
    public activatedRoute: ActivatedRoute,
    // public fileService: FileService,
    // public authService: AuthenticationService,
    private titleService: Title
  ) { super(router, activatedRoute); }

  public pagination: PagingControl<ProjectCard> = {
    paging: new PagingInfo(1, 9),
    filter: new ProjectFilterInfo()
  };
  public categories: LookupItem[] = [];
  ngOnInit() {
    this.titleService.setTitle('Все проекты');
  }

  setCategories() {
    this.showLoader = true;
    this.subscriptions.add(
      this.projectService.getCategories().subscribe(
        (reply: ReplyMessage<LookupItem[]>) => {
          this.categories = reply.value;
          this.showLoader = false;
        },
        () => { this.showLoader = false; }
      )
    );
  }
  
  setProjects() {
    this.showLoader = true;
    const request: ProjectSearchRequestMessage = {

    }
    this.subscriptions.add(
      this.projectService.openedProjects(request).subscribe(
        (reply: PagedReplyMessage<ProjectCard[]>) => {
          this.pagination.collection = reply.value;
          this.pagination.paging = reply.paging;
          this.showLoader = false;
        },
        () => { this.showLoader = false;}
      )
    );
  }
}
