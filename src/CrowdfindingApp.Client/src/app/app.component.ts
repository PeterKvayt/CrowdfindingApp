import { Component, ViewChild } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Base } from './views/Base';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent extends Base {
  public subscriptions = new Subscription();

  @ViewChild(NavMenuComponent, {static: false}) navMenuComponent: NavMenuComponent;

  constructor(
    public router: Router,
    public activeatedRoute: ActivatedRoute
  ) {
    super(router, activeatedRoute);

    this.subscriptions.add(
      this.router.events.subscribe(event => {
        if (event instanceof NavigationEnd) {
          this.navMenuComponent.ngOnInit();
        }
      }
      ));
  }
}
