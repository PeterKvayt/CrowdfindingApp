import { Component, OnInit } from '@angular/core';
import { Routes } from 'src/app/models/immutable/Routes';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {

  public helpRoute = Routes.help;

  constructor() { }

  ngOnInit() {
  }

}
