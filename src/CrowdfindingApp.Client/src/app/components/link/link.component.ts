import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-link',
  templateUrl: './link.component.html',
  styleUrls: ['./link.component.css']
})
export class LinkComponent implements OnInit {

  constructor() { }

  @Input() route: string;
  @Input() href: string;
  @Input() value: string;
  @Input() empty: string;

  ngOnInit() {
    this.route = this.route === undefined ? null : '/' + this.route;
  }

}
