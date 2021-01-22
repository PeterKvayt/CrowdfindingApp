import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/auth.service';
import { NavItemInitializer } from './NavItemsInitializer';
import { NavItem } from './NavItem';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

  public navsHolder = new NavItemInitializer(this.authService);
  constructor(
    private authService: AuthenticationService
  ) { }

  public isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  public ngOnInit(): void {

  }

  public onNavItemClick(event): void {
    if (event.target.innerText === this.navsHolder.signOutNav.value) {
      this.authService.signOut();
    }
  }
}