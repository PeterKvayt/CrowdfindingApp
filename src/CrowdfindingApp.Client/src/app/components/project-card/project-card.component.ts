import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ProjectCard } from './ProjectCard';


@Component({
  selector: 'app-project-card',
  templateUrl: './project-card.component.html',
  styleUrls: ['./project-card.component.css']
})
export class ProjectCardComponent implements OnInit {

  constructor() { }
  
  @Input() editable: boolean;
  @Input() card: ProjectCard;
  @Input() colLg: number;
  @Input() colMd: number;
  @Input() colSm: number;

  @Output() deleteEvent = new EventEmitter<string>();
  @Output() editEvent = new EventEmitter<string>();

  public columnClass: string;

  public ngOnInit(): void {
    const lg = this.colLg ? this.colLg : 12;
    const md = this.colMd ? this.colMd : 12;
    const sm = this.colSm ? this.colSm : 12;
    this.columnClass = 'col-lg-' + lg + 'col-md-' + md + 'col-sm-' + sm;
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

  public getProgressStatus(){
    if (this.getProgress() < this.card.purpose || this.card.purpose === 0) {
      return 'ИДЕТ СБОР';
    } else {
      return 'УСПЕХ';
    }
  }

  public onDeleteClick() {
    this.deleteEvent.emit(this.card.id);
  }

  public onEditClick() {
    this.editEvent.emit(this.card.id);
  }
}
