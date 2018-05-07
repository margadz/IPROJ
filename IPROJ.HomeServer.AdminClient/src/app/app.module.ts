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
import { AddingDevicesComponent } from './adding-devices/adding-devices.component';
import { DeviceManagementComponent } from './device-management/device-management.component';
import { DeviceTemplateComponent } from './device-template/device-template.component';
import { FormsModule } from '@angular/forms';
import { DialogModule } from 'primeng/dialog';
import { ChartModule } from 'primeng/chart';
import { HistoryChartComponent } from './history-chart/history-chart.component';
import { InstantChartComponent } from './instant-chart/instant-chart.component';
import { SignalRMessengerService } from './signal-r-messenger.service';



@NgModule({
  declarations: [
    AppComponent,
    DevicesComponent,
    DeviceDetailComponent,
    MessagesComponent,
    DashboardComponent,
    DeviceReadingsComponent,
    AddingDevicesComponent,
    DeviceManagementComponent,
    DeviceTemplateComponent,
    HistoryChartComponent,
    InstantChartComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    DialogModule,
    ChartModule,
    NgbModule.forRoot()
  ],
  providers: [ DeviceService, MessageService, DeviceReadingService, SignalRMessengerService ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
