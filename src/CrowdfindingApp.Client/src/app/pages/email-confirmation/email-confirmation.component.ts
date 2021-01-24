import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-email-confirmation',
  templateUrl: './email-confirmation.component.html',
  styleUrls: ['./email-confirmation.component.css']
})
export class EmailConfirmationComponent extends Base implements OnInit {
  constructor(public router: Router,
    public activatedRoute: ActivatedRoute,
    private titleService: Title) {
    super(router, activatedRoute);
  }
  public ngOnInit(): void {
    this.titleService.setTitle('Подтверждение почты');
  }

  public onConfirmClick(): void {

  }
}
