import { Injectable } from '@angular/core';
import {LogMessage} from './logMessage';

@Injectable()
export class MessageService {
  private messages: LogMessage[] = [];

  add(message: LogMessage) {
    this.messages.push(message);
  }

  getMessages(): LogMessage[] {
    return this.messages;
  }

  clear() {
    this.messages = [];
  }
}
