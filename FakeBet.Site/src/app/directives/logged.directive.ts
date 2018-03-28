import {Directive, ElementRef} from '@angular/core';
import {RouterLink} from '@angular/router';

@Directive({
  selector: '[appLogged]'
})
export class LoggedDirective {

  constructor(private routerLink: RouterLink, private eltRef: ElementRef) {
  }

}
