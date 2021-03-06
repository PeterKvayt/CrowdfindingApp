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

  public onValueChange(value: string): void {
      this.item.value = value;
      this.valueChange.emit(this.item);
  }

  public ngOnInit(): void {
    this.item.valid = this.item.valid === undefined ? true : this.item.valid;
    this.min = this.item.min === undefined ? '1900-01-01' : this.item.min.toISOString().substr(0, 10);
    this.max = this.item.max === undefined ? null : this.item.max.toISOString().substr(0, 10);
  }

}
