import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ProjectCardViewModel } from '../../view-models/ProjectCardViewModel';

@Component({
  selector: 'app-project-card',
  templateUrl: './project-card.component.html',
  styleUrls: ['./project-card.component.css']
})
export class ProjectCardComponent implements OnInit {

  constructor() { }
  
  @Input() editable: boolean;
  @Input() card: ProjectCardViewModel;

  @Output() deleteEvent = new EventEmitter<string>();
  @Output() editEvent = new EventEmitter<string>();

  public ngOnInit(): void {
  }

  public getWidth(): string {
    let result = 0;
    if (this.card.currentResult > 0) {
      result = (this.card.currentResult / this.card.purpose) * 100;
    }
    return result + '%';
  }

  public onDeleteClick() {
    this.deleteEvent.emit(this.card.id);
  }

  public onEditClick() {
    this.editEvent.emit(this.card.id);
  }
}
