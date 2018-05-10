import {Component, Input, OnInit} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {DeviceReading} from '../deviceReading';
import {HistoryChartComponent} from '../history-chart/history-chart.component';

@Component({
  selector: 'app-instant-chart',
  templateUrl: './instant-chart.component.html',
  styleUrls: ['./instant-chart.component.scss']
})
export class InstantChartComponent implements OnInit {
  @Input() reading$: Observable<DeviceReading>;
  private readings: DeviceReading[];
  private readonly emptyReading: DeviceReading;
  data: any;
  options: any;

  constructor() {
    this.readings = [];
    this.SetupOption();
    this.emptyReading = new DeviceReading();
    this.emptyReading.value = 0;
  }

  ngOnInit() {
    this.getReadings();
  }

  private getReadings(): void {
    this.reading$.subscribe(readings => {
      this.AddReading(readings);
      this.setupData();
    });
  }

  private setupData() {
    this.data = {};
    this.data.labels = this.readings.map(reading => '');
    this.data.datasets = [
      {
        label: 'Aktualne zużycie energii elektrycznej',
        backgroundColor: '#42cef4',
        borderColor: '#283096',
        data: this.readings.map(reading => reading.value)
      }
    ];
  }

  private AddReading(reading: DeviceReading): void {
    if (this.readings.length === 0) {
      for (let i = 0; i < 60; i++) {
        this.readings.push(this.emptyReading);
      }
    } else {
      this.readings.push(reading);
    }
    if (this.readings.length > 60) {
      this.readings.shift();
    }
  }

  private SetupOption(): void {
    this.options = {
      title: {
        display: false,
      },
      legend: {
        display: false,
        labels: {
          fontSize: 0
        }
      },
      animation: {
        duration: 0
      },
      scales: {
        yAxes: [{
          scaleLabel: {
            display: true,
            labelString: 'W',
            fontSize: 14
          },
          ticks: {
            beginAtZero: true
          },
        }],
      },
      elements: {
        point: {
          radius: 0
        }
      }
    };
  }
}
