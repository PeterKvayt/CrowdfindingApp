import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { SelectInput } from './SelectInput';

@Component({
  selector: 'app-select',
  templateUrl: './select.component.html',
  styleUrls: ['./select.component.css']
})
export class SelectComponent implements OnInit {

  @Input() item: SelectInput;
  @Output() selectedValue = new EventEmitter<string>();

  public onValueSelect(value: string): void {
    this.item.currentValue = this.item.list.find(x => x.value === value);
    this.selectedValue.emit(value);
  }

  public ngOnInit(): void {
   }
}
