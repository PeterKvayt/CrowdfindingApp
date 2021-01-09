import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpService } from './services/http.service';
import { AuthenticationGuardService } from './services/auth.guard.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ParamInterceptor } from './services/request.interceptor.service';
import { AuthenticationService } from './services/auth.service';
import { JwtModule } from '@auth0/angular-jwt';
import { Settings } from './services/settings.service';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => sessionStorage.getItem(Settings.acccssToken),
      }
    })
  ],
  providers: [
    HttpService,
    AuthenticationGuardService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ParamInterceptor,
      multi: true
    },
    AuthenticationService
  ]
})
export class CommonComponentsModule { }
