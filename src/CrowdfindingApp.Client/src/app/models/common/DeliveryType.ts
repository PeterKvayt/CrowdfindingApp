import { LookupItem } from './LookupItem';

export class DeliveryTypes {
  static withoutDelivery = new LookupItem('withoutDelivery', 'Доставка отсутствует');
  static someCountries = new LookupItem('someCountries', 'Некоторые страны');
  static wholeWorld = new LookupItem('wholeWorld', 'Весь мир');
}
