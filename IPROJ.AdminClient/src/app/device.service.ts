import { Injectable } from '@angular/core';
import {Device} from './device';
import {Observable} from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MessageService } from './message.service';
import { catchError, map, tap } from 'rxjs/operators';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

@Injectable()
export class DeviceService {
  private baseUrl = 'http://192.168.1.10:12345/api/devices/';
  constructor(
    private messageService: MessageService,
    private httpClient: HttpClient) {
  }

  getDevices(): Promise<Device[]> {
    return this.httpClient.get<Device[]>(`${this.baseUrl}all`)
      .pipe(catchError(this.handleError('getDevices', []))).toPromise();
  }

  getDevice(id: string): Promise<Device>{
    return this.httpClient.get<Device>(`${this.baseUrl}id/` + id)
      .pipe(catchError(this.handleError('getDevice: ' + id, null))).toPromise();
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      this.messageService.add('DeviceService', error.message)
      console.error(error);
      return of(result as T);
    };
  }
}
