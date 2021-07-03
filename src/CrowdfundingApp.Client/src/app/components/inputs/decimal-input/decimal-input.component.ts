import { Component, Input, Output, EventEmitter, OnInit, ElementRef, ViewChild } from '@angular/core';
import { DecimalInput } from './DecimalInput';

@Component({
  selector: 'app-decimal-input',
  templateUrl: './decimal-input.component.html',
  styleUrls: ['./decimal-input.component.css']
})
export class DecimalInputComponent implements OnInit {

  @Input() item: DecimalInput;
  @Output() valueChange = new EventEmitter<DecimalInput>();
  @ViewChild('decimalInput', {static: true}) input: ElementRef;

  public onValueChange(val: string): void {
    const result = this.checkValue(val);
    this.input.nativeElement.value = result;
    this.item.value = <number><any>(result);
    this.valueChange.emit(this.item);
  }

  public ngOnInit(): void {
    this.item.valid = this.item.valid === undefined ? true : this.item.valid;
    this.item.min = this.item.min === undefined ? 0 : this.item.min;
    this.item.max = this.item.max === undefined ? Number.MAX_SAFE_INTEGER : this.item.max;
    this.item.placeholder = this.item.placeholder === undefined ? this.item.label : this.item.placeholder;
  }

  private checkValue(value: string): string {
    let result = value.replace(new RegExp('[^0-9.,]'), '').replace(',', '.');
    if (result.includes('.')) {
      if (result.startsWith('.')) {
        result = this.item.min.toString() + result;
      }
      const resultValues = result.split('.');
      if (resultValues.length >= 2) {
        result = resultValues[0] + '.' + resultValues[1].substr(0, 2);
      } else {
        result = resultValues[0] + '.';
      }
    }
    if (result) {
      return result;
    }
    return '';
  }
}
