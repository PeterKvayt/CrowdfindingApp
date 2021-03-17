import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { SaveDraftProjectRequestMessage } from '../models/requests/projects/SaveDraftProjectRequestMessage';


@Injectable()
export class ProjectService {

  constructor(
    private http: HttpService
  ) { }

  private controller = 'projects/';

  public getCities() {
    return this.http.get(this.controller + 'cities');
  }

  public getCountries() {
    return this.http.get(this.controller + 'countries');
  }

  public getCategories() {
    return this.http.get(this.controller + 'categories');
  }

  public save(model: SaveDraftProjectRequestMessage) {
    return this.http.post<SaveDraftProjectRequestMessage>(this.controller + 'save-draft', model);
  }

  public getProjects() {
    return this.http.get('projects');
  }

  public getUserProjects() {
    return this.http.get(this.controller + 'user-projects');
  }

  // public update(model: ProjectModel) {
  //   return this.http.put('projects', model);
  // }

  public delete(id: string) {
    return this.http.delete('projects', id);
  }
}