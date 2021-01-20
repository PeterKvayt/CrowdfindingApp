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
import { ProfileProjectsComponent } from './pages/profile-projects/profile-projects.component';
import { TextInputComponent } from './components/inputs/text-input/text-input.component';
import { PasswordInputComponent } from './components/inputs/password-input/password-input.component';
import { DecimalInputComponent } from './components/inputs/decimal-input/decimal-input.component';
import { RegularBtnComponent } from './components/buttons/regular-btn/regular-btn.component';
import { ResetPasswordComponent } from './pages/reset-password/reset-password.component';
import { ProfileInfoComponent } from './pages/profile-info/profile-info.component';
import { ProfileSecurityComponent } from './pages/profile-security/profile-security.component';
import { AuthenticationService } from './services/auth.service';

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
    ProfileProjectsComponent,
    TextInputComponent,
    PasswordInputComponent,
    DecimalInputComponent,
    ResetPasswordComponent,
    ProfileInfoComponent,
    ProfileSecurityComponent,
    RegularBtnComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'sign-in', component: SignInComponent },
      { path: 'sign-up', component: SignUpComponent },
      { path: 'forgot-password', component: ForgotPasswordComponent },
      { path: 'reset-password', component: ResetPasswordComponent },
      { path: 'profile', component: ProfileComponent, canActivate: [AuthenticationGuardService] },
      { path: 'profile/projects', component: ProfileProjectsComponent, canActivate: [AuthenticationGuardService] },
      { path: 'profile/create-project', component: CreateProjectComponent, canActivate: [AuthenticationGuardService] },
      { path: 'profile/info', component: ProfileInfoComponent, canActivate: [AuthenticationGuardService] },
      { path: 'profile/security', component: ProfileSecurityComponent, canActivate: [AuthenticationGuardService] },
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
    HttpService,
    UserService,
    ProjectService,
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
