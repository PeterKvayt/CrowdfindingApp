import { LookupItem } from '../common/LookupItem';
import { DeliveryTypeEnum } from '../enums/DeliveryTypeEnum';

export class DeliveryTypes {
  static withoutDelivery = new LookupItem(DeliveryTypeEnum.WithoutDelivery.toString(), 'Доставка отсутствует');
  static someCountries = new LookupItem('SomeCountries', 'Некоторые страны');
  static wholeWorld = new LookupItem('WholeWorld', 'Весь мир');
}
