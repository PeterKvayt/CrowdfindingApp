export class AcceptOrderRequestMessage {
  public rewardId: string;
  public count: number;
  public surname: string;
  public name: string;
  public middleName: string;
  public countryId: string;
  public fullAddress: string;
  public postCode: string;
  public payCardNumber: string;
  public payCardOwnerName: string;
  public payCardCvv: string;
  public payCardExpirationDate: Date;
}
