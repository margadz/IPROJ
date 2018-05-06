export class LogMessage {
   level: LogMessageLevel;
   message: string;
   sender: string

   constructor (message: string, level: LogMessageLevel, sender: string) {
     this.message = message;
     this.level = level;
     this.sender = sender;
   }
}

export enum LogMessageLevel {
  Info,
  Error,
}
