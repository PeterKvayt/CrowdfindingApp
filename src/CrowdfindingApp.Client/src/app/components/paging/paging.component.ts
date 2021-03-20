import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { ProjectService } from 'src/app/services/project.service';
import { PagingInfo } from 'src/app/models/common/PagingInfo';
import { PagingControl } from './PagingControl';
import { ProjectCard } from '../project-card/ProjectCard';
import { GenericLookupItem } from 'src/app/models/common/GenericLookupItem';
import { ProjectFilterInfo } from 'src/app/models/replies/projects/ProjectFilterInfo';
import { ProjectSearchRequestMessage } from 'src/app/models/requests/projects/ProjectSearchRequestMessage';
import { ProjectStatusEnum } from 'src/app/models/enums/ProjectStatus';
import { PagedReplyMessage } from 'src/app/models/replies/common/PagedReplyMessage';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-paging',
  templateUrl: './paging.component.html',
  styleUrls: ['./paging.component.css']
})
export class PagingComponent implements OnInit, OnDestroy {

  @Input() control: PagingControl<ProjectCard>;
  @Input() loadingOn: boolean;

  constructor(
    private projectService: ProjectService
  ) { }

  public pageCount = 1;
  public pageNumber = 1;
  public pageCollection: GenericLookupItem<number, string>[] = [];
  private subscriptions = new Subscription();
  public currentPage: GenericLookupItem<number, string>;

  public setPageArray() {
    for (let index = 0; index < this.pageCount && index < 3; index++) {
      this.pageCollection.push(new GenericLookupItem<number, string>(index, 'page-button-item'));
    }
    if (this.pageCollection.find(x => x.key === this.pageCount - 1) === undefined) {
      this.pageCollection.push(new GenericLookupItem<number, string>(this.pageCount - 1, 'page-button-item'));
    }
    this.pageCollection[0].value = 'page-button-item active-page-item';
  }
  ngOnInit() {
    this.pageCount = this.getPageCount();
    this.setPageArray();
    this.currentPage = this.pageCollection[0];
    this.pageNumber = this.control ? this.control.paging.pageNumber : 1;
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  isNormalCount(): boolean {
    return this.pageCount <= 12;
  }

  getPageCount(): number {
    if (!this.control) { return 1; }
    if (this.control.paging.pageSize <= 0) { this.control.paging.pageSize = 9; }
    return Math.ceil(this.control.paging.totalCount / this.control.paging.pageSize);
  }

  onPageClick(item: GenericLookupItem<number, string>) {
    if (item === this.currentPage) { return; }
    this.currentPage.value = 'page-button-item';
    item.value = 'page-button-item active-page-item';
    this.currentPage = item;
    this.control.paging.pageNumber = this.currentPage.key + 1;
    this.recalculatePageCollection();
    this.fetchCards();
  }

  recalculatePageCollection() {
    let recalculatedPages: GenericLookupItem<number, string>[] = [this.currentPage];
    let backIndex = this.currentPage.key;
    for (let index = this.currentPage.key; index <= this.currentPage.key + 2; index++) {
      if (index + 1 <= this.pageCount - 1) {
        recalculatedPages.push(new GenericLookupItem<number, string>(index + 1, 'page-button-item'));
      }
      if (--backIndex >= 0) {
        recalculatedPages.unshift(new GenericLookupItem<number, string>(backIndex, 'page-button-item'));
      }
    }
    this.pageCollection = recalculatedPages;

    if (this.pageCollection[0].key !== 0) {
      this.pageCollection.unshift(new GenericLookupItem<number, string>(0, 'page-button-item'));
    }
    if (this.pageCollection[this.pageCollection.length - 1].key !== this.pageCount - 1) {
      this.pageCollection.push(new GenericLookupItem<number, string>(this.pageCount - 1, 'page-button-item'));
    }
  }

  fetchCards() {
    const request: ProjectSearchRequestMessage = {
      filter: this.control.filter,
      paging: new PagingInfo(this.control.paging.pageNumber, this.control.paging.pageSize)
     };
    this.subscriptions.add(
      this.projectService.cards(request).subscribe(
        (reply: PagedReplyMessage<ProjectCard[]>) => {
          this.control.collection = reply.value;
          this.control.paging = reply.paging;
        }
      )
    );
  }
}
