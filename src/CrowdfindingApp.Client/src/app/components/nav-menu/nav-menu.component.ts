import { Component, OnInit } from '@angular/core';
import { NavItem } from './NavItem';
import { AuthenticationService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  public navItems: NavItem[] = [
    new NavItem('Профиль', 'profile', 'user-circle'),
    new NavItem('Войти', 'sign-in', 'sign-in-alt'),
    new NavItem('Выйти', 'sign-in', 'sign-out-alt' ),
  ];
  constructor(
    private authService: AuthenticationService
  ) { }

  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  public ngOnInit(): void {
    this.initNavItems();
  }

  private initNavItems(): void {
    if (this.authService.isAuthenticated()) {
      this.navItems[0].show = true;
      this.navItems[1].show = false;
      this.navItems[2].show = true;
    } else {
      this.navItems[0].show = false;
      this.navItems[1].show = true;
      this.navItems[2].show = false;
    }
  }

  public onNavItemClick(event): void {
    if (event.target.innerText === this.navItems[2].value) {
      this.authService.signOut();
    }
  }
}