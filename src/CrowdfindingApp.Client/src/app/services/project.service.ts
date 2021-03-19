import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { SaveDraftProjectRequestMessage } from '../models/requests/projects/SaveDraftProjectRequestMessage';
import { ProjectModerationRequestMessage } from '../models/requests/projects/ProjectModerationRequestMessage';
import { ProjectSearchRequestMessage } from '../models/requests/projects/ProjectSearchRequestMessage';


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

  public moderate(model: ProjectModerationRequestMessage) {
    return this.http.post<ProjectModerationRequestMessage>(this.controller + 'moderate', model);
  }

  public cards(model: ProjectSearchRequestMessage) {
    return this.http.post<ProjectSearchRequestMessage>(this.controller + 'cards', model);
  }

  public getProjects() {
    return this.http.get(this.controller);
  }

  public getUserProjects() {
    return this.http.get(this.controller + 'user-projects');
  }

  public delete(id: string) {
    return this.http.delete('projects', id);
  }
}