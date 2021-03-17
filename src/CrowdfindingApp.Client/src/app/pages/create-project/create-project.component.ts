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
import { DateInput } from 'src/app/components/inputs/date-input/DateInput';
import { ProjectCard } from 'src/app/components/project-card/ProjectCard';
import { SelectInput } from 'src/app/components/selectors/select/SelectInput';
import { SelectItem } from 'src/app/components/selectors/select/SelectItem';
import { RewardCard } from 'src/app/components/reward-card/RewardCard';
import { DeliveryTypes } from 'src/app/models/common/DeliveryType';
import { ReplyMessage } from 'src/app/models/replies/common/ReplyMessage';

@Component({
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
  styleUrls: ['./create-project.component.css']
})
export class CreateProjectComponent extends Base implements OnInit {

  private currentDate = new Date();

  // project fields
  private projectCategory: string;
  public projectNameInput: TextInput = { placeholder: 'Введите название проекта' };
  public projectShortDescriptionInput: TextArea = { placeholder: 'Кратко опишите проект (до 280 символов)', max: 280 };
  public projectDescriptionInput: TextArea = { placeholder: 'Введите полное описание проекта ...' };
  public projectVideoInput: TextInput = { placeholder: 'Введите ссылку на видео' };
  public projectPurposeInput: DecimalInput = { placeholder: 'Введите финансовую цель (BYN)', min: 1 };
  public projectDurationInput: DecimalInput = { placeholder: 'Введите финансовую цель (BYN)', min: 1, max: 180 };
  public faqList: LookupItem[] = [];
  public projectOwnerSurnameInput: TextInput = { placeholder: 'Фамилия' };
  public projectOwnerNameInput: TextInput = { placeholder: 'Имя' };
  public projectOwnerMiddleNameInput: TextInput = { placeholder: 'Отчество' };
  public projectOwerPassportNumberInput: TextInput = { placeholder: 'Серия и номер', label: 'Паспортные данные', min: 9, max: 9 };
  public projectOwerPrivateNumberInput: TextInput = { placeholder: 'Личный номер', min: 14, max: 14 };
  public projectOwerWhomIssuedDocInput: TextInput = { placeholder: 'Адрес регистрации' };
  public projectOwerPhoneNumberInput: TextInput = { placeholder: 'Контактный номер', min: 13, max: 13 };
  public projectOwerAddressInput: TextInput = { placeholder: 'Кем выдан' };
  public projectOwerWhenIssuedDocInput: DateInput = { label: 'Дата выдачи документа' };
  public projectRewardsList: RewardCard[] = [];
  public projectOwerBirthdayInput: DateInput = {
    label: 'Дата рождения',
    max: new Date(this.currentDate.getFullYear() - 18, this.currentDate.getMonth(), this.currentDate.getDay()),
    value: new Date(this.currentDate.getFullYear() - 18, this.currentDate.getMonth(), this.currentDate.getDay()).toISOString().substr(0, 10)
  };

  // reward fields
  public rewardNameInput: TextInput = { placeholder: 'Введите название вознаграждения' };
  public rewardCostInput: DecimalInput = { placeholder: 'Введите стоимость (BYN)', min: 1 };
  public rewardCountRestrictionsInput: DecimalInput = { placeholder: 'Введите количество', min: 1 };
  public rewardDescriptionInput: TextArea = { placeholder: 'Введите описание вознаграждения' };
  public rewardDeliveryIncludedCountries: GenericLookupItem<string, number>[] = [];
  public rewardDeliveryExcludedCountries: GenericLookupItem<string, number>[] = [];
  public rewardWholeWorldDeliveryCostInput: DecimalInput = { placeholder: 'Введите стоимость доставки по всему миру (BYN)', min: 1 };

  // common
  public countryDeliveryCostInput: DecimalInput = { placeholder: 'Введите стоимость доставки (BYN)', min: 1 };
  public questionInput: TextInput = { placeholder: 'Введите вопрос' };
  public answerInput: TextArea = { placeholder: 'Введите ответ на вопрос' };
  public categorySelectInput: SelectInput = {
    list: [
      new SelectItem('1', 'Еда'),
      new SelectItem('2', 'Дизайн'),
    ],
    defaultValue: 'Выберите категорию'
  };
  public citiesSelectInput: SelectInput = {
    list: [],
    defaultValue: 'Выберите город'
  };
  public countrySelectInput: SelectInput = {
    list: [],
    defaultValue: 'Выберите страну'
  };

  // help props
  private selectedDeliveryType = DeliveryTypes.withoutDelivery.value;
  private selectedCountry: string;
  private selectedCity: string;
  private selectedMonth: string;
  private selectedYear: string;
  public feedBackModalShow = false;
  public projectCard: ProjectCard = {
    name: this.projectNameInput.value ? this.projectNameInput.value : 'Название',
    description: this.projectDescriptionInput.value ? this.projectDescriptionInput.value : 'Описание',
    category: this.projectCategory ? this.categorySelectInput.list.find(x => x.value === this.projectCategory === undefined).name : 'Категория',
    imgPath: 'assets/img/stock-project.png',
    purpose: this.projectPurposeInput.value ? this.projectPurposeInput.value : 0,
    currentResult: 0,
    id: null,
  };

  public onProjectNameChange(): void {
    if (this.projectNameInput.value) {
      this.projectCard.name = this.projectNameInput.value;
    } else {
      this.projectCard.name = 'Название';
    }
  }

  public onProjectShortDecriptionChange(): void {
    if (this.projectShortDescriptionInput.value) {
      this.projectCard.description = this.projectShortDescriptionInput.value;
    } else {
      this.projectCard.description = 'Описание';
    }
  }

  public onProjectPurposeChange(): void {
    if (this.projectPurposeInput.value) {
      this.projectCard.purpose = this.projectPurposeInput.value;
    } else {
      this.projectCard.purpose = 0;
    }
  }

  // tabs
  public generalInfoTab = new TabElement('Общая информация', true);
  public rewardsTab = new TabElement('Вознаграждения', false);
  public descriptionTab = new TabElement('Подробное описание', false);
  public ownerInfoTab = new TabElement('Платежная информация', false);

  // sub tabs
  public withoutDeliverySubTab = new TabElement(DeliveryTypes.withoutDelivery.value, true);
  public someCountriesDeliverySubTab = new TabElement(DeliveryTypes.someCountries.value, false);
  public wholeWorldDeliverySubTab = new TabElement(DeliveryTypes.wholeWorld.value, false);

  public getCountryNameById(id: string): string {
    return this.countrySelectInput.list.find(x => x.value === id).name;
  }

  public onTabClick(tab: TabElement): void {
    this.generalInfoTab.isActive = false;
    this.rewardsTab.isActive = false;
    this.descriptionTab.isActive = false;
    this.ownerInfoTab.isActive = false;
    tab.isActive = true;
  }

  public onDeliverySubTabClick(tab: TabElement): void {
    if (tab.isActive) { return; }
    this.selectedDeliveryType = tab.value;
    this.countrySelectInput.list.forEach(x => x.disabled = false);
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
    this.setCountries();
    this.setCities();
  }

  private setCountries(): void {
    this.subscriptions.add(
          this.projectService.getCountries().subscribe(
            (reply: ReplyMessage<LookupItem[]>) => {
              reply.value.forEach(country => {
                this.countrySelectInput.list.push(new SelectItem(country.key, country.value));
              });
            }
          )
        );
  }

  private setCities(): void {
    this.subscriptions.add(
          this.projectService.getCities().subscribe(
            (reply: ReplyMessage<LookupItem[]>) => {
              reply.value.forEach(city => {
                this.citiesSelectInput.list.push(new SelectItem(city.key, city.value));
              });
            }
          )
        );
  }

  // General Tab functional
  public onCategorySelect(value: string): void {
    this.projectCategory = value;
    this.projectCard.category = this.categorySelectInput.list.find(x => x.value === value).name;
  }

  public onCitySelect(value: string): void {
    this.selectedCity = value;
  }

  public onMonthSelect(value: string): void {
    this.selectedMonth = value;
  }

  public onYearSelect(value: string): void {
    this.selectedYear = value;
  }

  public onCountrySelect(value: string): void {
    this.selectedCountry = value;
  }

  public onSomeCountryDeliveryAddClick(): void {
    if (!this.countryDeliveryCostInput.value || !this.selectedCountry) { return; }
    this.countrySelectInput.list.find(x => x.value === this.selectedCountry).disabled = true;
    this.rewardDeliveryIncludedCountries.push(
      new GenericLookupItem<string, number>(this.selectedCountry, this.countryDeliveryCostInput.value));
    this.countryDeliveryCostInput.value = undefined;
  }

  public onExcludedCountryDeliveryAddClick(): void {
    this.rewardDeliveryExcludedCountries.push(
      new GenericLookupItem<string, number>(this.selectedCountry, this.countryDeliveryCostInput.value));
    this.countryDeliveryCostInput.value = undefined;
  }

  public onRemoveCountryFromIncludedList(country: GenericLookupItem<string, number>): void {
    this.countrySelectInput.list.find(x => x.value === country.key).disabled = false;
    this.rewardDeliveryIncludedCountries.remove(country);
  }

  public onRewardAddClick(): void {
    this.projectRewardsList.push(
      new RewardCard(
        this.rewardNameInput.value,
        this.rewardCostInput.value,
        this.rewardDescriptionInput.value,
        'assets/img/stock-reward.jpg',
        this.selectedMonth,
        this.selectedYear,
        this.selectedDeliveryType,
        this.rewardCountRestrictionsInput.value
      )
    );

    this.selectedDeliveryType = DeliveryTypes.withoutDelivery.value;
    this.rewardNameInput.value = undefined;
    this.rewardCostInput.value = undefined;
    this.rewardCountRestrictionsInput.value = undefined;
    this.rewardDescriptionInput.value = undefined;
    this.rewardWholeWorldDeliveryCostInput.value = undefined;
    this.rewardDeliveryExcludedCountries = [];
    this.rewardDeliveryIncludedCountries = [];
  }

  public onQuestionAddClick(): void {
    if (!this.questionInput.value || !this.answerInput.value) { return; }
    this.faqList.push(
      new LookupItem(this.questionInput.value, this.answerInput.value)
    );
    this.questionInput.value = undefined;
    this.answerInput.value = undefined;
  }

  public onquestionRemoveClick(question: LookupItem): void {
    this.faqList.remove(question);
  }

  public onquestionEditClick(question: LookupItem): void {
    this.questionInput.value = question.value;
    this.answerInput.value = question.key;
    this.faqList.remove(question);
  }

  public onDownloadImgClick(): void {
    // console.log(value);
  }

  public toModerationClick(): void {
    
  }

  public onSaveClick(): void {
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
  }

private getProject(): void {

}

  public onFeedbackShowClick(): void {
    this.feedBackModalShow = true;
  }
}
