import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
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
import { DeliveryTypes } from 'src/app/models/immutable/DeliveryType';
import { ReplyMessage } from 'src/app/models/replies/common/ReplyMessage';
import { DraftProjectInfo } from 'src/app/models/replies/projects/DraftProjectInfo';
import { RewardInfo } from 'src/app/models/replies/rewards/RewardInfo';
import { SaveDraftProjectRequestMessage } from 'src/app/models/requests/projects/SaveDraftProjectRequestMessage';
import { QuestionInfo } from 'src/app/models/replies/projects/QuestionInfo';
import { DeliveryTypeEnum } from 'src/app/models/enums/DeliveryTypeEnum';
import { Data } from 'src/app/models/immutable/Data';

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
  public projectDurationInput: DecimalInput = { placeholder: 'Введите продолжительность проекта в днях', min: 1, max: 180 };
  public faqList: QuestionInfo[] = [];
  public projectOwnerSurnameInput: TextInput = { placeholder: 'Фамилия' };
  public projectOwnerNameInput: TextInput = { placeholder: 'Имя' };
  public projectOwnerMiddleNameInput: TextInput = { placeholder: 'Отчество' };
  public projectOwerPassportNumberInput: TextInput = { placeholder: 'Серия и номер', label: 'Паспортные данные', min: 9, max: 9 };
  public projectOwerPrivateNumberInput: TextInput = { placeholder: 'Личный номер', min: 14, max: 14 };
  public projectOwerWhomIssuedDocInput: TextInput = { placeholder: 'Адрес регистрации' };
  public projectOwerPhoneNumberInput: TextInput = { placeholder: 'Контактный номер', min: 13, max: 13 };
  public projectOwerAddressInput: TextInput = { placeholder: 'Кем выдан' };
  public projectOwerWhenIssuedDocInput: DateInput = { label: 'Дата выдачи документа' };
  public projectRewardsList: RewardInfo[] = [];
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
  public rewardDeliveryCountries: GenericLookupItem<string, number>[] = [];
  public rewardWholeWorldDeliveryCostInput: DecimalInput = { placeholder: 'Введите стоимость доставки по всему миру (BYN)', min: 1 };

  // common
  public countryDeliveryCostInput: DecimalInput = { placeholder: 'Введите стоимость доставки (BYN)', min: 1 };
  public questionInput: TextInput = { placeholder: 'Введите вопрос' };
  public answerInput: TextArea = { placeholder: 'Введите ответ на вопрос' };
  public categorySelectInput: SelectInput = {
    list: [],
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
  public rewardCardList: RewardCard[] = [];

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
  public withoutDeliverySubTab = new TabElement('Доставка отсутствует', true);
  public someCountriesDeliverySubTab = new TabElement('Некоторые страны'  , false);
  public wholeWorldDeliverySubTab = new TabElement('Весь мир', false);

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
    this.rewardDeliveryCountries = [];
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
    this.setCategories();
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

  private setCategories(): void {
    this.subscriptions.add(
          this.projectService.getCategories().subscribe(
            (reply: ReplyMessage<LookupItem[]>) => {
              reply.value.forEach(category => {
                this.categorySelectInput.list.push(new SelectItem(category.key, category.value));
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

  public onDeliveryAddClick(): void {
    if (!this.countryDeliveryCostInput.value || !this.selectedCountry) { return; }
    this.countrySelectInput.list.find(x => x.value === this.selectedCountry).disabled = true;
    this.rewardDeliveryCountries.push(
      new GenericLookupItem<string, number>(this.selectedCountry, this.countryDeliveryCostInput.value));
    this.countryDeliveryCostInput.value = undefined;
    const nextCountry = this.countrySelectInput.list.filter(x => x.disabled === false)[0];
    if (nextCountry && this.selectedCountry !== nextCountry.value) {
      this.selectedCountry = nextCountry.value;
    } else {
      this.selectedCountry = undefined;
    }
  }

  public onRemoveCountryFromIncludedList(country: GenericLookupItem<string, number>): void {
    this.countrySelectInput.list.find(x => x.value === country.key).disabled = false;
    this.rewardDeliveryCountries.remove(country);
  }

  public onRewardAddClick(): void {
    this.rewardCardList.push(
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

    let deliveryType = DeliveryTypeEnum.WithoutDelivery;
    if (this.wholeWorldDeliverySubTab.isActive) {
      deliveryType = DeliveryTypeEnum.WholeWorld;
    } else if (this.someCountriesDeliverySubTab.isActive) {
      deliveryType = DeliveryTypeEnum.SomeCountries;
    }

    if (deliveryType === DeliveryTypeEnum.WholeWorld) {
      this.rewardDeliveryCountries.push(new GenericLookupItem(Data.wholeWorldDeliveryId , this.rewardWholeWorldDeliveryCostInput.value));
    }

    const rewardInfo: RewardInfo = {
      id: null,
      projectId: null,
      title: this.rewardNameInput.value,
      price: this.rewardCostInput.value,
      description: this.rewardDescriptionInput.value,
      deliveryDate: new Date(<number><any>this.selectedYear, <number><any>this.selectedMonth),
      isLimited: this.rewardCountRestrictionsInput.value ? true : false,
      limit: this.rewardCountRestrictionsInput.value,
      image: null,
      deliveryType: deliveryType,
      deliveryCountries:  this.rewardDeliveryCountries
    };
    this.projectRewardsList.push(rewardInfo);

    this.selectedDeliveryType = DeliveryTypeEnum.WithoutDelivery.toString();
    this.rewardNameInput.value = undefined;
    this.rewardCostInput.value = undefined;
    this.rewardCountRestrictionsInput.value = undefined;
    this.rewardDescriptionInput.value = undefined;
    this.rewardWholeWorldDeliveryCostInput.value = undefined;
    this.rewardDeliveryCountries = [];
    this.countrySelectInput.list.forEach(country => country.disabled = false);
  }

  public onQuestionAddClick(): void {
    if (!this.questionInput.value || !this.answerInput.value) { return; }
    this.faqList.push(
      new QuestionInfo(this.questionInput.value, this.answerInput.value)
    );
    this.questionInput.value = undefined;
    this.answerInput.value = undefined;
  }

  public onquestionRemoveClick(question: QuestionInfo): void {
    this.faqList.remove(question);
  }

  public onquestionEditClick(question: QuestionInfo): void {
    this.questionInput.value = question.question;
    this.answerInput.value = question.answer;
    this.faqList.remove(question);
  }

  public onDownloadImgClick(): void {
    // console.log(value);
  }

  public toModerationClick(): void {
    
  }

  public onSaveClick(): void {
      const model = this.getSaveRequestModel();
      console.log(model);
      this.subscriptions.add(
        this.projectService.save(model).subscribe(
          () => { this.redirect('profile'); }
        )
      );
  }

  private getSaveRequestModel(): SaveDraftProjectRequestMessage {
    const draft: DraftProjectInfo = {
      id: null,
      categoryId: this.projectCategory,
      title: this.projectNameInput.value,
      shortDescription: this.projectShortDescriptionInput.value,
      fullDescription: this.projectDescriptionInput.value,
      location: this.selectedCity,
      videoUrl: this.projectVideoInput.value,
      image: null,
      startDateTime: null,
      duration: this.projectDurationInput.value,
      budget: this.projectPurposeInput.value,
      authorSurname: this.projectOwnerSurnameInput.value,
      authorName: this.projectOwnerNameInput.value,
      authorDateOfBirth: this.projectOwerBirthdayInput.value,
      authorMiddleName: this.projectOwnerMiddleNameInput.value,
      authorPersonalNumber: this.projectOwerPrivateNumberInput.value,
      whomGivenDocument: this.projectOwerWhomIssuedDocInput.value,
      whenGivenDocument: new Date(this.projectOwerWhenIssuedDocInput.value),
      authorAddress: this.projectOwerAddressInput.value,
      authorPhone: this.projectOwerPhoneNumberInput.value,
      rewards: this.projectRewardsList,
      questions: this.faqList
    };
    return new SaveDraftProjectRequestMessage(draft);
  }

  public onFeedbackShowClick(): void {
    this.feedBackModalShow = true;
  }
}
