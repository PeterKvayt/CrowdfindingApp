import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { SelectInput } from '../select/SelectInput';
import { SelectItem } from '../select/SelectItem';

@Component({
  selector: 'app-year-selector',
  templateUrl: './year-selector.component.html',
  styleUrls: ['./year-selector.component.css']
})
export class YearSelectorComponent implements OnInit {

  constructor() { }

  public select: SelectInput = {
    list: [],
    defaultValue: 'Выберите год'
  };

  @Input() yearsCount: number;
  @Output() selectedValue = new EventEmitter<string>();

  ngOnInit() {
    const currentYear = (new Date()).getFullYear();
    for (let index = 0; index < this.yearsCount; index++) {
      const item = currentYear + index;
      this.select.list.push(new SelectItem(item.toString(), item.toString()));
    }
  }

  public onSelect(value: string): void {
    this.selectedValue.emit(value);
}
}
