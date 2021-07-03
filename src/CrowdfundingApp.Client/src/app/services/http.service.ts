import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Settings } from './settings.service';

@Injectable()
export class HttpService {
  constructor(private httpClient: HttpClient) { }
  
  public get(controller: string) {
    return this.httpClient.get(Settings.apiRoute + controller);
  }

  public post<ModelType>(controller: string, model: ModelType, contentType: string = 'application/json') {
     const headerOptions = new HttpHeaders().set('Content-Type', contentType);
     return this.httpClient.post(Settings.apiRoute + controller, model, {headers: headerOptions});
  }

  public delete(controller: string, id: number | string) {
    const action = Settings.apiRoute + controller + '/' + id;
    return this.httpClient.delete(action);
  }

  public put<ModelType>(controller: string, model: ModelType, contentType: string = 'application/json') {
    //  const headerOptions = new HttpHeaders().set('Content-Type', contentType);
    return this.httpClient.put(Settings.apiRoute + controller, model);
  }
}
