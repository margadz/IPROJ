import { Injectable } from '@angular/core';

@Injectable()
export class MessageService {
  private messages: string[] = [];

  add(sender: string,  message: string) {
    this.messages.push(`Sender: ${sender} - ${message}` );
  }

  getMessages(): string[] {
    return this.messages;
  }

  clear() {
    this.messages = [];
  }
}
