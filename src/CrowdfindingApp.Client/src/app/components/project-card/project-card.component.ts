import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ProjectCard } from './ProjectCard';
import { ProjectStatusEnum } from 'src/app/models/enums/ProjectStatus';
import { FileService } from 'src/app/services/file.service';


@Component({
  selector: 'app-project-card',
  templateUrl: './project-card.component.html',
  styleUrls: ['./project-card.component.css']
})
export class ProjectCardComponent implements OnInit {

  constructor(
    private fileService: FileService
  ) { }
  
  @Input() editable: boolean;
  @Input() card: ProjectCard;
  @Input() colLg: number;
  @Input() colMd: number;
  @Input() colSm: number;

  @Output() deleteClick = new EventEmitter();
  @Output() editClick = new EventEmitter();

  public columnClass: string;
  public imageUrl = 'assets/img/stock-project.png';

  public ngOnInit(): void {
    this.setImagePath();
    const lg = this.colLg ? this.colLg : 12;
    const md = this.colMd ? this.colMd : 12;
    const sm = this.colSm ? this.colSm : 12;
    this.columnClass = 'col-lg-' + lg + ' col-md-' + md + ' col-sm-' + sm;
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

  public getProgress(): number {
    if (!this.card.purpose || this.card.purpose === 0 || this.card.currentResult < 0) {
      return 0;
    } else {
      return (this.card.currentResult / this.card.purpose) * 100;
    }
  }

  public getStatus(): string {
    switch (this.card.status) {
      case ProjectStatusEnum.Active: return 'ИДЕТ СБОР';
      case ProjectStatusEnum.Complited: return 'УСПЕХ';
      case ProjectStatusEnum.Draft: return 'ЧЕРНОВИК';
      case ProjectStatusEnum.Moderation: return 'МОДЕРАЦИЯ';
      case ProjectStatusEnum.Stopped: return 'ПРИОСТАНОВЛЕН';
      default:
        break;
    }
  }

  public onDeleteClick() {
    this.deleteClick.emit();
  }

  public onEditClick() {
    this.editClick.emit();
  }
}
