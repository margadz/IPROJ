import { Component, OnInit, Input } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import { DeviceReading } from '../deviceReading';

@Component({
  selector: 'app-signal-r-client',
  templateUrl: './signal-r-client.component.html',
  styleUrls: ['./signal-r-client.component.css']
})
export class SignalRClientComponent implements OnInit {
  private _hubConnection: HubConnection;
  instantReading: DeviceReading;
  @Input() deviceId: string;
  constructor() {
  }

  ngOnInit() {
    this._hubConnection = new HubConnection('http://localhost:12345/current');

    this._hubConnection.on('SendMessage', (data: any) => {
      this.instantReading = (<DeviceReading[]>data).filter(reading => reading.deviceId == this.deviceId)[0];
    });

    this._hubConnection.start()
      .then(() => {
        console.log('Hub connection started');
      })
      .catch(() => {
        console.log('Error while establishing connection');
      });
  }
}
