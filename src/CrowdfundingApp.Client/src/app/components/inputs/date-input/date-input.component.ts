import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DateInput } from './DateInput';

@Component({
  selector: 'app-date-input',
  templateUrl: './date-input.component.html',
  styleUrls: ['./date-input.component.css']
})
export class DateInputComponent implements OnInit {
  @Input() item: DateInput;

  @Output() valueChange = new EventEmitter<DateInput>();

  public min: string;
  public max: string;
  public currentValue: string;

  public onValueChange(event): void {
      this.item.value = new Date(event.originalTarget.value);
      this.valueChange.emit(this.item);
  }

  public ngOnInit(): void {
    this.item.valid = this.item.valid === undefined ? true : this.item.valid;
    this.currentValue = this.item.value === null || this.item.value === undefined
      ? undefined
      : this.item.value.toString().substr(0, 10);
    this.min = this.item.min === undefined ? '1900-01-01' : this.item.min.toISOString().substr(0, 10);
    this.max = this.item.max === undefined ? undefined : this.item.max.toISOString().substr(0, 10);
  }

}
