import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Settings } from './settings.service';

@Injectable()
export class ParamInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = sessionStorage.getItem(Settings.accessToken);
      if (token) {
        const authRequest = request.clone(
          {
            headers: request.headers.set('Authorization', token).set('Content-Type', 'application/json')
          }
        );
        return next.handle(authRequest);
      }

    return next.handle(request);
  }
}