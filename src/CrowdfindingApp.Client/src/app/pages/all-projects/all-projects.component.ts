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
import { CategoryItem } from 'src/app/components/category-item/CategoryItem';
import { ProjectStatusEnum } from 'src/app/models/enums/ProjectStatus';
import { GetFuncEnum } from 'src/app/models/enums/GetFuncEnum';

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
    private titleService: Title
  ) { super(router, activatedRoute); }
  public getFuncType = GetFuncEnum.opened;
  public pagination: PagingControl<ProjectCard> = {
    paging: new PagingInfo(1, 9),
    filter: new ProjectFilterInfo()
  };
  public categories: CategoryItem[] = [];
  public filters: CategoryItem[] = [
    new CategoryItem(ProjectStatusEnum.Complited, 'Успешные', false),
    new CategoryItem(ProjectStatusEnum.Active, 'Активные', false),
    new CategoryItem(ProjectStatusEnum.Finalized, 'Завершенные', false),
  ];
  ngOnInit() {
    this.titleService.setTitle('Все проекты');
    this.setCategories();
    this.setProjects(new ProjectFilterInfo());
  }

  setCategories() {
    this.showLoader = true;
    this.subscriptions.add(
      this.projectService.getCategories().subscribe(
        (reply: ReplyMessage<LookupItem[]>) => {
          reply.value.forEach(x => this.categories.push(new CategoryItem(x.key, x.value, false)));
          this.showLoader = false;
        },
        () => { this.showLoader = false; }
      )
    );
  }

  setProjects(filter: ProjectFilterInfo) {
    this.showLoader = true;
    const request: ProjectSearchRequestMessage = {
      paging: this.pagination.paging,
      filter: filter
    };
    this.subscriptions.add(
      this.projectService.openedProjects(request).subscribe(
        (reply: PagedReplyMessage<ProjectCard[]>) => {
          this.pagination.collection = reply.value;
          this.pagination.paging = reply.paging;
          this.showLoader = false;
        },
        () => { this.showLoader = false; }
      )
    );
  }

  onSearchClick() {
    const filter: ProjectFilterInfo = {
      categoryId: this.categories.filter(x => x.active).map(x => x.id),
      status: this.filters.filter(x => x.active).map(x => <ProjectStatusEnum>x.id)
    };
    this.setProjects(filter);
  }
}
