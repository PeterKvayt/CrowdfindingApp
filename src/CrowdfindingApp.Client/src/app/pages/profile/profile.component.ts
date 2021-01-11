import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Base } from '../Base';
import { AccountService } from 'src/app/services/account.service';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-profile',

  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent extends Base implements OnInit {
  constructor(
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public accountService: AccountService,
    private titleService: Title
  ) {
    super(router, activatedRoute);
  }

  public ngOnInit(): void {
    this.titleService.setTitle('Профиль');
  }
}