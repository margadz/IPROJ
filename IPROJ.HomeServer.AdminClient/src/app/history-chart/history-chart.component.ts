import { Component, OnInit, Input } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { DeviceReading } from '../deviceReading';

@Component({
  selector: 'app-history-chart',
  templateUrl: './history-chart.component.html',
  styleUrls: ['./history-chart.component.scss']
})
export class HistoryChartComponent implements OnInit {
  @Input() readings$: Observable<DeviceReading[]>;
  private readings: DeviceReading[];
  data: any;
  options: any;

  public static FormatDate(date: Date): string {
    const newDate = new Date(date);
    return newDate.toLocaleDateString();
  }

  constructor() {
    this.options = {
      title: {
        display: true,
        text: 'Wykres zużycia energii elektrycznej',
        fontSize: 16
      },
      legend: {
        display: false
      },
      scales: {
        yAxes: [{
          scaleLabel: {
            display: true,
            labelString: 'kWh',
            fontSize: 14
          }
        }]
      }
    };
  }

  ngOnInit() {
    this.getReadings();
  }

  private getReadings(): void {
    this.readings$.subscribe(readings => {
      this.readings = readings.reverse();
      this.setupData();
    });
  }

  private setupData() {
    this.data = {};
    this.data.labels = this.readings.map(reading => HistoryChartComponent.FormatDate(reading.readingTimeStamp));
    this.data.datasets = [
      {
        label: 'Dzienne zużycie',
        backgroundColor: '#42A5F5',
        borderColor: '#1E88E5',
        data: this.readings.map(reading => reading.value)
      }
    ];
  }
}
