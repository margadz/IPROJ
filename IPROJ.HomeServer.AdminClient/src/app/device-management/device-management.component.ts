import { Component, OnInit } from '@angular/core';
import {DeviceService} from '../device.service';
import {Device} from '../device';

@Component({
  selector: 'app-device-management',
  templateUrl: './device-management.component.html',
  styleUrls: ['./device-management.component.scss']
})
export class DeviceManagementComponent implements OnInit {
  devices: Device[];
  constructor(private deviceService: DeviceService) { }

  ngOnInit() {
    this.getDevices();
  }

  getDevices() {
    this.deviceService.getDevices()
      .then(result => this.devices = result);
  }
}
