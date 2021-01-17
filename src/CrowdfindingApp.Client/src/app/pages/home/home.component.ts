import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { ProjectService } from 'src/app/services/project.service';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent extends Base implements OnInit {

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
    this.titleService.setTitle('Главная');
    // this.setProjects();
  }

  // private setProjects(): void {
  //   this.subscriptions.add(
  //     this.projectService.getProjects().subscribe(
  //       (response: ProjectCardViewModel[]) => {
  //         this.projects = response.slice(0,4);
  //       },
  //       error => { 
  //         this.handleError(error);
  //        }
  //     )
  //   )
  // }
}