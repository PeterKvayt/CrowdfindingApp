import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-tab',
  templateUrl: './tab.component.html',
  styleUrls: ['./tab.component.css']
})
export class TabComponent implements OnInit {

  @Input() value: string;
  @Input() ico: string;
  @Input() icoBefore: boolean;
  @Input() active: boolean;
  
  public class: string;
  
  constructor() { }
  ngOnInit() {
    this.value = this.value === undefined ? 'emplty value' : this.value;
  }

  public getClass(): string {
    if (this.active) {
      return 'active';
    } else {
      return '';
    }
  }
}
