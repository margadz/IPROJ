import { Component, OnInit } from '@angular/core';
import {MessageService} from '../message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.scss']
})
export class MessagesComponent implements OnInit {
  private _isCollapsed = true;

  constructor(private messageService: MessageService ) { }

  ngOnInit() {
  }

  getMessages(): string[] {
    return this.messageService.getMesages();
  }

  get isCollapsed(): boolean {
    return this._isCollapsed;
  }

  switchCollapse(): void {
    this._isCollapsed = !this._isCollapsed;
  }

  clearMessages(): void {
    this.messageService.clear();
    this.switchCollapse();
  }
}
