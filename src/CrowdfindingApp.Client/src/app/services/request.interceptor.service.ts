import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Settings } from './settings.service';
import { MessageService } from './message.service';
import { catchError } from 'rxjs/operators';
import { ReplyMessageBase } from '../models/replies/common/ReplyMessageBase';
import { ReplyMessage } from '../models/replies/common/ReplyMessage';

@Injectable()
export class ParamInterceptor implements HttpInterceptor {
  constructor(
    public messageService: MessageService
  ) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = sessionStorage.getItem(Settings.accessToken);
      if (token) {
        const authRequest = request.clone(
          {
            headers: request.headers.set('Authorization', token).set('Content-Type', 'application/json')
          }
        );
        return next.handle(authRequest)
        .pipe(catchError(
          (errorResponse: HttpErrorResponse) => this.handleError(errorResponse)
        ));
      }

    return next.handle(request)
      .pipe(catchError(
      (errorResponse: HttpErrorResponse) => this.handleError(errorResponse)
    ));
  }

  private handleError(errorResponse: HttpErrorResponse): Observable<HttpEvent<any>> {
    if (errorResponse.error  instanceof ErrorEvent) {
      
    } else {
      this.messageService.addErrorRange(errorResponse.error.errors);
    }

    return throwError(errorResponse);
  }
}