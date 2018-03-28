import {Injectable} from '@angular/core';
import {BehaviorSubject} from 'rxjs/BehaviorSubject';

@Injectable()
export class IsLoggedService {
  private messageSource = new BehaviorSubject<boolean>(false);
  currentMessage = this.messageSource.asObservable();

  // todo https://angularfirebase.com/lessons/sharing-data-between-angular-components-four-methods/

  constructor() {
  }

  changeMessage(message: boolean) {
    this.messageSource.next(message);
  }

}
