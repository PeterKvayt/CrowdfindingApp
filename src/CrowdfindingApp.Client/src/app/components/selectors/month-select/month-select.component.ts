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
      new SelectItem('Январь', '1'),
      new SelectItem('Февраль', '2'),
      new SelectItem('Март', '3'),
      new SelectItem('Апрель', '4'),
      new SelectItem('Май', '5'),
      new SelectItem('Июнь', '6'),
      new SelectItem('Июль', '7'),
      new SelectItem('Август', '8'),
      new SelectItem('Сентябрь', '9'),
      new SelectItem('Октябрь', '10'),
      new SelectItem('Ноябрь', '11'),
      new SelectItem('Дкабрь', '12'),
    ],
    defaultValue: 'Выберите месяц'
  };

  @Output() selectedValue = new EventEmitter<string>();

  public onSelect(value: string): void {
    this.selectedValue.emit(value);
}

}
