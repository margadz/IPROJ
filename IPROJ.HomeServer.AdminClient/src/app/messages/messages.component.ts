import { Component, OnInit } from '@angular/core';
import {MessageService} from '../message.service';

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

  getMessages(): string[] {
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
