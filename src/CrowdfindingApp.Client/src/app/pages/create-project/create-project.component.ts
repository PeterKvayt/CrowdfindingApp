import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
// import { ProjectModel } from 'src/app/models/ProjectModel';
import { ProjectService } from 'src/app/services/project.service';
import { TextInput } from 'src/app/components/inputs/text-input/TextInput';
import { DecimalInput } from 'src/app/components/inputs/decimal-input/DecimalInput';
import { Title } from '@angular/platform-browser';
import { TabElement } from 'src/app/components/tab/Tabelement';
import { TextArea } from 'src/app/components/inputs/text-area/TextArea';
import { LookupItem } from 'src/app/models/common/LookupItem';

@Component({
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
  styleUrls: ['./create-project.component.css']
})
export class CreateProjectComponent extends Base implements OnInit {

  
  public projectNameInput: TextInput = { placeholder: 'Введите название проекта' };
  public projectShortDescriptionInput: TextArea = { placeholder: 'Кратко опишите проект (до 280 символов)' };
  public projectVideoInput: TextArea = { placeholder: 'Введите ссылку на видео' };
  public purposeInput: DecimalInput = { placeholder: 'Введите финансовую цель (BYN)', min: 1 };
  public durationInput: DecimalInput = { placeholder: 'Введите финансовую цель (BYN)', min: 1, max: 180 };
  public categorySelect: LookupItem[] = 
  [
    new LookupItem('Еда', '1'),
    new LookupItem('Дизайн', '2'),
  ];
  public citiesList: LookupItem[] = 
  [
    new LookupItem('Минск', '1'),
    new LookupItem('Пинск', '2'),
  ];

  public generalInfoTab = new TabElement('Общая информация', true);
  public rewardsTab = new TabElement('Вознаграждения', false);
  public descriptionTab = new TabElement('Подробное описание', false);
  public paymentTab = new TabElement('Платежная информация', false);

  constructor(
    public router: Router,
    private projectService: ProjectService,
    public activatedRoute: ActivatedRoute,
    private titleService: Title
    ) { super(router, activatedRoute); }
  public ngOnInit(): void {
    this.titleService.setTitle('Создание проекта');
  }

  public onTabClick(tab: TabElement): void {
    this.generalInfoTab.isActive = false;
    this.rewardsTab.isActive = false;
    this.descriptionTab.isActive = false;
    this.paymentTab.isActive = false;
    tab.isActive = true;
  }

  // General Tab functional

  public onCategorySelect(value: string): void{
    console.log(value);
  }

  public onCitySelect(value: string): void{
    console.log(value);
  }

  public onDownloadImgClick(): void{
    // console.log(value);
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
