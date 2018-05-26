import { Injectable } from '@angular/core';
import {Device} from './device';
import {Observable} from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MessageService } from './message.service';
import { catchError, map, tap } from 'rxjs/operators';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import { LogMessage, LogMessageLevel } from './logMessage';
import {environment} from '../environments/environment';

@Injectable()
export class DeviceService {
  private baseUrl = `${environment.baseUri}:${environment.basePort}/api/devices/`;
  constructor(
    private messageService: MessageService,
    private httpClient: HttpClient) {
  }

  getDevices(): Promise<Device[]> {
    return this.httpClient.get<Device[]>(`${this.baseUrl}`)
      .pipe(catchError(this.handleError('getDevices', []))).toPromise();
  }

  addDevice(newDevice: Device): Promise<any> {
    return this.httpClient.post<Device>(`${this.baseUrl}`, newDevice).pipe(catchError(this.handleError('addDevice', []))).toPromise();
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      this.messageService.add(new LogMessage(error.message, LogMessageLevel.Error, 'DeviceService'));
      console.error(error.message);
      return of(result as T);
    };
  }
}
