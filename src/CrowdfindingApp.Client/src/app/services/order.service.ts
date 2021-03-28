import { HttpService } from './http.service';
import { Injectable } from '@angular/core';
import { AcceptOrderRequestMessage } from '../models/requests/orders/AcceptorderRequestMessage';

@Injectable()
export class OrderService {
  constructor(
    private http: HttpService
  ) { }

  private controller = 'orders/';

  public accept(model: AcceptOrderRequestMessage) {
    return this.http.post<AcceptOrderRequestMessage>(this.controller, model);
  }

  public getOrders() {
    return this.http.get(this.controller);
  }

  public getProjectOrders(id: string) {
    return this.http.get(this.controller + 'projectOrders/' + id);
  }
}
