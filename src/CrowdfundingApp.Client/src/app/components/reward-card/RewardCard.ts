import { DeliveryTypeEnum } from 'src/app/models/enums/DeliveryTypeEnum';

export class RewardCard {
  constructor(
    public name: string,
    public price: number,
    public description: string,
    public image: string,
    public deliveryType: DeliveryTypeEnum,
    public deliveryDate: Date,
    public isLimited: boolean,
    public availableCount?: number,
    public id?: string
  ) { }
}
