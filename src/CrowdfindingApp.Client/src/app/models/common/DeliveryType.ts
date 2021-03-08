import { LookupItem } from './LookupItem';

export class DeliveryTypes {
  static withoutDelivery = new LookupItem('Доставка отсутствует', 'withoutDelivery');
  static someCountries = new LookupItem('Некоторые страны', 'someCountries');
  static wholeWorld = new LookupItem('Весь мир', 'wholeWorld');
}
