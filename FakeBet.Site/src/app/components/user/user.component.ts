import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  user: any = {};
  avatarPath = 'assets/avatar.png';

  constructor() {
  }

  ngOnInit() {
    const storageString = localStorage.getItem('currentUser');
    if (storageString == null) {
      return;
    }
    this.user = JSON.parse(storageString);
  }
}
