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
import { ReplyMessage } from 'src/app/models/replies/common/ReplyMessage';
import { ProjectInfo } from 'src/app/models/replies/projects/ProjectInfo';
import { RewardInfo } from 'src/app/models/replies/rewards/RewardInfo';
import { SaveDraftProjectRequestMessage } from 'src/app/models/requests/projects/SaveDraftProjectRequestMessage';
import { QuestionInfo } from 'src/app/models/replies/projects/QuestionInfo';
import { DeliveryTypeEnum } from 'src/app/models/enums/DeliveryTypeEnum';
import { Data } from 'src/app/models/immutable/Data';
import { ProjectModerationRequestMessage } from 'src/app/models/requests/projects/ProjectModerationRequestMessage';
import { ProjectStatusEnum } from 'src/app/models/enums/ProjectStatus';
import { FileInput } from 'src/app/components/inputs/file-input/FileInput';
import { FileService } from 'src/app/services/file.service';
import { SaveImageRequestMessage } from 'src/app/models/requests/files/SaveImageRequestMessage';
import { AuthenticationService } from 'src/app/services/auth.service';
import { Routes } from 'src/app/models/immutable/Routes';

@Component({
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
  styleUrls: ['./create-project.component.css']
})
export class CreateProjectComponent extends Base implements OnInit {

  private currentDate = new Date();

  // project fields
  public projectId: string = null;
  private projectCategory: string;
  public projectImageInput = new FileInput('Загрузить изображение', 'fas fa-upload');
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
  public projectOwnerPassportNumberInput: TextInput = { placeholder: 'Серия и номер', label: 'Паспортные данные', min: 9, max: 9 };
  public projectOwnerPrivateNumberInput: TextInput = { placeholder: 'Личный номер', min: 14, max: 14 };
  public projectOwnerWhomIssuedDocInput: TextInput = { placeholder: 'Кем  выдан документ' };
  public projectOwnerPhoneNumberInput: TextInput = { placeholder: 'Контактный номер', min: 13, max: 13 };
  public projectOwnerAddressInput: TextInput = { placeholder: 'Адрес регистрации' };
  public projectOwnerWhenIssuedDocInput: DateInput = { label: 'Дата выдачи документа' };
  public projectRewardsList: RewardInfo[] = [];
  public projectOwnerBirthdayInput: DateInput = {
    label: 'Дата рождения',
    max: new Date(this.currentDate.getFullYear() - 18, this.currentDate.getMonth(), this.currentDate.getDay()),
    value: new Date(this.currentDate.getFullYear() - 18, this.currentDate.getMonth(), this.currentDate.getDay())
  };

  // reward fields
  private rewardId: string = null;
  public rewardImageInput = new FileInput('Загрузить изображение', 'fas fa-download');
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

  // help props
  public projectPageRoute = Routes.project;
  private selectedCountry: string;
  private selectedCity: string;
  private selectedMonth: string;
  private selectedYear: string;
  public feedBackModalShow = false;
  public projectCard: ProjectCard = {
    name: this.projectNameInput.value ? this.projectNameInput.value : 'Название',
    description: this.projectDescriptionInput.value ? this.projectDescriptionInput.value : 'Описание',
    categoryName: this.projectCategory ? this.categorySelectInput.list.find(x => x.value === this.projectCategory === undefined).name : 'Категория',
    categoryId: this.projectCategory,
    imgPath: this.projectImageInput.fileName
      ? this.fileService.absoluteFileStoragePath + this.projectImageInput.fileName
      : 'assets/img/stock-project.png',
    purpose: this.projectPurposeInput.value ? this.projectPurposeInput.value : 0,
    currentResult: 0,
    status: ProjectStatusEnum.Draft,
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
  public someCountriesDeliverySubTab = new TabElement('Некоторые страны', false);
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
    public fileService: FileService,
    public authService: AuthenticationService,
    private titleService: Title
  ) { super(router, activatedRoute); }
  public ngOnInit(): void {
    this.setCities();
    this.setCategories();
    this.projectId = this.activatedRoute.snapshot.paramMap.get('projectId');
    if (this.projectId) {
      this.titleService.setTitle('Редактирование проекта');
      this.setProjectInfo(this.projectId);
      this.setCountries(this.projectRewardsList[0] ? this.projectRewardsList[0].deliveryCountries : null);
    } else {
      this.titleService.setTitle('Создание проекта');
      this.setCountries(null);
    }
  }

  setProjectInfo(projectId: string): void {
    this.showLoader = true;
    this.subscriptions.add(
      this.projectService.getProjectById(projectId).subscribe(
        (reply: ReplyMessage<ProjectInfo>) => {
          this.projectCategory = reply.value.categoryId;
          if (reply.value.categoryId) {
            let category = this.categorySelectInput.list.find(x => x.value === reply.value.categoryId);
            if (category) {
              category.selected = true;
            }
          }

          this.projectNameInput.value = reply.value.title;
          this.projectShortDescriptionInput.value = reply.value.shortDescription;
          this.projectDescriptionInput.value = reply.value.fullDescription;

          this.selectedCity = reply.value.location;
          if (reply.value.location) {
            this.citiesSelectInput.list.find(x => x.value === reply.value.location).selected = true;
          }

          this.projectVideoInput.value = reply.value.videoUrl;
          this.projectImageInput.fileName = reply.value.image,
          //startDateTime: null,
          this.projectDurationInput.value = reply.value.duration;
          this.projectPurposeInput.value = reply.value.budget;
          this.projectOwnerSurnameInput.value = reply.value.authorSurname;
          this.projectOwnerNameInput.value = reply.value.authorName;
          this.projectOwnerBirthdayInput.value = reply.value.authorDateOfBirth
            ? reply.value.authorDateOfBirth
            : new Date(this.currentDate.getFullYear() - 18, this.currentDate.getMonth(), this.currentDate.getDay());
          this.projectOwnerMiddleNameInput.value = reply.value.authorMiddleName;
          this.projectOwnerPassportNumberInput.value = reply.value.authorPersonalNo;
          this.projectOwnerPrivateNumberInput.value = reply.value.authorIdentificationNo;
          this.projectOwnerWhomIssuedDocInput.value = reply.value.whomGivenDocument;
          this.projectOwnerWhenIssuedDocInput.value = reply.value.whenGivenDocument;
          this.projectOwnerAddressInput.value = reply.value.authorAddress;
          this.projectOwnerPhoneNumberInput.value = reply.value.authorPhone;
          this.projectRewardsList = reply.value.rewards ? reply.value.rewards : [];
          this.faqList = reply.value.questions ? reply.value.questions : [];
          
          this.projectCard.id = reply.value.id;
          const category = this.categorySelectInput.list.find(x => x.value === reply.value.categoryId);
          this.projectCard.categoryName = category ? category.name : 'Категория';
          this.projectCard.categoryId = reply.value.categoryId;
          this.projectCard.purpose = reply.value.budget;
          this.projectCard.currentResult = 0;
          this.projectCard.description = reply.value.shortDescription ? reply.value.shortDescription : 'Описание';
          this.projectCard.name = reply.value.title ? reply.value.title : 'Название';
          this.projectCard.currentResult = ProjectStatusEnum.Draft;
          this.projectCard.imgPath = reply.value.image ? reply.value.image : 'assets/img/stock-project.png';

          this.showLoader = false;
        },
        () => { this.showLoader = false; }
      )
    );
  }

  private setCountries(countries: GenericLookupItem<string, number>[]): void {
    this.subscriptions.add(
      this.projectService.getCountries().subscribe(
        (reply: ReplyMessage<LookupItem[]>) => {
          if (countries) {
            reply.value.forEach(country => {
              if (countries.find(x => x.key === country.key)) {
                this.countrySelectInput.list.push(new SelectItem(country.key, country.value, true));
              } else {
                this.countrySelectInput.list.push(new SelectItem(country.key, country.value));
              }
            });
          } else {
            reply.value.forEach(country => {
              this.countrySelectInput.list.push(new SelectItem(country.key, country.value));
            });
          }
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
    this.projectCard.categoryName = this.categorySelectInput.list.find(x => x.value === value).name;
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
    let deliveryType = DeliveryTypeEnum.WithoutDelivery;
    if (this.wholeWorldDeliverySubTab.isActive) {
      deliveryType = DeliveryTypeEnum.WholeWorld;
    } else if (this.someCountriesDeliverySubTab.isActive) {
      deliveryType = DeliveryTypeEnum.SomeCountries;
    }

    if (deliveryType === DeliveryTypeEnum.WholeWorld) {
      this.rewardDeliveryCountries.push(new GenericLookupItem(Data.wholeWorldDeliveryId, this.rewardWholeWorldDeliveryCostInput.value));
    }

    let deliveryDate = new Date();
    if (this.selectedYear && this.selectedMonth) {
      deliveryDate = new Date(<number><any>this.selectedYear, <number><any>this.selectedMonth);
    }

    let image = this.rewardImageInput.fileName;
    if(image === 'assets/img/stock-reward.jpg') {
      image = null;
    }
    const rewardInfo: RewardInfo = {
      id: this.rewardId,
      projectId: this.projectId,
      title: this.rewardNameInput.value,
      price: this.rewardCostInput.value,
      description: this.rewardDescriptionInput.value,
      deliveryDate: deliveryDate,
      isLimited: this.rewardCountRestrictionsInput.value ? true : false,
      limit: this.rewardCountRestrictionsInput.value,
      image: image,
      deliveryType: deliveryType,
      deliveryCountries: this.rewardDeliveryCountries
    };
    if (this.rewardId) {
      this.projectRewardsList.remove(this.projectRewardsList.find(x => x.id === this.rewardId));
    }
    this.projectRewardsList.push(rewardInfo);

    this.rewardNameInput.value = undefined;
    this.rewardCostInput.value = undefined;
    this.rewardDescriptionInput.value = undefined;
    this.rewardCountRestrictionsInput.value = undefined;
    this.rewardWholeWorldDeliveryCostInput.value = undefined;
    this.rewardImageInput.fileName = 'assets/img/stock-reward.jpg';
    this.rewardDeliveryCountries = [];
    this.countrySelectInput.list.forEach(country => country.disabled = false);
  }

  onRewardChangeClick(reward: RewardInfo) {
    this.rewardId = reward.id;
    let wholeWorldDeliveryPrice;
    if (reward.deliveryType === DeliveryTypeEnum.WholeWorld) {
      this.onDeliverySubTabClick(this.wholeWorldDeliverySubTab);
      wholeWorldDeliveryPrice = reward.deliveryCountries.find(x => x.key === Data.wholeWorldDeliveryId).value;
    } else if (reward.deliveryType === DeliveryTypeEnum.SomeCountries) {
      this.onDeliverySubTabClick(this.someCountriesDeliverySubTab);
    } else if (reward.deliveryType === DeliveryTypeEnum.WithoutDelivery) {
      this.onDeliverySubTabClick(this.withoutDeliverySubTab);
    }
    this.rewardNameInput.value = reward.title;
    this.rewardCostInput.value = reward.price;
    this.rewardDescriptionInput.value = reward.description;
    this.rewardCountRestrictionsInput.value = reward.limit;
    this.rewardWholeWorldDeliveryCostInput.value = wholeWorldDeliveryPrice;
    this.rewardDeliveryCountries = reward.deliveryCountries;
    this.rewardImageInput.fileName = reward.image;
    this.countrySelectInput.list.forEach(country => {
      if (reward.deliveryCountries.find(x => x.key === country.value)) {
        country.disabled = true;
      }
    });
    this.projectRewardsList.remove(reward);
  }

  onRewardDeleteClick(reward: RewardInfo) {
    this.projectRewardsList.remove(reward);
  }

  getRewardCardFromInfo(reward: RewardInfo): RewardCard {
    return new RewardCard(
      reward.title,
      reward.price,
      reward.description,
      reward.image,
      reward.deliveryType,
      reward.deliveryDate,
      reward.limit
    );
  }

  public onQuestionAddClick(): void {
    if (!this.questionInput.value || !this.answerInput.value) { return; }
    this.faqList.push(
      new QuestionInfo(this.questionInput.value, this.answerInput.value)
    );
    this.questionInput.value = undefined;
    this.answerInput.value = undefined;
  }

  public onQuestionRemoveClick(question: QuestionInfo): void {
    this.faqList.remove(question);
  }

  public onQuestionEditClick(question: QuestionInfo): void {
    this.questionInput.value = question.question;
    this.answerInput.value = question.answer;
    this.faqList.remove(question);
  }

  public onProjectImageUpload(): void {
    
  }

  public toStartClick() {
    this.subscriptions.add(
      this.projectService.setStatus(ProjectStatusEnum.Active, this.projectId).subscribe(
        () => { this.redirect(Routes.profile); },
        () => { console.log('ewed')}
      )
    );
  }

  public toModerationClick(): void {
    this.showLoader = true;
    const model = new ProjectModerationRequestMessage(this.getProjectModel());
    this.subscriptions.add(
      this.projectService.moderate(model).subscribe(
        (reply: ReplyMessage<string>) => {
          this.redirect('profile');
        this.showLoader = false; },
        () => { this.showLoader = false; }
      )
    );
  }

  public onSaveClick(): void {
    this.showLoader = true;
    const model = new SaveDraftProjectRequestMessage(this.getProjectModel());
    this.subscriptions.add(
      this.projectService.save(model).subscribe(
        (reply: ReplyMessage<string>) => {
          if (!this.projectId && reply.value) {
            this.redirect(Routes.projectEdit + '/' + reply.value);
          }
          this.showLoader = false;
        },
        () => { this.showLoader = false; }
      )
    );
  }

  private getProjectModel(): ProjectInfo {
    const draft: ProjectInfo = {
      id: this.projectId,
      categoryId: this.projectCategory,
      title: this.projectNameInput.value,
      shortDescription: this.projectShortDescriptionInput.value,
      fullDescription: this.projectDescriptionInput.value,
      location: this.selectedCity,
      videoUrl: this.projectVideoInput.value,
      image: this.projectImageInput.fileName ? this.projectImageInput.fileName : null,
      //startDateTime: null,
      duration: this.projectDurationInput.value ? this.projectDurationInput.value : null,
      budget: this.projectPurposeInput.value ? this.projectPurposeInput.value : null,
      authorSurname: this.projectOwnerSurnameInput.value,
      authorName: this.projectOwnerNameInput.value,
      authorDateOfBirth: this.projectOwnerBirthdayInput.value,
      authorMiddleName: this.projectOwnerMiddleNameInput.value,
      authorPersonalNo: this.projectOwnerPassportNumberInput.value,
      authorIdentificationNo: this.projectOwnerPrivateNumberInput.value,
      whomGivenDocument: this.projectOwnerWhomIssuedDocInput.value,
      whenGivenDocument: new Date(this.projectOwnerWhenIssuedDocInput.value),
      authorAddress: this.projectOwnerAddressInput.value,
      authorPhone: this.projectOwnerPhoneNumberInput.value,
      rewards: this.projectRewardsList,
      questions: this.faqList
    };
    return draft;
  }

  onImageUpload(input: FileInput) {
    this.showLoader = true;
    const data = new FormData();
    data.append('file', input.file);
    this.subscriptions.add(
    this.fileService.save(data).subscribe(
        (reply: ReplyMessage<string>) => {
          input.fileName = reply.value;
          this.showLoader = false;
        },
        () => {this.showLoader = false; }
      )
    );
  }
}
