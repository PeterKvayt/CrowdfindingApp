import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { RewardService } from 'src/app/services/reward.service';
import { ReplyMessage } from 'src/app/models/replies/common/ReplyMessage';
import { RewardInfo } from 'src/app/models/replies/rewards/RewardInfo';
import { RewardCard } from 'src/app/components/reward-card/RewardCard';
import { DeliveryTypeEnum } from 'src/app/models/enums/DeliveryTypeEnum';
import { TextInput } from 'src/app/components/inputs/text-input/TextInput';
import { SelectInput } from 'src/app/components/selectors/select/SelectInput';
import { DecimalInput } from 'src/app/components/inputs/decimal-input/DecimalInput';
import { OrderService } from 'src/app/services/order.service';
import { AcceptOrderRequestMessage } from 'src/app/models/requests/orders/AcceptorderRequestMessage';
import { Routes } from 'src/app/models/immutable/Routes';
import { LookupItem } from 'src/app/models/common/LookupItem';
import { SelectItem } from 'src/app/components/selectors/select/SelectItem';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-create-order-page',
  templateUrl: './create-order-page.component.html',
  styleUrls: ['./create-order-page.component.css']
})
export class CreateOrderPageComponent extends Base implements OnInit {

  constructor(
    public router: Router,
    public activatedRoute: ActivatedRoute,
    private titleService: Title,
    private rewardService: RewardService,
    public orderService: OrderService,
    private projectService: ProjectService
  ) { super(router, activatedRoute); }

  public rewardId: string;
  public reward: RewardInfo;

  public countInput: DecimalInput = { placeholder: 'Введите коичество', min: 1, value: 1, label: 'Введите количество' };
  public nameInput: TextInput = { placeholder: 'Имя получателя' };
  public surnameInput: TextInput = { placeholder: 'Фамилия получателя' };
  public middleNameInput: TextInput = { placeholder: 'Отчество получателя' };
  public fullAddressInput: TextInput = {
    placeholder: 'Полный адрес',
    example: 'Пример: г. Минск, ул. Первомайская, дом 1, кв.13'
  };
  public postIndexInput: TextInput = { placeholder: 'Почтовый индекс' };

  public countrySelect: SelectInput = { list: [], defaultValue: 'Выберите страну' };
  public currentCountryId: string;
  
  // public countries: LookupItem[] = [];

  ngOnInit() {
    this.titleService.setTitle('Оформление заказа');
    this.rewardId = this.activatedRoute.snapshot.params.rewardId;
    this.setRewardInfo();
  }

  setCountries() {
    this.showLoader = true;
    this.subscriptions.add(
      this.projectService.getCountries().subscribe(
        (reply: ReplyMessage<LookupItem[]>) => {
          if (reply.value) {
            this.reward.deliveryCountries.forEach(x => {
              const country = reply.value.find(c => c.key === x.key);
              if (country) {
                this.countrySelect.list.push(new SelectItem(country.key, country.value));
              }
            });
          }
          this.showLoader = false;
        },
        () => {this.showLoader = false; }
      )
    );
  }

  setRewardInfo() {
    this.showLoader = true;
    this.subscriptions.add(
      this.rewardService.getPublicById(this.rewardId).subscribe(
        (reply: ReplyMessage<RewardInfo>) => {
          this.reward = reply.value;
          this.setCountries();
          this.showLoader = false;
        },
        () => { this.showLoader = false; }
      )
    );
  }

  hasDelivery(): boolean {
    return this.reward.deliveryType === DeliveryTypeEnum.SomeCountries
      || this.reward.deliveryType === DeliveryTypeEnum.WholeWorld;
  }

  getDeliveryPrice(): number {
    if (this.currentCountryId) {
      const delivery = this.reward.deliveryCountries.find(x => x.key === this.currentCountryId);
      return delivery ? delivery.value : null;
    } else {
      return null;
    }
  }

  getTotal() {
    let result: number = this.reward.price * (this.countInput.value ? this.countInput.value : 1);
    if (this.currentCountryId && this.hasDelivery()) {
      result += this.getDeliveryPrice();
      return result;
    } else if (this.hasDelivery()) {
      return 'Выберите страну доставки';
    } else {
      return result;
    }
  }

  getCard(reward: RewardInfo): RewardCard {
    return new RewardCard(
      reward.title,
      reward.price,
      reward.description,
      reward.image,
      reward.deliveryType,
      reward.deliveryDate,
      reward.limit,
      reward.id
    );
  }

  onContinueClick() {
    this.showLoader = true;
    const request: AcceptOrderRequestMessage = {
      rewardId: this.rewardId,
      count: this.countInput.value,
      surname: this.surnameInput.value,
      name: this.nameInput.value,
      middleName: this.middleNameInput.value,
      countryId: this.currentCountryId,
      fullAddress: this.fullAddressInput.value,
      postCode: this.postIndexInput.value
    };
    this.subscriptions.add(
      this.orderService.accept(request).subscribe(
        () => {
          this.showLoader = false;
          this.redirect(Routes.project + '/' + this.reward.projectId);
        },
        () => { this.showLoader = false; }
      )
    );
  }

  onSelectValue(value: string) {
    this.currentCountryId = value;
  }
}