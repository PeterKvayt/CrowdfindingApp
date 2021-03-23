import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-rewards-page',
  templateUrl: './rewards-page.component.html',
  styleUrls: ['./rewards-page.component.css']
})
export class RewardsPageComponent extends Base implements OnInit {

  constructor(
    public router: Router,
    public activatedRoute: ActivatedRoute,
    private titleService: Title,
    // private projectService: ProjectService,
    // public fileService: FileService,
    // public authService: AuthenticationService,
  ) { super(router, activatedRoute); }

  ngOnInit() {
    console.log(this.activatedRoute.snapshot.params.projectId);
  }

  setRewards() {

  }

}
