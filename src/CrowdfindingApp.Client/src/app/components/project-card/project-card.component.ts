import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ProjectCard } from './ProjectCard';
import { ProjectStatusEnum } from 'src/app/models/enums/ProjectStatus';
import { FileService } from 'src/app/services/file.service';
import { AuthenticationService } from 'src/app/services/auth.service';


@Component({
  selector: 'app-project-card',
  templateUrl: './project-card.component.html',
  styleUrls: ['./project-card.component.css']
})
export class ProjectCardComponent implements OnInit {

  constructor(
    private fileService: FileService,
    private authService: AuthenticationService
  ) { }
  
  @Input() editable: boolean;
  @Input() card: ProjectCard;

  @Output() deleteClick = new EventEmitter();
  @Output() editClick = new EventEmitter();

  public columnClass: string;
  public imageUrl = 'assets/img/stock-project.png';

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
