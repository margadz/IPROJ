import { Component, OnInit } from '@angular/core';
import {MessageService} from '../message.service';
import {LogMessage} from '../logMessage';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.scss']
})
export class MessagesComponent implements OnInit {
  private isCollapsedP = true;

  constructor(private messageService: MessageService ) { }

  ngOnInit() {
  }

  getMessages(): LogMessage[] {
    return this.messageService.getMessages();
  }

  get isCollapsed(): boolean {
    return this.isCollapsedP;
  }

  switchCollapse(): void {
    this.isCollapsedP = !this.isCollapsedP;
  }

  clearMessages(): void {
    this.messageService.clear();
    this.switchCollapse();
  }
}
