import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Device } from '../device';
import {DeviceService} from '../device.service';

@Component({
  selector: 'app-device-template',
  templateUrl: './device-template.component.html',
  styleUrls: ['./device-template.component.scss']
})
export class DeviceTemplateComponent implements OnInit {
  @Input() device: Device;
  @Input() isNew = false;
  @Input() isBlank = false;
  @ViewChild('saveModal') modal: any;

  constructor(private deviceService: DeviceService) { }

  ngOnInit() {
    if (this.isNew && this.isBlank) {
      this.device = new Device();
      this.device.typeOfReading = 'PowerConsumption';
    }
    if (this.device.deviceId === '00000000-0000-0000-0000-000000000000') {
        this.device.deviceId = undefined;
    }
  }

  get deviceType(): string {
    if (this.device.typeOfDevice === undefined) {
      return '';
    }
    return this.device.typeOfDevice.toString();
  }

  async updateDevice(): Promise<any> {
    await this.deviceService.addDevice(this.device);
  }

  isValid(): boolean {
    return this.device.typeOfDevice !== undefined
      && this.device.name !== null
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
