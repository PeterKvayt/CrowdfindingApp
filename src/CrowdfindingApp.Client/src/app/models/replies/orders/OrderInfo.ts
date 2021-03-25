import { OrderStatusEnum } from '../../enums/OrderStatusEnum';

export class OrderInfo {
  public id: string;
  public userId: string;
  public rewardId: string;
  public count: number;
  public isPrivate: boolean;
  public paymentMethod: number;
  public status: OrderStatusEnum;
  public paymentDateTime: Date;
  public surname: string;
  public name: string;
  public middleName: string;
  public countryId: string;
  public fullAddress: string;
  public postCode: string;
  public projectName: string;
  public projectId: string;
  public rewardName: string;
  public countryName: string;
  public total: number;
  public deliveryCost: number;
}
