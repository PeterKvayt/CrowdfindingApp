import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { SaveImageRequestMessage } from '../models/requests/files/SaveImageRequestMessage';
import { Settings } from './settings.service';


@Injectable()
export class FileService {
  constructor(
    private http: HttpService
  ) { }

  private controller = 'files/';

  public absoluteFileStoragePath = Settings.host + 'root/';
  public save(model: FormData) {
    return this.http.put<FormData>(this.controller + 'save-image', model, 'multipart/form-data; boundary=**SPLITTER**');
  }
}