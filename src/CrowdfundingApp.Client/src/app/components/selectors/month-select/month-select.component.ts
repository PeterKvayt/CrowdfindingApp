import { Component, EventEmitter, Output } from '@angular/core';
import { SelectItem } from '../select/SelectItem';
import { SelectInput } from '../select/SelectInput';

@Component({
  selector: 'app-month-select',
  templateUrl: './month-select.component.html',
  styleUrls: ['./month-select.component.css']
})
export class MonthSelectComponent {

  public select: SelectInput = {
    list: [
      new SelectItem('1', 'Январь'),
      new SelectItem('2', 'Февраль'),
      new SelectItem('3', 'Март'),
      new SelectItem('4', 'Апрель'),
      new SelectItem('5', 'Май'),
      new SelectItem('6', 'Июнь'),
      new SelectItem('7', 'Июль'),
      new SelectItem('8', 'Август'),
      new SelectItem('9', 'Сентябрь'),
      new SelectItem('10', 'Октябрь'),
      new SelectItem('11', 'Ноябрь'),
      new SelectItem('12', 'Декабрь'),
    ],
    defaultValue: 'Выберите месяц'
  };

  @Output() selectedValue = new EventEmitter<string>();

  public onSelect(value: string): void {
    this.select.currentValue = this.select.list.find(x => x.value === value);
    this.selectedValue.emit(value);
}

}
