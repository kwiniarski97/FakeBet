import {Component, Input, OnInit} from '@angular/core';
import {LocalStorageService} from '../../services/localstorage.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  @Input()
  isLogged: boolean;

  constructor(private localStorageService: LocalStorageService) {
  }

  ngOnInit() {
    this.isLogged = this.localStorageService.isUserLogged();
  }

}
