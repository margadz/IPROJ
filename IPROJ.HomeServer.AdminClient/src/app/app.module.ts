import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { DevicesComponent } from './devices/devices.component';
import { DeviceService } from './device.service';
import { DeviceDetailComponent } from './device-detail/device-detail.component';
import { MessageService } from './message.service';
import { MessagesComponent } from './messages/messages.component';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './/app-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DeviceReadingsComponent } from './device-readings/device-readings.component';
import { DeviceReadingService } from './device-reading.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { GraphComponent } from './graph/graph.component';
import {CurrentMeasurementService} from './current-measurement.service';
import { AddingDevicesComponent } from './adding-devices/adding-devices.component';


@NgModule({
  declarations: [
    AppComponent,
    DevicesComponent,
    DeviceDetailComponent,
    MessagesComponent,
    DashboardComponent,
    DeviceReadingsComponent,
    GraphComponent,
    AddingDevicesComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    NgbModule.forRoot()
  ],
  providers: [ DeviceService, MessageService, DeviceReadingService, CurrentMeasurementService ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }