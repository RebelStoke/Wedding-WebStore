import {Component, OnInit} from '@angular/core';
import {NgbCalendar, NgbDate, NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';
import {BookingService} from './booking.service';
import {Order} from './order';
import {Customer} from './customer';


@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.scss']
})
export class BookingComponent implements OnInit {

  model: NgbDateStruct;
  date1: NgbDate;
  ordersInMonth: Order[];

  constructor(private calendar: NgbCalendar, private bookingService: BookingService) {
  }

  selectToday() {
    this.model = this.calendar.getToday();
  }


  ngOnInit() {
    this.date1 = this.calendar.getToday();
    this.bookingService.getDatesOfMonth(this.date1).subscribe(orders => this.ordersInMonth = orders);
  }

  isDayReserved(date) {
    for (const d of this.ordersInMonth) {
      if (d.dateForOrderToBeCompleted.day === date.day && d.dateForOrderToBeCompleted.month === date.month && d.type === 'Pending') {
        return true;
      }
    }
    return false;
  }

  isDayUnavailable(date) {
    for (const d of this.ordersInMonth) {
      if (d.dateForOrderToBeCompleted.day === date.day && d.dateForOrderToBeCompleted.month === date.month && d.type === 'Rejected') {
        return true;
      }
    }
    return false;
  }

  changeMonth(next: { year: number; month: number }) {
    this.date1.month = next.month;
    this.date1.year = next.year;
    this.bookingService.getDatesOfMonth(this.date1).subscribe(orders => this.ordersInMonth = orders);
  }

  createOrder(name, email, place, phone) {
    if (this.model != null) {
    const c = new Customer(name, email, phone);
    const o = new Order(c, this.calendar.getToday(), this.model, place);
    this.bookingService.createOrder(o).subscribe();
    }

  }
}
