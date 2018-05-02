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
  styleUrls: ['./device-readings.component.scss']
})
export class DeviceReadingsComponent implements OnInit {
  private _total$: Observable<string>;
  private _selectedReadings$: Observable<DeviceReading[]>;
  private _fromDate: NgbDateStruct;
  private _toDate: NgbDateStruct;
  private _totalConsumptionSubject: ReplaySubject<string>;
  private _selectedReadingSubject: ReplaySubject<DeviceReading[]>;
  private _selectedReadings: DeviceReading[];
  readings:  DeviceReading[];
  hoveredDate: NgbDateStruct;

  constructor(
    private deviceReadingService: DeviceReadingService,
    private route: ActivatedRoute,
    calendar: NgbCalendar) {
    this._fromDate = calendar.getToday();
    this._toDate = calendar.getNext(calendar.getToday(), 'd', 10);
    this._totalConsumptionSubject = new ReplaySubject<string>();
    this._selectedReadingSubject = new ReplaySubject<DeviceReading[]>();
    this._selectedReadings = [];
  }

  private n: number = 0;

  private normalizeDate(date: Date): Date{
    const newDate = new Date(date);
    newDate.setHours(0, 0, 0, 0);
    return newDate;
  }

  private getDate(date: NgbDateStruct): Date {
    return new Date(date.year + '-' + date.month + '-' + date.day);
  }

  private normalizeNgDate(date: NgbDateStruct): Date {
      let newDate = this.getDate(date);
      newDate = this.normalizeDate(newDate);
      return newDate;
  }

  ngOnInit() {
    this.getReadings();
  }

  get total$(): Observable<string> {
    if (!this._total$) {
      this._total$ = this._totalConsumptionSubject.asObservable();
    }
    return this._total$;
  }

  get selectedReadings$(): Observable<DeviceReading[]> {
    if (!this._selectedReadings$) {
      this._selectedReadings$ = this._selectedReadingSubject.asObservable();
    }
    return this._selectedReadings$;
  }

  getReadings() {
    const id = this.route.snapshot.paramMap.get('id');
    this.deviceReadingService.getReadingFor(id).then(result => this.readings = result.sort((a, b) => {
      if (a.readingTimeStamp > b.readingTimeStamp) { return -1; }
      if (a.readingTimeStamp === b.readingTimeStamp) { return 0; }
      if (a.readingTimeStamp < b.readingTimeStamp) { return 1; } }));
  }

  hasDayReading(date: NgbDateStruct): boolean {
    const newDate = this.normalizeDate(this.getDate(date));
    const index = this.readings.findIndex(reading => this.normalizeDate(reading.readingTimeStamp).getTime() === newDate.getTime());
    return index > 0;
  }

  getTotal(): number {
    let result = 0;
    const fromDate = this.normalizeNgDate(this._fromDate);
    const toDate = this.normalizeNgDate(this._toDate);
    for (const reading of this.readings) {
      const date = this.normalizeDate(reading.readingTimeStamp);
      if (date >= fromDate && date <= toDate) {
        result += reading.value;
        this._selectedReadings.push(reading);
      }
    }
    return result;
  }

  onDateSelection(date: NgbDateStruct) {
    if (!this._fromDate && !this._toDate) {
      this._fromDate = date;
    } else if (this._fromDate && !this._toDate && after(date, this._fromDate)) {
      this._toDate = date;
    } else {
      this._toDate = null;
      this._fromDate = date;
    }
    if (this._fromDate && this._toDate) {
      this._selectedReadings = [];
      this._totalConsumptionSubject.next(this.getTotal().toPrecision(4));
      this._selectedReadingSubject.next(this._selectedReadings);
    }
  }

  isHovered = date => this._fromDate && !this._toDate && this.hoveredDate && after(date, this._fromDate) && before(date, this.hoveredDate);
  isInside = date => after(date, this._fromDate) && before(date, this._toDate);
  isFrom = date => equals(date, this._fromDate);
  isTo = date => equals(date, this._toDate);

  func(): void {
    console.log(5);
  }
}


