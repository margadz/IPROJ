import { Component, OnInit } from '@angular/core';
import {Device} from '../device';
import {DeviceService} from '../device.service';

@Component({
  selector: 'app-readings-tab',
  templateUrl: './readings-tab.component.html',
  styleUrls: ['./readings-tab.component.scss']
})
export class ReadingsTabComponent implements OnInit {
  devices: Device[];
  constructor(private deviceService: DeviceService) { }

  ngOnInit() {
    this.getDevices();
  }

  getDevices() {
    this.deviceService.getDevices()
      .then(result => this.devices = result.filter(device => device.isActive === true));
  }
}
