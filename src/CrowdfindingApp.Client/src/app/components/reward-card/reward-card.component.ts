import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { RewardCard } from './RewardCard';
import { DeliveryTypeEnum } from 'src/app/models/enums/DeliveryTypeEnum';
import { FileService } from 'src/app/services/file.service';
import { Routes } from 'src/app/models/immutable/Routes';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-reward-card',
  templateUrl: './reward-card.component.html',
  styleUrls: ['./reward-card.component.css']
})
export class RewardCardComponent implements OnInit {

  @Input() item: RewardCard;
  @Input() editable: boolean;
  @Input() showBuyOption: boolean;

  @Output() changeClick = new EventEmitter();
  @Output() deleteClick = new EventEmitter();
  @Output() buyClick = new EventEmitter();

  public deliveryTypeName: string;
  public imageUrl = 'assets/img/stock-reward.jpg';

  constructor(
    private fileService: FileService,
    private router: Router,
    public authService: AuthenticationService
  ) { }
  ngOnInit() {
    this.setImageUrl();;
    this.deliveryTypeName = this.getTypeName();
  }

  setImageUrl(): void {
    if (this.item.image) {
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

  onBuyClick() {
    if (this.item.id) {
      this.router.navigate([Routes.order + '/' + this.item.id]);
    }
  }

  hasBuyOption(): boolean {
    if (this.showBuyOption && this.authService.isAuthenticated()) {
      if (this.item.isLimited) {
        return this.item.availableCount > 0;
      } else {
        return true;
      }
    } else {
      return false;
    }
  }
}
