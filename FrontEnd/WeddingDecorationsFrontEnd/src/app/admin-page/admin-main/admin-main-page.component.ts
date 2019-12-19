import { Component, OnInit } from '@angular/core';

import { Order } from '../../shared/models/order';

import { OrderService } from '../../shared/services/admin-services/order.service';
import { Observable } from 'rxjs';

import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-admin-main-page',
  templateUrl: './admin-main-page.component.html',
  providers: [OrderService],
  styleUrls: ['./admin-main-page.component.scss']
})
export class AdminMainComponent implements OnInit{
  orders$: Observable<Order[]>;
  collectionSize$ : Observable<number>;

  insertForm: FormGroup;

  closeResult: string;

  modalContent:undefined

  currentType:string;


constructor(public orderService: OrderService,private modalService: NgbModal  , private formBuilder: FormBuilder){
  this.orders$ = orderService.orders$;
  this.collectionSize$ = orderService.collectionSize$;

  this.insertForm = this.formBuilder.group({
    OrderM: ['', Validators.required],
    OrderC: ['', Validators.required],
    OrderL: ['', Validators.required],
    OrderT: ['', Validators.required],
    CustN: ['', Validators.required],
    CustE: ['', Validators.required],
    CustP: ['', Validators.required]
  });
}

  ngOnInit() {
  }

  get DateMade() { return this.insertForm.get('OrderM'); }
  get DateCreate() { return this.insertForm.get('OrderC'); }
  get Location() { return this.insertForm.get('OrderL'); }
  get Type() { return this.insertForm.get('OrderT'); }

  get CustomerName() { return this.insertForm.get('CustN'); }
  get CustomerEmail() { return this.insertForm.get('CustE'); }
  get CustomerPhone() { return this.insertForm.get('CustP'); }

  deleteOrder(order : number) {
    // update current page of items
    this.orderService.deleteOrder(order).subscribe((data)=>{
      console.log(data);
    });
  }

  modalOrderView(content,order) {
  this.modalContent=order;
    this.modalService.open(content).result.then((result) => {
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

  editInfo() {
    console.log(this.DateMade.value, this.DateCreate.value, this.Location.value, this.Type.value, this.CustomerName.value, this.CustomerEmail.value, this.CustomerPhone.value)
  }

}
