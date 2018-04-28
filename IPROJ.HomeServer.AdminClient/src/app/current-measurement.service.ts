import { Injectable } from '@angular/core';
import { DeviceReading } from './deviceReading';
import { HubConnection } from '@aspnet/signalr';
import { Observable } from 'rxjs/Observable';
import { ReplaySubject } from 'rxjs/ReplaySubject';

@Injectable()
export class CurrentMeasurementService {
  private _hubConnection: HubConnection;
  private _subjects:  Map<string, ReplaySubject<DeviceReading>> = new Map<string, ReplaySubject<DeviceReading>>();
  private _readings: Map<string, Observable<DeviceReading>> = new Map<string, Observable<DeviceReading>>();
  private _started = false;
  constructor() { }

  getCurrentReadings(deviceId: string): Observable<DeviceReading>{
    this.initialize();
    return this.getObservable(deviceId);
  }

  private initialize(): void{
    if (!this._started) {
      this._hubConnection = new HubConnection('http://192.168.1.10:12345/current');
      this._hubConnection.on('SendMessage', (data: any) => {
        this.updateReadings(data);
      });

      this._hubConnection.start()
        .then(() => {
          console.log('Hub connection started');
        })
        .catch(() => {
          console.log('Error while establishing connection');
        });
      this._started = true;
    }
  }

  private updateReadings(readings: DeviceReading[]): void {
    if (!readings) {
      return;
    }

    this._subjects.forEach((value, key) => {
        const foundReading = readings.find(reading => reading.deviceId === key);
        if (foundReading) {
          this._subjects.get(key).next(foundReading);
        }
      });
  }

  private getObservable(deviceId: string): Observable<DeviceReading>{
    if (this._readings.get(deviceId)) {
      return this._readings.get(deviceId);
    }
    this._subjects.set(deviceId, new ReplaySubject<DeviceReading>());
    this._readings.set(deviceId, this._subjects.get(deviceId).asObservable());
    return this._readings[deviceId];
 }
}
