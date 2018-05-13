import { Component, OnInit } from '@angular/core';
import {SettingsService} from '../settings.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {
  powerCost: number;
  constructor(private settings: SettingsService) { }

  ngOnInit() {
    this.powerCost = this.getPowerCost();
  }

  getPowerCost(): number {
    return this.settings.powerCost;
  }

  setPowerCost() {
    this.settings.powerCost = this.powerCost;
  }
}
