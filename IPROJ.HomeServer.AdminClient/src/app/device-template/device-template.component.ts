import { Component, Input, OnInit } from '@angular/core';
import { Device } from '../device';
import {DeviceService} from '../device.service';

@Component({
  selector: 'app-device-template',
  templateUrl: './device-template.component.html',
  styleUrls: ['./device-template.component.scss']
})
export class DeviceTemplateComponent implements OnInit {
  @Input() device: Device;
  isNew: boolean;

  constructor(private deviceService: DeviceService) { }

  ngOnInit() {
    if (this.device === undefined) {
      this.device = new Device();
      this.device.typeOfReading = 'PowerConsumption';
      this.isNew = true;
      }
  }

  get deviceType(): string {
    if (this.device.typeOfDevice === undefined) {
      return '';
    }
    return this.device.typeOfDevice.toString();
  }

  updateDevice(): void {
    this.deviceService.addDevice(this.device).then(() => console.log('done'));
  }

  isValid(): boolean {
    return this.device.typeOfDevice !== undefined
      && this.device.name.length > 0
      && this.device.name.length < 25
      && this.isHostValid(this.device.host);
  }

  private isHostValid(host: string): boolean {
    const parts = host.split(':');
    const ip = parts[0].split('.');
    const port = parts[1];
    return this.isNumberValid(port, 1, 65535) &&
      ip.length === 4 &&
      ip.every((segment) => this.isNumberValid(segment, 0, 255));
  }

  private isNumberValid(value: string, min: number, max: number): boolean {
    const number = Number(value);
    return number >= min && number <= max;
  }
}
