import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { RewardCard } from './RewardCard';
import { DeliveryTypeEnum } from 'src/app/models/enums/DeliveryTypeEnum';

@Component({
  selector: 'app-reward-card',
  templateUrl: './reward-card.component.html',
  styleUrls: ['./reward-card.component.css']
})
export class RewardCardComponent implements OnInit {

  @Input() item: RewardCard;
  @Input() editable: boolean;

  @Output() changeClick = new EventEmitter();
  @Output() deleteClick = new EventEmitter();

  public deliveryTypeName: string;
  constructor() { }
  ngOnInit() {
    this.deliveryTypeName = this.getTypeName();
  }

  onChangeClick() {
    this.changeClick.emit();
  }

  onDeleteClick() {
    this.deleteClick.emit();
  }

  getTypeName(): string {
    switch (this.item.deliveryType) {
      case <number>DeliveryTypeEnum.SomeCountries: return 'Некоторые страны';
      case <number>DeliveryTypeEnum.WholeWorld: return 'Весь мир';
      case <number>DeliveryTypeEnum.WithoutDelivery: return 'Доставка отсутствует';
      default: return '';
    }
  }
}
