import { Component, Input, OnInit} from '@angular/core';
import { Device} from '../device';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { DeviceService } from '../device.service';

@Component({
  selector: 'app-device-detail',
  templateUrl: './device-detail.component.html',
  styleUrls: ['./device-detail.component.css']
})
export class DeviceDetailComponent implements OnInit {
  device: Device;
  constructor(
    private route: ActivatedRoute,
    private deviceService: DeviceService,
    private location: Location,
  ) { }

  ngOnInit() {
    this.getDevice();
  }

  getDevice(): void {
    const id  = this.route.snapshot.paramMap.get('id');
    this.deviceService.getDevice(id).then(result => this.device = result);
  }

  goBack(): void {
    this.location.back();
  }
}
