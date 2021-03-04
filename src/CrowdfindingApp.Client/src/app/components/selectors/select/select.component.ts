import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { LookupItem } from 'src/app/models/common/LookupItem';

@Component({
  selector: 'app-select',
  templateUrl: './select.component.html',
  styleUrls: ['./select.component.css']
})
export class SelectComponent implements OnInit {

  @Input() collection: LookupItem[];
  @Input() defaultValue: string;
  @Output() selectedValue = new EventEmitter<string>();

  public onValueSelect(value: string): void {
      this.selectedValue.emit(value);
  }

  public ngOnInit(): void {
    
  }
}
