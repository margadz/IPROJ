import {Component, OnInit, Input, Inject} from '@angular/core';
import { DeviceReading} from '../deviceReading';
import { DeviceReadingService } from '../device-reading.service';
import {ActivatedRoute} from '@angular/router';
import {NgbDateStruct, NgbCalendar} from '@ng-bootstrap/ng-bootstrap';
import {Observable} from 'rxjs/Observable';

const equals = (one: NgbDateStruct, two: NgbDateStruct) =>
  one && two && two.year === one.year && two.month === one.month && two.day === one.day;

const before = (one: NgbDateStruct, two: NgbDateStruct) =>
  !one || !two ? false : one.year === two.year ? one.month === two.month ? one.day === two.day
    ? false : one.day < two.day : one.month < two.month : one.year < two.year;

const after = (one: NgbDateStruct, two: NgbDateStruct) =>
  !one || !two ? false : one.year === two.year ? one.month === two.month ? one.day === two.day
    ? false : one.day > two.day : one.month > two.month : one.year > two.year;

const equalsDate = (one: NgbDateStruct, two: Date) => one && two && two.getFullYear() === one.year && two.getMonth() === one.month && two.getDay() === one.day;

const beforeDate = (one: NgbDateStruct, two: Date) =>
  !one || !two ? false : one.year === two.getFullYear() ? one.month === two.getMonth() ? one.day === two.getDay()
    ? false : one.day < two.getDay() : one.month < two.getMonth() : one.year < two.getFullYear();

const afterDate = (one: NgbDateStruct, two: Date) =>
  !one || !two ? false : one.year === two.getFullYear() ? one.month === two.getMonth() ? one.day === two.getDay()
    ? false : one.day > two.getDay() : one.month > two.getMonth() : one.year > two.getFullYear();

@Component({
  selector: 'app-device-readings',
  templateUrl: './device-readings.component.html',
  styleUrls: ['./device-readings.component.css']
})
export class DeviceReadingsComponent implements OnInit {
  readings:  DeviceReading[];
  hoveredDate: NgbDateStruct;
  fromDate: NgbDateStruct;
  toDate: NgbDateStruct;

  constructor(
    private deviceReadingService: DeviceReadingService,
    private route: ActivatedRoute,
    calendar: NgbCalendar) {
    this.fromDate = calendar.getToday();
    this.toDate = calendar.getNext(calendar.getToday(), 'd', 10);
  }

  ngOnInit() {
    this.getReadings();
  }

  getReadings() {
    const id = this.route.snapshot.paramMap.get('id');
    this.deviceReadingService.getReadingFor(id).then(result => this.readings = result);
  }

  getDate(reading: DeviceReading): string{
    return reading.readingTimeStamp.getDate().toString();
  }

  getTotal(): Observable<number>{
    let result = 0;
    for (const reading of this.readings){
      const date = new Date(reading.readingTimeStamp);
      /*if ((equalsDate(this.fromDate, date) || afterDate(this.fromDate, date)) &&
        (equalsDate(this.fromDate, date) || beforeDate(this.fromDate, date))) {*/
         result += reading.value;
    }
    return result;
  }

  onDateSelection(date: NgbDateStruct) {
    if (!this.fromDate && !this.toDate) {
      this.fromDate = date;
    } else if (this.fromDate && !this.toDate && after(date, this.fromDate)) {
      this.toDate = date;
    } else {
      this.toDate = null;
      this.fromDate = date;
    }
  }

  isHovered = date => this.fromDate && !this.toDate && this.hoveredDate && after(date, this.fromDate) && before(date, this.hoveredDate);
  isInside = date => after(date, this.fromDate) && before(date, this.toDate);
  isFrom = date => equals(date, this.fromDate);
  isTo = date => equals(date, this.toDate);
}


