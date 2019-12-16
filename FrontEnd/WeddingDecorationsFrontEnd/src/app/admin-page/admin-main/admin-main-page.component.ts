import { Component, OnInit } from '@angular/core';
import { Order } from '../../shared/models/order';

import { OrderService } from '../../shared/services/admin-services/order.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-admin-main-page',
  templateUrl: './admin-main-page.component.html',
  providers: [OrderService],
  styleUrls: ['./admin-main-page.component.scss']
})
export class AdminMainComponent {
  orders$: Observable<Order[]>;
  collectionSize$ : Observable<number>;

constructor(public orderService: OrderService){
  this.orders$ = orderService.orders$;
  this.collectionSize$ = orderService.collectionSize$;
}
  deleteOrder(order : number) {
    // update current page of items
    console.log(order);
    this.orderService.deleteOrder(order).subscribe((data)=>{
      console.log("success");
    });;
  }
}
