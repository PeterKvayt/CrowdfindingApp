import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-email-confirmed',
  templateUrl: './email-confirmed.component.html',
  styleUrls: ['./email-confirmed.component.css']
})
export class EmailConfirmedComponent extends Base implements OnInit {
  constructor(public router: Router,
    public activatedRoute: ActivatedRoute,
    private titleService: Title) {
    super(router, activatedRoute);
  }
  public ngOnInit(): void {
    this.titleService.setTitle('Добро пожаловать!');
  }

  public onConfirmClick(): void {

  }
}
