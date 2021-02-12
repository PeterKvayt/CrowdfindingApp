import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
// import { ProjectModel } from 'src/app/models/ProjectModel';
import { ProjectService } from 'src/app/services/project.service';
import { TextInput } from 'src/app/components/inputs/text-input/TextInput';
import { DecimalInput } from 'src/app/components/inputs/decimal-input/DecimalInput';
import { Title } from '@angular/platform-browser';
import { TabElement } from 'src/app/components/tab/Tabelement';

@Component({
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
  styleUrls: ['./create-project.component.css']
})
export class CreateProjectComponent extends Base implements OnInit {

  public nameInput: TextInput = { label: 'Название' };
  public descriptionInput: TextInput = { label: 'Описание' };
  public purposeInput: DecimalInput = { label: 'Введите финансовую цель', placeholder: 'Финансовая цель', min: 1};
  public imageInput: string;
  
  constructor(
    public router: Router,
    private projectService: ProjectService,
    public activatedRoute: ActivatedRoute,
    private titleService: Title
    ) { super(router, activatedRoute); }
  public ngOnInit(): void {
    this.titleService.setTitle('Создание проекта');
  }

  public generalInfoTab = new TabElement('Общая информация', false);
  public rewardsTab = new TabElement('Вознаграждения', false);
  public descriptionTab = new TabElement('Подробное описание', false);
  public paymentTab = new TabElement('Платежная информация', false);

  public onTabClick(tab: TabElement): void {
    this.generalInfoTab.iaActive = false;
    this.rewardsTab.iaActive = false;
    this.descriptionTab.iaActive = false;
    this.paymentTab.iaActive = false;
    tab.iaActive = true;
  }

  // public onCreateClick(): void {
  //   const project: ProjectModel = {
  //     Name: this.nameInput.value,
  //     Description: this.descriptionInput.value,
  //     CategoryId: '',
  //     ImgPath: this.imageInput,
  //     FinancialPurpose: this.purposeInput.value,
  //     CurrentResult: 0
  //   };
  //   this.subscriptions.add(
  //     this.projectService.create(project).subscribe(
  //       () => { this.redirect('profile'); }
  //     )
  //   );
  // }
}
