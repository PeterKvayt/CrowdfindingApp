import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { ProjectService } from 'src/app/services/project.service';
// import { ProjectCardViewModel } from 'src/app/view-models/ProjectCardViewModel';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-profile-projects',
  templateUrl: './profile-projects.component.html',
  styleUrls: ['./profile-projects.component.css']
})
export class ProfileProjectsComponent extends Base implements OnInit {

  constructor(
    private projectService: ProjectService,
    public router: Router,
    public activatedRoute: ActivatedRoute,
    private titleService: Title
    ) {
      super(router, activatedRoute);
  }
  
  // public projects: ProjectCardViewModel[] = [];

  public ngOnInit(): void {
    this.titleService.setTitle('Мои проекты');
    this.setProjects();
  }

  private setProjects(): void {
    // this.subscriptions.add(
    //   this.projectService.getUserProjects().subscribe(
    //     (response: ProjectCardViewModel[]) => {
    //       this.projects = response;
    //     },
    //     error => { 
    //       this.handleError(error);
    //      }
    //   )
    // )
  }

  public onEditClick(id){
    this.redirect('profile-edit-project');
  }

  public onDeleteClick(id: string){
    this.subscriptions.add(
      this.projectService.delete(id).subscribe(
        () =>  { this.deleteProjectById(id); },
        error => {
          this.handleError(error);
        }
      )
    )
  }

  private deleteProjectById(id: string){
    // for (let index = 0; index < this.projects.length; index++) {
    //   if (this.projects[index].id === id) {
    //     this.projects = this.projects.splice(index, 1);
    //     break;
    //   }
    // }
  }
}
