import { NavItem } from './NavItem';
import { AuthenticationService } from 'src/app/services/auth.service';
import { Routes } from 'src/app/models/immutable/Routes';

export class NavItemInitializer {
  constructor(private authService: AuthenticationService) {
    this.centralNavs = [
      this.allProjectsNav,
      this.createProjectNav,
      this.helpNav,
      this.searchNav,
    ];
    this.rightNavs = [
      this.profileNav,
      this.signInNav,
    ];
  }

  public centralNavs: NavItem[];
  public rightNavs: NavItem[];

  public allProjectsNav = new NavItem('Все проекты', Routes.allProjects, 'fas fa-stream', true );
  public createProjectNav = new NavItem('Правила проекта', Routes.projectRules, 'fas fa-book-open', true );
  public helpNav = new NavItem('Помощь', 'help', 'fas fa-info', true );
  public searchNav = new NavItem('Поиск', 'search', 'fas fa-search', true );
  public profileNav = new NavItem('Профиль', Routes.profile, 'fas fa-user-circle');
  public signInNav = new NavItem('Войти', Routes.signIn, 'fas fa-sign-in-alt');

  public prepareNavItems(): void {
    if (this.authService.isAuthenticated()) {
      this.profileNav.show = true;
      this.signInNav.show = false;
    } else {
      this.profileNav.show = false;
      this.signInNav.show = true;
    }
  }
}