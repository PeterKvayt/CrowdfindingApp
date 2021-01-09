import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { TextInput } from './TextInput';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css']
})
export class TextInputComponent implements OnInit {

  @Input() item: TextInput;

  @Output() valueChange = new EventEmitter<TextInput>();

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
