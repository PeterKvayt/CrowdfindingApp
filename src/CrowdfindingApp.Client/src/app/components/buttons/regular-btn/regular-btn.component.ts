import { Component, Input, Output, EventEmitter, OnInit, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-regular-btn',
  templateUrl: './regular-btn.component.html',
  styleUrls: ['./regular-btn.component.css']
})
export class RegularBtnComponent implements OnInit {

  @Input() link: string;
  @Input() value: string;
  @Input() fluid: boolean;
  @Input() ico: string;
  @Input() icoBefore: boolean;

  public class: string;
  
  public ngOnInit(): void {
    this.link = this.link === undefined ? null : this.link;
    this.value = this.value === undefined ? 'emplty value' : this.value;
    this.class = this.fluid ? 'fluid-regular-btn' : 'regular-btn';
  }
}
