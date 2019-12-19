import {NgbDate, NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';
import {Customer} from './customer';

export class Order {
  constructor(c: Customer, today: NgbDate, model: NgbDateStruct, location: string) {
    this.customer = c;
    this.dateWhenOrderWasMade = today;
    this.dateForOrderToBeCompleted = new NgbDate(model.year, model.month, model.day);
    this.location = location;
  }

  dateWhenOrderWasMade: NgbDate;
  dateForOrderToBeCompleted: NgbDate;
  customer: Customer;
  location: string;
  type: string;
}
