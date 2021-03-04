import { Component, EventEmitter, Output } from '@angular/core';
import { LookupItem } from 'src/app/models/common/LookupItem';

@Component({
  selector: 'app-month-select',
  templateUrl: './month-select.component.html',
  styleUrls: ['./month-select.component.css']
})
export class MonthSelectComponent {

  public monthList: LookupItem[] =
  [
    new LookupItem('Январь', '1'),
    new LookupItem('Февраль', '2'),
    new LookupItem('Март', '3'),
    new LookupItem('Апрель', '4'),
    new LookupItem('Май', '5'),
    new LookupItem('Июнь', '6'),
    new LookupItem('Июль', '7'),
    new LookupItem('Август', '8'),
    new LookupItem('Сентябрь', '9'),
    new LookupItem('Октябрь', '10'),
    new LookupItem('Ноябрь', '11'),
    new LookupItem('Дкабрь', '12'),
  ];

  @Output() selectedValue = new EventEmitter<string>();

  public onSelect(value: string): void {
    this.selectedValue.emit(value);
}

}
