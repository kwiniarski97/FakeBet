import {Injectable} from '@angular/core';

@Injectable()
export class AlertService {


  constructor() {
  }

  emitError(message: String) {
    alert(message);
  }

  emitConfirm(message: string): boolean {
    return confirm(message);
  }
}
