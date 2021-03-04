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
import { GenericLookupItem } from 'src/app/models/common/GenericLookupItem';
import { retry } from 'rxjs/operators';

@Component({
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
  styleUrls: ['./create-project.component.css']
})
export class CreateProjectComponent extends Base implements OnInit {

  // project fields
  public projectNameInput: TextInput = { placeholder: 'Введите название проекта' };
  public projectShortDescriptionInput: TextArea = { placeholder: 'Кратко опишите проект (до 280 символов)' };
  public projectVideoInput: TextInput = { placeholder: 'Введите ссылку на видео' };
  public purposeInput: DecimalInput = { placeholder: 'Введите финансовую цель (BYN)', min: 1 };
  public durationInput: DecimalInput = { placeholder: 'Введите финансовую цель (BYN)', min: 1, max: 180 };

  // reward fields
  public rewardNameInput: TextInput = { placeholder: 'Введите название вознаграждения' };
  public rewardCostInput: DecimalInput = { placeholder: 'Введите стоимость (BYN)', min: 1 };
  public rewardCountRestrictionsInput: DecimalInput = { placeholder: 'Введите количество', min: 1 };
  public rewardDescriptionInput: TextArea = { placeholder: 'Введите описание вознаграждения' };
  public rewardDeliveryIncludedCountries: GenericLookupItem<string, number>[] = [];
  public rewardDeliveryExcludedCountries: string[] = [];
  public rewardWholeWorldDeliveryCostInput: DecimalInput = { placeholder: 'Введите стоимость доставки по всему миру (BYN)', min: 1 };

  public countryDeliveryCostInput: DecimalInput = { placeholder: 'Введите стоимость доставки (BYN)', min: 1 };

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
  public countryList: LookupItem[] = 
  [
    new LookupItem('Беларусь', '1'),
    new LookupItem('Россия', '2'),
  ];

  public generalInfoTab = new TabElement('Общая информация', true);
  public rewardsTab = new TabElement('Вознаграждения', false);
  public descriptionTab = new TabElement('Подробное описание', false);
  public paymentTab = new TabElement('Платежная информация', false);

  public onTabClick(tab: TabElement): void {
    this.generalInfoTab.isActive = false;
    this.rewardsTab.isActive = false;
    this.descriptionTab.isActive = false;
    this.paymentTab.isActive = false;
    tab.isActive = true;
  }

  public withoutDeliverySubTab = new TabElement('Доставка отсутствует', true);
  public someCountriesDeliverySubTab = new TabElement('Некоторые страны', false);
  public wholeWorldDeliverySubTab = new TabElement('Весь мир', false);

  public onDeliverySubTabClick(tab: TabElement): void {
    if(tab.isActive) { return; }

    this.rewardDeliveryExcludedCountries = [];
    this.rewardDeliveryIncludedCountries = [];
    this.withoutDeliverySubTab.isActive = false;
    this.someCountriesDeliverySubTab.isActive = false;
    this.wholeWorldDeliverySubTab.isActive = false;
    tab.isActive = true;
  }

  constructor(
    public router: Router,
    private projectService: ProjectService,
    public activatedRoute: ActivatedRoute,
    private titleService: Title
    ) { super(router, activatedRoute); }
  public ngOnInit(): void {
    this.titleService.setTitle('Создание проекта');
  }

  

  // General Tab functional

  public onCategorySelect(value: string): void {
    console.log(value);
  }

  public onCitySelect(value: string): void {
    console.log(value);
  }

  public onMonthSelect(value: string): void {
    console.log(value);
  }

  public onYearSelect(value: string): void {
    console.log(value);
  }

  private selectedCountry: string;
  public onCountrySelect(value: string): void {
    this.selectedCountry = value;
  }

  public onSomeCountryDeliveryAddClick(): void {
    if (!this.countryDeliveryCostInput.value || !this.selectedCountry) { return; }
    this.rewardDeliveryIncludedCountries.push(
      new GenericLookupItem<string, number>(this.selectedCountry, this.countryDeliveryCostInput.value));
      this.countryDeliveryCostInput.value = undefined;
  }

  public onExcludedCountryDeliveryAddClick(): void {
    this.rewardDeliveryExcludedCountries.push(
      new GenericLookupItem<string, number>(this.selectedCountry, this.countryDeliveryCostInput.value));
  }

  public onRemoveCountryFromIncludedList(country: GenericLookupItem<string, number>): void {
    this.rewardDeliveryIncludedCountries.remove(country);
  }

  public onRewardAddClick(): void {
    this.rewardNameInput.value = undefined;
    this.rewardCostInput.value = undefined;
    this.rewardCountRestrictionsInput.value = undefined;
    this.rewardDescriptionInput.value = undefined;
    this.rewardWholeWorldDeliveryCostInput.value = undefined;
    this.rewardDeliveryExcludedCountries = [];
    this.rewardDeliveryIncludedCountries = [];
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
