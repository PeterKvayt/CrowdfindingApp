import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { ProjectModel } from '../models/ProjectModel';


@Injectable()
export class ProjectService {

  constructor(
    private http: HttpService
  ) { }

  public create(model: ProjectModel) {
    return this.http.postJson<ProjectModel>('projects', model);
  }

  public getProjects() {
    return this.http.get('projects');
  }

  public getUserProjects() {
    return this.http.get('projects/user-projects');
  }

  public update(model: ProjectModel) {
    return this.http.put('projects', model);
  }

  public delete(id: string) {
    return this.http.delete('projects', id);
  }
}