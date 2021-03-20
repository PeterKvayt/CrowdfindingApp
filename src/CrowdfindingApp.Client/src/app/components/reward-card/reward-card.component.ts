import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { RewardCard } from './RewardCard';
import { DeliveryTypeEnum } from 'src/app/models/enums/DeliveryTypeEnum';
import { FileService } from 'src/app/services/file.service';

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
  public imageUrl: string = 'assets/img/stock-reward.jpg';

  constructor(
    private fileService: FileService
  ) { }
  ngOnInit() {
    this.setImageUrl();;
    this.deliveryTypeName = this.getTypeName();
  }

  setImageUrl(): void {
    if (this.item.image) {
      console.log(this.item.image !== this.imageUrl)
      console.log(this.item.image)
      console.log(this.imageUrl)
      if (this.item.image !== this.imageUrl) {
        this.imageUrl = this.fileService.absoluteFileStoragePath + this.item.image;
      }
    }
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
