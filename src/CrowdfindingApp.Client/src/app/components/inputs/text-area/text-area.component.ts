import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { TextArea } from './TextArea';

@Component({
  selector: 'app-text-area',
  templateUrl: './text-area.component.html',
  styleUrls: ['./text-area.component.css']
})
export class TextAreaComponent implements OnInit {

  @Input() item: TextArea;

  @Output() valueChange = new EventEmitter<TextArea>();

  public onValueChange(value: string): void {
      this.item.value = value;
      this.valueChange.emit(this.item);
  }

  public ngOnInit(): void {
    this.item.valid = this.item.valid === undefined ? true : this.item.valid;
    this.item.min = this.item.min === undefined ? 0 : this.item.min;
    this.item.max = this.item.max === undefined ? 100099999999 : this.item.max;
    this.item.placeholder = this.item.placeholder === undefined ? this.item.label : this.item.placeholder;
  }

}
