import { Component, OnInit } from '@angular/core';
import {NgbCalendar, NgbDate, NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.scss']
})
export class BookingComponent implements OnInit {

  model: NgbDateStruct;
  date: {year: number, month: number};
  date1: NgbDate = this.calendar.getToday();

  constructor(private calendar: NgbCalendar) {
  }

  selectToday() {
    this.model = this.calendar.getToday();
  }


  ngOnInit() {
  }

  onDateSelect($event: NgbDate) {
    this.date1 = $event;
  }
}
