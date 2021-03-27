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
import { AuthenticationService } from 'src/app/services/auth.service';
import { Routes } from 'src/app/models/immutable/Routes';
import { ProjectInputs } from './ProjectInputs';
import { RewardInputs } from './RewardInputs';

@Component({
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
  styleUrls: ['./create-project.component.css']
})
export class CreateProjectComponent extends Base implements OnInit {

  private currentDate = new Date();

  public projectInputs = new ProjectInputs();
  public rewardInputs = new RewardInputs();

  // project fields
  public projectId: string = null;
  public faqList: QuestionInfo[] = [];
  public projectRewardsList: RewardInfo[] = [];

  // reward fields
  private rewardId: string = null;
  public rewardDeliveryCountries: GenericLookupItem<string, number>[] = [];
  // public rewardInputs.rewardImageInput = new FileInput('Загрузить изображение', 'fas fa-download');
  // public rewardInputs.rewardNameInput: TextInput = { placeholder: 'Введите название вознаграждения' };
  // public rewardInputs.rewardCostInput: DecimalInput = { placeholder: 'Введите стоимость (BYN)', min: 1 };
  // public rewardInputs.rewardCountRestrictionsInput: DecimalInput = { placeholder: 'Введите количество', min: 1 };
  // public rewardInputs.rewardDescriptionInput: TextArea = { placeholder: 'Введите описание вознаграждения' };
  // public rewardInputs.rewardWholeWorldDeliveryCostInput: DecimalInput = { placeholder: 'Введите стоимость доставки по всему миру (BYN)', min: 1 };

  // common
  public countryDeliveryCostInput: DecimalInput = { placeholder: 'Введите стоимость доставки (BYN)', min: 1 };
  public questionInput: TextInput = { placeholder: 'Введите вопрос' };
  public answerInput: TextArea = { placeholder: 'Введите ответ на вопрос' };

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
    name: this.projectInputs.name.value ? this.projectInputs.name.value : 'Название',
    description: this.projectInputs.description.value ? this.projectInputs.description.value : 'Описание',
    categoryName: this.projectInputs.categorySelect.currentValue ? this.projectInputs.categorySelect.currentValue.name : 'Категория',
    categoryId: this.projectInputs.categorySelect.currentValue ? this.projectInputs.categorySelect.currentValue.value : undefined,
    imgPath: this.projectInputs.image.fileName
      ? this.fileService.absoluteFileStoragePath + this.projectInputs.image.fileName
      : 'assets/img/stock-project.png',
    purpose: this.projectInputs.purpose.value ? this.projectInputs.purpose.value : 0,
    currentResult: 0,
    status: ProjectStatusEnum.Draft,
    id: null,
  };

  public onProjectNameChange(): void {
    if (this.projectInputs.name.value) {
      this.projectCard.name = this.projectInputs.name.value;
    } else {
      this.projectCard.name = 'Название';
    }
  }

  public onProjectShortDecriptionChange(): void {
    if (this.projectInputs.shortDescription.value) {
      this.projectCard.description = this.projectInputs.shortDescription.value;
    } else {
      this.projectCard.description = 'Описание';
    }
  }

  public onProjectPurposeChange(): void {
    if (this.projectInputs.purpose.value) {
      this.projectCard.purpose = this.projectInputs.purpose.value;
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
          if (reply.value.categoryId) {
            let category = this.projectInputs.categorySelect.list.find(x => x.value === reply.value.categoryId);
            if (category) {
              category.selected = true;
              this.projectInputs.categorySelect.currentValue = category;
            }
          }

          this.projectInputs.name.value = reply.value.title;
          this.projectInputs.shortDescription.value = reply.value.shortDescription;
          this.projectInputs.description.value = reply.value.fullDescription;

          this.selectedCity = reply.value.location;
          if (reply.value.location) {
            this.citiesSelectInput.list.find(x => x.value === reply.value.location).selected = true;
          }

          this.projectInputs.video.value = reply.value.videoUrl;
          this.projectInputs.image.fileName = reply.value.image,
          //startDateTime: null,
          this.projectInputs.duration.value = reply.value.duration;
          this.projectInputs.purpose.value = reply.value.budget;
          this.projectInputs.ownerSurname.value = reply.value.authorSurname;
          this.projectInputs.ownerName.value = reply.value.authorName;
          this.projectInputs.ownerBirthday.value = reply.value.authorDateOfBirth
            ? reply.value.authorDateOfBirth
            : new Date(this.currentDate.getFullYear() - 18, this.currentDate.getMonth(), this.currentDate.getDay());
          this.projectInputs.ownerMiddleName.value = reply.value.authorMiddleName;
          this.projectInputs.ownerPassportNumber.value = reply.value.authorPersonalNo;
          this.projectInputs.ownerPrivateNumber.value = reply.value.authorIdentificationNo;
          this.projectInputs.ownerWhomIssuedDoc.value = reply.value.whomGivenDocument;
          this.projectInputs.ownerWhenIssuedDoc.value = reply.value.whenGivenDocument;
          this.projectInputs.ownerAddress.value = reply.value.authorAddress;
          this.projectInputs.ownerPhoneNumber.value = reply.value.authorPhone;
          this.projectRewardsList = reply.value.rewards ? reply.value.rewards : [];
          this.faqList = reply.value.questions ? reply.value.questions : [];
          
          this.projectCard.id = reply.value.id;
          const category = this.projectInputs.categorySelect.list.find(x => x.value === reply.value.categoryId);
          this.projectCard.categoryName = category ? category.name : 'Категория';
          this.projectCard.categoryId = reply.value.categoryId;
          this.projectCard.purpose = reply.value.budget;
          this.projectCard.currentResult = 0;
          this.projectCard.description = reply.value.shortDescription ? reply.value.shortDescription : 'Описание';
          this.projectCard.name = reply.value.title ? reply.value.title : 'Название';
          this.projectCard.status = ProjectStatusEnum.Draft;
          this.projectCard.imgPath = reply.value.image;

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
            this.projectInputs.categorySelect.list.push(new SelectItem(category.key, category.value));
          });
        }
      )
    );
  }

  // General Tab functional
  public onCategorySelect(value: string): void {
    this.projectCard.categoryName = this.projectInputs.categorySelect.currentValue.name;
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
      this.rewardDeliveryCountries.push(new GenericLookupItem(Data.wholeWorldDeliveryId, this.rewardInputs.wholeWorldDeliveryCost.value));
    }

    let deliveryDate = new Date();
    if (this.selectedYear && this.selectedMonth) {
      deliveryDate = new Date(<number><any>this.selectedYear, <number><any>this.selectedMonth);
    }

    const rewardInfo: RewardInfo = {
      id: this.rewardId,
      projectId: this.projectId,
      title: this.rewardInputs.name.value,
      price: this.rewardInputs.cost.value,
      description: this.rewardInputs.description.value,
      deliveryDate: deliveryDate,
      isLimited: this.rewardInputs.countRestrictions.value ? true : false,
      limit: this.rewardInputs.countRestrictions.value,
      image: this.rewardInputs.image.fileName,
      deliveryType: deliveryType,
      deliveryCountries: this.rewardDeliveryCountries
    };
    if (this.rewardId) {
      this.projectRewardsList.remove(this.projectRewardsList.find(x => x.id === this.rewardId));
    }
    this.projectRewardsList.push(rewardInfo);

    this.rewardInputs.name.value = undefined;
    this.rewardInputs.cost.value = undefined;
    this.rewardInputs.description.value = undefined;
    this.rewardInputs.countRestrictions.value = undefined;
    this.rewardInputs.wholeWorldDeliveryCost.value = undefined;
    this.rewardInputs.image.fileName = undefined;
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
    this.rewardInputs.name.value = reward.title;
    this.rewardInputs.cost.value = reward.price;
    this.rewardInputs.description.value = reward.description;
    this.rewardInputs.countRestrictions.value = reward.limit;
    this.rewardInputs.wholeWorldDeliveryCost.value = wholeWorldDeliveryPrice;
    this.rewardDeliveryCountries = reward.deliveryCountries;
    this.rewardInputs.image.fileName = reward.image;
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
      reward.isLimited,
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
      categoryId: this.projectInputs.categorySelect.currentValue ? this.projectInputs.categorySelect.currentValue.value : undefined,
      title: this.projectInputs.name.value,
      shortDescription: this.projectInputs.shortDescription.value,
      fullDescription: this.projectInputs.description.value,
      location: this.selectedCity,
      videoUrl: this.projectInputs.video.value,
      image: this.projectInputs.image.fileName ? this.projectInputs.image.fileName : null,
      //startDateTime: null,
      duration: this.projectInputs.duration.value ? this.projectInputs.duration.value : null,
      budget: this.projectInputs.purpose.value ? this.projectInputs.purpose.value : null,
      authorSurname: this.projectInputs.ownerSurname.value,
      authorName: this.projectInputs.ownerName.value,
      authorDateOfBirth: this.projectInputs.ownerBirthday.value,
      authorMiddleName: this.projectInputs.ownerMiddleName.value,
      authorPersonalNo: this.projectInputs.ownerPassportNumber.value,
      authorIdentificationNo: this.projectInputs.ownerPrivateNumber.value,
      whomGivenDocument: this.projectInputs.ownerWhomIssuedDoc.value,
      whenGivenDocument: new Date(this.projectInputs.ownerWhenIssuedDoc.value),
      authorAddress: this.projectInputs.ownerAddress.value,
      authorPhone: this.projectInputs.ownerPhoneNumber.value,
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
