import { Component, OnInit } from '@angular/core';
import { DeviceService } from '../device.service';
import { Device } from '../device';

@Component({
  selector: 'app-devices',
  templateUrl: './devices.component.html',
  styleUrls: ['./devices.component.scss']
})

export class DevicesComponent implements OnInit {
  devices: Device[];
  constructor(private deviceService: DeviceService) { }

  ngOnInit() {
    this.getDevices();
  }

  getDevices() {
    this.deviceService.getDevices().then(result => this.devices = result);
  }
}
