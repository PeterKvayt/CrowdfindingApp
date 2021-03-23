import { GenericLookupItem } from '../../common/GenericLookupItem';
import { DeliveryTypeEnum } from '../../enums/DeliveryTypeEnum';
import { RewardCard } from 'src/app/components/reward-card/RewardCard';

export class RewardInfo {
  public id: string;
  public projectId: string;
  public title: string;
  public price: number;
  public description: string;
  public deliveryDate: Date;
  public isLimited: boolean;
  public limit: number;
  public image: string;
  public deliveryType: DeliveryTypeEnum;
  public deliveryCountries: GenericLookupItem<string, number>[];

  public getCard = function() {
    return new RewardCard(
      this.title,
      this.price,
      this.description,
      this.image,
      this.deliveryType,
      this.deliveryDate,
      this.limit
    );
  }
  // getCard(): RewardCard {
  //   return new RewardCard(
  //     this.title,
  //     this.price,
  //     this.description,
  //     this.image,
  //     this.deliveryType,
  //     this.deliveryDate,
  //     this.limit
  //   );
  // }
}
