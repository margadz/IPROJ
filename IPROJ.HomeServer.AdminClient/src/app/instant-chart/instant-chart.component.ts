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
  data: any;
  options: any;

  constructor() {
    this.readings = [];
    this.SetupOption();
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
        backgroundColor: '#1A5D00',
        borderColor: '#283096',
        data: this.readings.map(reading => reading.value)
      }
    ];
  }

  private AddReading(reading: DeviceReading): void {
    this.readings.push(reading);
    if (this.readings.length > 50) {
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
            suggestedMin: 0,
          },
          gridLines: {
            display: false
          }
        }],
        xAxes: [{
          gridLines: {
            display: false
          }
        }]
      },
      elements: {
        point: {
          radius: 0
        }
      }
    };
  }
}
