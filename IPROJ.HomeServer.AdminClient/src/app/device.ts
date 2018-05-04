import {ReadingType} from './readingType';
import {DeviceType} from './deviceType';

export class Device {
  deviceId: string;
  name: string;
  typeOfReading: string;
  typeOfDevice: DeviceType;
  isActive: boolean;
  deviceIdString: string;
  host: string;
  customId: string;
}
