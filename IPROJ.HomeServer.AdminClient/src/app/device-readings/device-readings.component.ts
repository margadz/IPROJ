import {Component, OnInit, Input } from '@angular/core';
import { DeviceReading} from '../deviceReading';
import { DeviceReadingService } from '../device-reading.service';
import {ActivatedRoute} from '@angular/router';
import {NgbDateStruct, NgbCalendar} from '@ng-bootstrap/ng-bootstrap';
import {Observable} from 'rxjs/Observable';
import {ReplaySubject} from 'rxjs/ReplaySubject';
import { Device } from '../device';
import {SettingsService} from '../settings.service';

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
  private fromDate: NgbDateStruct;
  private toDate: NgbDateStruct;
  private totalConsumptionSubject: ReplaySubject<string>;
  private selectedReadingSubject: ReplaySubject<DeviceReading[]>;
  private selectedReadings: DeviceReading[];
  @Input() device: Device;
  readings:  DeviceReading[];
  hoveredDate: NgbDateStruct;

  constructor(
    private deviceReadingService: DeviceReadingService,
    calendar: NgbCalendar,
    private settings: SettingsService) {
    this.fromDate = calendar.getToday();
    this.toDate = calendar.getNext(calendar.getToday(), 'd', 10);
    this.totalConsumptionSubject = new ReplaySubject<string>();
    this.selectedReadingSubject = new ReplaySubject<DeviceReading[]>();
    this.selectedReadings = [];
  }

  private normalizeDate(date: Date): Date {
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
      this._total$ = this.totalConsumptionSubject.asObservable();
    }
    return this._total$;
  }

  totalCost(value: string): string {
    return (Number(value) * this.settings.powerCost).toPrecision(3).toString();
  }

  get selectedReadings$(): Observable<DeviceReading[]> {
    if (!this._selectedReadings$) {
      this._selectedReadings$ = this.selectedReadingSubject.asObservable();
    }
    return this._selectedReadings$;
  }

  private getReadings() {
    this.deviceReadingService.getReadingFor(this.device.deviceId).then(result => this.readings = result.sort((a, b) => {
      if (a.readingTimeStamp > b.readingTimeStamp) { return -1; }
      if (a.readingTimeStamp === b.readingTimeStamp) { return 0; }
      if (a.readingTimeStamp < b.readingTimeStamp) { return 1; } }));
  }

  private hasDayReading(date: NgbDateStruct): boolean {
    const newDate = this.normalizeDate(this.getDate(date));
    const index = this.readings.findIndex(reading => this.normalizeDate(reading.readingTimeStamp).getTime() === newDate.getTime());
    return index > 0;
  }

  private getTotal(): number {
    let result = 0;
    const fromDate = this.normalizeNgDate(this.fromDate);
    const toDate = this.normalizeNgDate(this.toDate);
    for (const reading of this.readings) {
      const date = this.normalizeDate(reading.readingTimeStamp);
      if (date >= fromDate && date <= toDate) {
        result += reading.value;
        this.selectedReadings.push(reading);
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
    if (this.fromDate && this.toDate) {
      this.selectedReadings = [];
      this.totalConsumptionSubject.next(this.getTotal().toPrecision(4));
      this.selectedReadingSubject.next(this.selectedReadings);
    }
  }

  isHovered = date => this.fromDate && !this.toDate && this.hoveredDate && after(date, this.fromDate) && before(date, this.hoveredDate);
  isInside = date => after(date, this.fromDate) && before(date, this.toDate);
  isFrom = date => equals(date, this.fromDate);
  isTo = date => equals(date, this.toDate);
}


