import { Component, OnInit, Input } from '@angular/core';

import * as d3 from 'd3-selection';
import * as d3Scale from 'd3-scale';
import * as d3Array from 'd3-array';
import * as d3Axis from 'd3-axis';
import {timeFormat} from "d3-time-format";
import {timeParse} from "d3-time-format";
import {DeviceReading} from '../deviceReading';
import {Observable} from 'rxjs/Observable';

@Component({
  selector: 'app-graph',
  templateUrl: './graph.component.html',
  styleUrls: ['./graph.component.css']
})
export class GraphComponent implements OnInit {
  subtitle = 'Wykres zużycia energii';
  @Input() readings$: Observable<DeviceReading[]>;
  @Input() readings: DeviceReading[];
  private width: number;
  private height: number;
  private margin = {top: 20, right: 20, bottom: 30, left: 40};

  private x: any;
  private y: any;
  private svg: any;
  private g: any;

  constructor() {}

  ngOnInit() {
    this.initSvg();
    this.initAxis();
    this.drawAxis();
    this.drawBars();
  }

  private initSvg() {
    this.svg = d3.select('svg');
    this.width = +this.svg.attr('width') - this.margin.left - this.margin.right;
    this.height = +this.svg.attr('height') - this.margin.top - this.margin.bottom;
    this.g = this.svg.append('g')
      .attr('transform', 'translate(' + this.margin.left + ',' + this.margin.top + ')');
  }

  private getReadings(): void{
    this.readings$.subscribe(readings => this.readings = readings);
  }

  private initAxis() {
    this.x = d3Scale.scaleBand().rangeRound([0, this.width]).padding(0.1);
    this.y = d3Scale.scaleLinear().rangeRound([this.height, 0]);
    this.x.domain(this.readings.map((d) => d.readingTimeStamp));

    this.y.domain([0, d3Array.max(this.readings, (d) => d.value)]);
  }

  private drawAxis() {
    this.g.append('g')
      .attr('class', 'axis axis--x')
      .attr('transform', 'translate(0,' + this.height + ')')
      .call(d3Axis.axisBottom(this.x))
    this.g.append('g')
      .attr('class', 'axis axis--y')
      .call(d3Axis.axisLeft(this.y).ticks (10))
      .append('text')
      .attr('class', 'axis-title')
      .attr('transform', 'rotate(-90)')
      .attr('y', 6)
      .attr('dy', '0.71em')
      .attr('text-anchor', 'end')
      .text('kWh');
  }

  private drawBars() {
    this.g.selectAll('.bar')
      .data(this.readings)
      .enter().append('rect')
      .attr('class', 'bar')
      .attr('x', (reading) => this.x(reading.readingTimeStamp) )
      .attr('y', (reading) => this.y(reading.value) )
      .attr('width', this.x.bandwidth())
      .attr('height', (reading) => this.height - this.y(reading.value) );
  }


}