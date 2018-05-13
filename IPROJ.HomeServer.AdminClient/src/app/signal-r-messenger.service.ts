import { Injectable } from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {HubConnection} from '@aspnet/signalr';
import {DeviceReading} from './deviceReading';
import {ReplaySubject} from 'rxjs/ReplaySubject';
import {MessageService} from './message.service';
import {LogMessage, LogMessageLevel} from './logMessage';
import {Device} from './device';
import {environment} from '../environments/environment';

@Injectable()
export class SignalRMessengerService {
  private readonly newDevices$: Observable<Device[]>;
  private url = `${environment.baseUri}:${environment.basePort}/current`;
  private hubConnection: HubConnection;
  private subjects:  Map<string, ReplaySubject<DeviceReading>> = new Map<string, ReplaySubject<DeviceReading>>();
  private readings: Map<string, Observable<DeviceReading>> = new Map<string, Observable<DeviceReading>>();
  private newDevicesSubject: ReplaySubject<Device[]> = new ReplaySubject<Device[]>();
  private started = false;

  constructor(private messageService: MessageService) {
    this.newDevices$ = this.newDevicesSubject.asObservable();
    this.initialize();
  }

  getCurrentReadings(deviceId: string): Observable<DeviceReading> {
    this.initialize();
    return this.getObservable(deviceId);
  }

  get Devices(): Observable<Device[]> {
    return this.newDevices$;
  }

  requestDeviceStateChange(reading: DeviceReading): Promise<void> {
    this.initialize();
    return this.hubConnection.invoke('SetDeviceStateRequest', reading).then((resuslt) => {
      this.messageService.add(new LogMessage('Device state change has been sent.', LogMessageLevel.Info, 'SignalRMessengerService'));
    });
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
      this.hubConnection.on('SendDiscoveredDevices', (data: any) => this.updateDiscoveredDevices(data));

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

  private updateDiscoveredDevices(devices: Device[]): void {
    this.newDevicesSubject.next(devices);
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
