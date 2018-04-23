import { Component, OnInit } from '@angular/core';
import {Device} from '../device';
import {DeviceService} from '../device.service';
import {CurrentMeasurementService} from '../current-measurement.service';
import {Observable} from 'rxjs/Observable';
import {DeviceReading} from '../deviceReading';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  devices: Device[];
  constructor(
    private deviceService: DeviceService) { }

  ngOnInit() {
    this.getDevices();
  }

  getDevices() {
    this.deviceService.getDevices().then(result => this.devices = result);
  }
}
