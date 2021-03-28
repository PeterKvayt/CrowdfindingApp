import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { ProjectService } from 'src/app/services/project.service';
import { Title } from '@angular/platform-browser';
import { PagedReplyMessage } from 'src/app/models/replies/common/PagedReplyMessage';
import { ProjectCard } from 'src/app/components/project-card/ProjectCard';
import { ProjectStatusEnum } from 'src/app/models/enums/ProjectStatus';
import { ProjectFilterInfo } from 'src/app/models/replies/projects/ProjectFilterInfo';
import { ProjectSearchRequestMessage } from 'src/app/models/requests/projects/ProjectSearchRequestMessage';
import { PagingInfo } from 'src/app/models/common/PagingInfo';
import { FileService } from 'src/app/services/file.service';
import { Routes } from 'src/app/models/immutable/Routes';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent extends Base implements OnInit {

  constructor(
    private projectService: ProjectService,
    public router: Router,
    public activatedRoute: ActivatedRoute,
    private titleService: Title,
    public fileService: FileService
    ) {
      super(router, activatedRoute);
  }
  
  // public imgHost = fileService.
  public projects: ProjectCard[] = [];

  public ngOnInit(): void {
    this.titleService.setTitle('Главная');
    this.setProjects();
  }

  private setProjects(): void {
    this.showLoader = true;
    this.subscriptions.add(
      this.projectService.getTopProjectCards(new PagingInfo(1, 3)).subscribe(
        (reply: PagedReplyMessage<ProjectCard[]>) => {
          this.projects = reply.value;
          this.showLoader = false;
        },
        () => {this.showLoader = false; }
      )
    );
  }

  onSlideClick(card: ProjectCard): void {
    this.redirect(Routes.project + '/' + card.id);
  }
}
