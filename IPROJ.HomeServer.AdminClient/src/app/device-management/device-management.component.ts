import { Component, OnInit } from '@angular/core';
import {DeviceService} from '../device.service';
import {Device} from '../device';
import { Observable } from 'rxjs/Observable';
import {SignalRMessengerService} from '../signal-r-messenger.service';

@Component({
  selector: 'app-device-management',
  templateUrl: './device-management.component.html',
  styleUrls: ['./device-management.component.scss']
})
export class DeviceManagementComponent implements OnInit {
  devices: Device[];
  constructor(private deviceService: DeviceService,
              private signalRMessenger: SignalRMessengerService) { }

  ngOnInit() {
    this.getDevices();
  }

  getDevices() {
    this.deviceService.getDevices()
      .then(result => this.devices = result);
  }

  async sendRequest() {
    console.log('clicked');
    await this.signalRMessenger.requestDeviceDiscovery();
    console.log('fnished');
  }

  getNewDevices(): Observable<Device[]> {
    return this.signalRMessenger.Devices;
  }
}
