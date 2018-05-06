import { Component, Input, OnInit} from '@angular/core';
import { Device} from '../device';
import { Observable } from 'rxjs/Observable';
import { DeviceReading } from '../deviceReading';
import {SignalRMessengerService} from '../signal-r-messenger.service';

@Component({
  selector: 'app-device-detail',
  templateUrl: './device-detail.component.html',
  styleUrls: ['./device-detail.component.scss']
})
export class DeviceDetailComponent implements OnInit {
  @Input() device: Device;
  constructor(private signalRMessenger: SignalRMessengerService) { }

  ngOnInit() {
  }

  getCurrentMeasurement(deviceId: string): Observable<DeviceReading> {
    return this.signalRMessenger.getCurrentReadings(deviceId);
  }

  get deviceType(): string {
    return this.device.typeOfDevice.toString();
  }
}
