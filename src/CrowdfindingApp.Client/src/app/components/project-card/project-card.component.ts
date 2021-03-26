import { Component, OnInit, Input, Output, EventEmitter, ViewChild, ElementRef } from '@angular/core';
import { ProjectCard } from './ProjectCard';
import { ProjectStatusEnum } from 'src/app/models/enums/ProjectStatus';
import { FileService } from 'src/app/services/file.service';
import { AuthenticationService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { Routes } from 'src/app/models/immutable/Routes';


@Component({
  selector: 'app-project-card',
  templateUrl: './project-card.component.html',
  styleUrls: ['./project-card.component.css']
})
export class ProjectCardComponent implements OnInit {

  constructor(
    private fileService: FileService,
    public router: Router,
    private authService: AuthenticationService
  ) { }
  @Input() editable: boolean;
  @Input() card: ProjectCard;

  @Output() deleteClick = new EventEmitter();
  @Output() editClick = new EventEmitter();

  public columnClass: string;
  public imageUrl = 'assets/img/stock-project.png';
  private emitterTriggered = false;

  public ngOnInit(): void {
    this.setImagePath();
  }

  public setImagePath() {
    if (this.card.imgPath) {
      if (this.card.imgPath === this.imageUrl) {
        return;
      }
      this.imageUrl = this.fileService.absoluteFileStoragePath + this.card.imgPath;
    }
  }

  public getWidth(): string {
    let result = 0;
    if (this.card.currentResult > 0) {
      result = (this.card.currentResult / this.card.purpose) * 100;
    }
    return result + '%';
  }

  public getProgress(): string {
    if (!this.card.purpose || this.card.purpose === 0 || this.card.currentResult < 0) {
      return '0';
    } else {
      const result = (this.card.currentResult / this.card.purpose * 100).toString().split('.');
      return result[0] + (result[1] ? '.' + result[1].substr(0, 3) : '');
    }
  }

  public getStatus(): string {
    switch (this.card.status) {
      case ProjectStatusEnum.Active: return 'ИДЕТ СБОР';
      case ProjectStatusEnum.Complited: return 'УСПЕШНЫЙ';
      case ProjectStatusEnum.Draft: return 'ЧЕРНОВИК';
      case ProjectStatusEnum.Moderation: return 'МОДЕРАЦИЯ';
      case ProjectStatusEnum.Stopped: return 'ПРИОСТАНОВЛЕН';
      case ProjectStatusEnum.Finalized: return this.card.currentResult >= this.card.purpose ? 'УСПЕШНЫЙ' : 'ПРОВАЛ';
      default:
        break;
    }
  }

  getHoverTitle() {
    switch (this.card.status) {
      case ProjectStatusEnum.Active: return 'ПОДДЕРЖАТЬ';
      case ProjectStatusEnum.Complited: return 'ПРОСМОТРЕТЬ';
      case ProjectStatusEnum.Draft: return 'ПРОСМОТРЕТЬ';
      case ProjectStatusEnum.Moderation: return 'ПРОСМОТРЕТЬ';
      case ProjectStatusEnum.Stopped: return 'ПРОСМОТРЕТЬ';
      default:
        break;
    }
  }

  public onDeleteClick() {
    this.emitterTriggered = true;
    this.deleteClick.emit();
  }

  public onEditClick() {
    this.emitterTriggered = true;
    this.editClick.emit();
  }

  onCardClick() {
    if (this.card.id && !this.emitterTriggered) {
      this.router.navigate([Routes.project + '/' + this.card.id]);
    }
  }
}
