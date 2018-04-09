import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DeviceReading } from './deviceReading';
import { Observable } from 'rxjs/Observable';
import { MessageService } from './message.service';
import {of} from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable()
export class DeviceReadingService {
  private baserUrl = 'http://localhost:12345/api/readings/readingsFor';
  constructor(
    private messageService: MessageService,
    private httpClient: HttpClient) { }

  getReadingFor(deviceId: string): Promise<DeviceReading[]> {
    const url = this.baserUrl + '/' + deviceId;
    return this.httpClient.get<DeviceReading[]>(`${url}`)
      .pipe(catchError(this.handleError('getReadingsFor: ' + deviceId, null))).toPromise();
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      console.error(error);
      return of(result as T);
    };
  }
}
