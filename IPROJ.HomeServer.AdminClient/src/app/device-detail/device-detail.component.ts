import { Component, Input, OnInit} from '@angular/core';
import { Device} from '../device';
import {CurrentMeasurementService} from '../current-measurement.service';
import {Observable} from 'rxjs/Observable';
import {DeviceReading} from '../deviceReading';

@Component({
  selector: 'app-device-detail',
  templateUrl: './device-detail.component.html',
  styleUrls: ['./device-detail.component.scss']
})
export class DeviceDetailComponent implements OnInit {
  @Input() device: Device;
  constructor(
    private currentService: CurrentMeasurementService) { }

  ngOnInit() {
  }

  getCurrentMeasurement(deviceId: string): Observable<DeviceReading> {
    return this.currentService.getCurrentReadings(deviceId);
  }
}
