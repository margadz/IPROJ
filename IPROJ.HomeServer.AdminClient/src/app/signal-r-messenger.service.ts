import { Injectable } from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {HubConnection} from '@aspnet/signalr';
import {DeviceReading} from './deviceReading';
import {ReplaySubject} from 'rxjs/ReplaySubject';
import {MessageService} from './message.service';
import {LogMessage, LogMessageLevel} from './logMessage';

@Injectable()
export class SignalRMessengerService {
  private url = 'http://192.168.1.10:12345/current';
  private hubConnection: HubConnection;
  private subjects:  Map<string, ReplaySubject<DeviceReading>> = new Map<string, ReplaySubject<DeviceReading>>();
  private readings: Map<string, Observable<DeviceReading>> = new Map<string, Observable<DeviceReading>>();
  private started = false;

  constructor(private messageService: MessageService) { }

  getCurrentReadings(deviceId: string): Observable<DeviceReading> {
    this.initialize();
    return this.getObservable(deviceId);
  }

  requestDeviceDiscovery(): Promise<void> {
    this.initialize();
    return this.hubConnection.invoke('DiscoverDevicesRequest').then((resuslt) => {
      this.messageService.add(new LogMessage('Discovery request has been sent.', LogMessageLevel.Info, 'SignalRMessengerService'));
    });
  }

  private initialize(): void {
    if (!this.started) {
      this.hubConnection = new HubConnection(this.url);
      this.hubConnection.on('SendReadings', (data: any) => {
        this.updateReadings(data);
      });

      this.hubConnection.start()
        .then(() => {
          this.messageService.add(new LogMessage('SignalR hub has been connected', LogMessageLevel.Info, 'SignalRMessengerService'));
        })
        .catch(() => {
          this.messageService
            .add(new LogMessage('Srror while establishing connection to SignalR hub.', LogMessageLevel.Error, 'SignalRMessengerService'));
          this.started = false;
        });
      this.started = true;
    }
  }

  private updateReadings(readings: DeviceReading[]): void {
    if (!readings) {
      return;
    }
    this.subjects.forEach((value, key) => {
      const foundReading = readings.find(reading => reading.deviceId === key);
      if (foundReading) {
        this.subjects.get(key).next(foundReading);
      }
    });
  }

  private getObservable(deviceId: string): Observable<DeviceReading> {
    if (this.readings.get(deviceId)) {
      return this.readings.get(deviceId);
    }
    this.subjects.set(deviceId, new ReplaySubject<DeviceReading>());
    this.readings.set(deviceId, this.subjects.get(deviceId).asObservable());
    return this.readings[deviceId];
  }
}
