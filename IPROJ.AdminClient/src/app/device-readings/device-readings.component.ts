import {Component, OnInit } from '@angular/core';
import { DeviceReading} from '../deviceReading';
import { DeviceReadingService } from '../device-reading.service';
import {ActivatedRoute} from '@angular/router';
import {NgbDateStruct, NgbCalendar} from '@ng-bootstrap/ng-bootstrap';
import {Observable} from 'rxjs/Observable';
import {ReplaySubject} from 'rxjs/ReplaySubject';

const equals = (one: NgbDateStruct, two: NgbDateStruct) =>
  one && two && two.year === one.year && two.month === one.month && two.day === one.day;

const before = (one: NgbDateStruct, two: NgbDateStruct) =>
  !one || !two ? false : one.year === two.year ? one.month === two.month ? one.day === two.day
    ? false : one.day < two.day : one.month < two.month : one.year < two.year;

const after = (one: NgbDateStruct, two: NgbDateStruct) =>
  !one || !two ? false : one.year === two.year ? one.month === two.month ? one.day === two.day
    ? false : one.day > two.day : one.month > two.month : one.year > two.year;

@Component({
  selector: 'app-device-readings',
  templateUrl: './device-readings.component.html',
  styleUrls: ['./device-readings.component.css']
})
export class DeviceReadingsComponent implements OnInit {
  private _total$: Observable<number>;
  readings:  DeviceReading[];
  hoveredDate: NgbDateStruct;
  fromDate: NgbDateStruct;
  toDate: NgbDateStruct;
  subject: ReplaySubject<number>;

  constructor(
    private deviceReadingService: DeviceReadingService,
    private route: ActivatedRoute,
    calendar: NgbCalendar) {
    this.fromDate = calendar.getToday();
    this.toDate = calendar.getNext(calendar.getToday(), 'd', 10);
    this.subject = new ReplaySubject<number>();
  }

  ngOnInit() {
    this.getReadings();
  }

  get total$(): Observable<number> {
    if (!this._total$) {
      this._total$ = this.subject.asObservable();
    }
    return this._total$;
  }

  getReadings() {
    const id = this.route.snapshot.paramMap.get('id');
    this.deviceReadingService.getReadingFor(id).then(result => this.readings = result);
  }


  getTotal(): number {
    let result = 0;
    const fromDate = this.getDate(this.fromDate);
    fromDate.setHours(0,0,0,0);
    const toDate = this.getDate(this.toDate);
    toDate.setHours(0,0,0,0);
    for (const reading of this.readings) {
      const date = new Date(reading.readingTimeStamp);
      date.setHours(0,0,0,0);
      if (date >= fromDate && date <= toDate) {
        result += reading.value;
      }
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
    if (this.fromDate && this.toDate){
      this.subject.next(this.getTotal());
    }
  }

  getDate(date: NgbDateStruct): Date{
    return new Date(date.year + '-' + date.month + '-' + date.day);
  }

  isHovered = date => this.fromDate && !this.toDate && this.hoveredDate && after(date, this.fromDate) && before(date, this.hoveredDate);
  isInside = date => after(date, this.fromDate) && before(date, this.toDate);
  isFrom = date => equals(date, this.fromDate);
  isTo = date => equals(date, this.toDate);
}


