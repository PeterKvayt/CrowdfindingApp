import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';

export interface IError {
  status: number;
  message: string;
}

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styles: []
})
export class ErrorComponent extends Base implements OnInit {

  constructor(
    public router: Router,
    public activatedRoute:  ActivatedRoute,
    private titleService: Title
  ) {
    super(router, activatedRoute);
   }


   public message: string;
   public status: number;

  public ngOnInit(): void {
    this.status = this.activatedRoute.snapshot.params['status'];
    this.message = this.activatedRoute.snapshot.params['message'];
    this.titleService.setTitle('Ошибка ' + this.status);
  }
}
