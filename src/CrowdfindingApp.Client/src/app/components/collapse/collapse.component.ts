import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-collapse',
  templateUrl: './collapse.component.html',
  styleUrls: ['./collapse.component.css']
})
export class CollapseComponent implements OnInit {

  @Input() header: string;
  @Input() body: string;

  public show = false;

  constructor() { }

  ngOnInit() {
  }

}
