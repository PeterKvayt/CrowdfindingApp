import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS  } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { ProjectCardComponent } from './components/project-card/project-card.component';
import { HttpService } from './services/http.service';
import { SignInComponent } from './pages/sign-in/sign-in.component';
import { SignUpComponent } from './pages/sign-up/sign-up.component';
import { ForgotPasswordComponent } from './pages/forgot-password/forgot-password.component';
import { UserService } from './services/user.service';
import { AuthenticationGuardService } from './services/auth.guard.service';
import { ProfileComponent } from './pages/profile/profile.component';
import { JwtModule } from '@auth0/angular-jwt';
import { CreateProjectComponent } from './pages/create-project/create-project.component';
import { ProjectService } from './services/project.service';
import { ErrorComponent } from './pages/error/error.component';
import { ParamInterceptor } from './services/request.interceptor.service';
import { TextInputComponent } from './components/inputs/text-input/text-input.component';
import { PasswordInputComponent } from './components/inputs/password-input/password-input.component';
import { DecimalInputComponent } from './components/inputs/decimal-input/decimal-input.component';
import { RegularBtnComponent } from './components/buttons/regular-btn/regular-btn.component';
import { PreloaderComponent } from './components/preloader/preloader.component';
import { ResetPasswordComponent } from './pages/reset-password/reset-password.component';
import { EmailConfirmationComponent } from './pages/email-confirmation/email-confirmation.component';
import { EmailConfirmedComponent } from './pages/email-confirmed/email-confirmed.component';
import { AuthenticationService } from './services/auth.service';
import { MessageService } from './services/message.service';
import { LinkComponent } from './components/link/link.component';
import { MessageComponent } from './components/message/message.component';
import { CreateProjectRulesComponent } from './pages/create-project-rules/create-project-rules.component';
import { TabComponent } from './components/tab/tab.component';
import { TextAreaComponent } from './components/inputs/text-area/text-area.component';
import { DropdownComponent } from './components/dropdown/dropdown.component';
import { SelectComponent } from './components/selectors/select/select.component';
import { MonthSelectComponent } from './components/selectors/month-select/month-select.component';
import { YearSelectorComponent } from './components/selectors/year-selector/year-selector.component';
import { DateInputComponent } from './components/inputs/date-input/date-input.component';
import { CollapseComponent } from './components/collapse/collapse.component';
import { FeedbackModalComponent } from './components/modals/feedback-modal/feedback-modal.component';
import { RewardCardComponent } from './components/reward-card/reward-card.component';
import { Routes } from './models/immutable/Routes';
import { FileInputComponent } from './components/inputs/file-input/file-input.component';
import { FileService } from './services/file.service';
import { PagingComponent } from './components/paging/paging.component';
import { ModerationListComponent } from './pages/moderation-list/moderation-list.component';
import { Roles } from './models/immutable/Roles';
import { ProjectPageComponent } from './pages/project-page/project-page.component';
import { AllProjectsComponent } from './pages/all-projects/all-projects.component';
import { CategoryItemComponent } from './components/category-item/category-item.component';
import { RewardsPageComponent } from './pages/rewards-page/rewards-page.component';
import { RewardService } from './services/reward.service';
import { CreateOrderPageComponent } from './pages/create-order-page/create-order-page.component';
import { OrderService } from './services/order.service';
import { ProfileSettingsComponent } from './pages/profile-settings/profile-settings.component';
import { HelpPageComponent } from './pages/help-page/help-page.component';
import { ImgComponent } from './components/img/img.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    ProjectCardComponent,
    SignInComponent,
    SignUpComponent,
    ForgotPasswordComponent,
    ProfileComponent,
    CreateProjectComponent,
    ErrorComponent,
    TextInputComponent,
    PasswordInputComponent,
    DecimalInputComponent,
    ResetPasswordComponent,
    RegularBtnComponent,
    PreloaderComponent,
    EmailConfirmationComponent,
    EmailConfirmedComponent,
    LinkComponent,
    MessageComponent,
    CreateProjectRulesComponent,
    TabComponent,
    TextAreaComponent,
    DropdownComponent,
    SelectComponent,
    MonthSelectComponent,
    YearSelectorComponent,
    DateInputComponent,
    CollapseComponent,
    FeedbackModalComponent,
    RewardCardComponent,
    FileInputComponent,
    PagingComponent,
    ModerationListComponent,
    ProjectPageComponent,
    AllProjectsComponent,
    CategoryItemComponent,
    RewardsPageComponent,
    CreateOrderPageComponent,
    ProfileSettingsComponent,
    HelpPageComponent,
    ImgComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: Routes.help, component: HelpPageComponent },
      { path: Routes.signIn, component: SignInComponent },
      { path: Routes.signUp, component: SignUpComponent },
      { path: Routes.allProjects, component: AllProjectsComponent },
      { path: Routes.rewards, component: RewardsPageComponent },
      { path: Routes.forgotPassword, component: ForgotPasswordComponent },
      { path: Routes.resetPassword, component: ResetPasswordComponent },
      { path: Routes.emailConfirmation, component: EmailConfirmationComponent },
      { path: Routes.emailConfirmed, component: EmailConfirmedComponent },
      { path: Routes.projectRules, component: CreateProjectRulesComponent },
      { path: Routes.projectCreate, component: CreateProjectComponent, canActivate: [AuthenticationGuardService] },
      { path: Routes.order + '/:rewardId', component: CreateOrderPageComponent, canActivate: [AuthenticationGuardService] },
      { path: Routes.projectEdit + '/:projectId', component: CreateProjectComponent, canActivate: [AuthenticationGuardService] },
      { path: Routes.moderationList,  component: ModerationListComponent,  canActivate: [AuthenticationGuardService],
        data: { role: Roles.admin } },
      { path: Routes.project + '/:projectId', component: ProjectPageComponent },
      { path: Routes.profile, component: ProfileComponent, canActivate: [AuthenticationGuardService] },
      { path: Routes.profileSettings, component: ProfileSettingsComponent, canActivate: [AuthenticationGuardService] },
      { path: Routes.profile + '/:userId', component: ProfileComponent, canActivate: [AuthenticationGuardService] },
      { path: 'error/:status', component: ErrorComponent },
      { path: '**', component: HomeComponent }
    ]),
    JwtModule.forRoot({
      config: {
        tokenGetter: () => sessionStorage.getItem('authToken'),
      }
    })
  ],
  providers: [
    MessageService,
    HttpService,
    OrderService,
    UserService,
    RewardService,
    ProjectService,
    FileService,
    AuthenticationGuardService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ParamInterceptor,
      multi: true
    },
    AuthenticationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

// extensions

declare global {
  export interface Array<T> {
    remove: (this: Array<T>, value: T) => void;
    removeByProp: <TArray, TValue>(this: Array<T>, propName: string, value: TValue) => void;
  }
}

Array.prototype.remove = function<T>(this: Array<T>, value: T): void{
  const index = this.indexOf(value);
  if (index > -1) {
    this.splice(index, 1);
  }
};

Array.prototype.removeByProp = function<TArray, TValue>(this: TArray[], propName: string, value: TValue): void {
  for (let index = 0; index < this.length; index++) {
    if (this[index]
        && this[index].hasOwnProperty(propName)
        && (arguments.length > 2 && this[index][propName] === value )
      ) {
      this.splice(index, 1);
    }
  }
};

