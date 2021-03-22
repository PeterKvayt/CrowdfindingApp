import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { SaveDraftProjectRequestMessage } from '../models/requests/projects/SaveDraftProjectRequestMessage';
import { ProjectModerationRequestMessage } from '../models/requests/projects/ProjectModerationRequestMessage';
import { ProjectSearchRequestMessage } from '../models/requests/projects/ProjectSearchRequestMessage';
import { ProjectStatusEnum } from '../models/enums/ProjectStatus';
import { SetProjectStatusRequestMessage } from '../models/requests/projects/SetProjectStatusRequestMessage';


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

  public ownerProjects(model: ProjectSearchRequestMessage) {
    return this.http.post<ProjectSearchRequestMessage>(this.controller + 'ownerProjects', model);
  }

  public search(model: ProjectSearchRequestMessage) {
    return this.http.post<ProjectSearchRequestMessage>(this.controller + 'search', model);
  }

  public openedProjects(model: ProjectSearchRequestMessage) {
    return this.http.post<ProjectSearchRequestMessage>(this.controller + 'openedProjects', model);
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

  public getProjectById(id: string) {
    return this.http.get(this.controller + id);
  }

  public getViewById(id: string) {
    return this.http.get(this.controller + 'projectView/' + id);
  }

  public setStatus(status: ProjectStatusEnum, projectId: string) {
    return this.http.post<SetProjectStatusRequestMessage>(this.controller + 'set-status',
      new SetProjectStatusRequestMessage(status, projectId));
  }
}