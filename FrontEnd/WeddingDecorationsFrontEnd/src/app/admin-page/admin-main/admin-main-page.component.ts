import { Component, OnInit } from '@angular/core';

import { Order } from '../../shared/models/order';

import { AdminViewComponent } from '../../admin-page/admin-components/admin-view/admin-view.component';

import { OrderService } from '../../shared/services/admin-services/order.service';
import { Observable } from 'rxjs';

import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-admin-main-page',
  templateUrl: './admin-main-page.component.html',
  providers: [OrderService,AdminViewComponent],
  styleUrls: ['./admin-main-page.component.scss']
})
export class AdminMainComponent {
  orders$: Observable<Order[]>;
  collectionSize$ : Observable<number>;

  closeResult: string;

  modalContent:undefined

constructor(public orderService: OrderService,private modalService: NgbModal , public adVW :AdminViewComponent){
  this.orders$ = orderService.orders$;
  this.collectionSize$ = orderService.collectionSize$;
}
  deleteOrder(order : number) {
    // update current page of items
    console.log(order);
    this.orderService.deleteOrder(order).subscribe((data)=>{
      console.log("success");
    });
  }

  modalOrderView(content,order) {
    this.modalContent=order;
    console.log(order);
    this.modalService.open(content ).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return  `with: ${reason}`;
    }
  }
}
