import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-changepassword',
  templateUrl: './changepassword.component.html',
  styleUrls: ['./changepassword.component.css']
})
export class ChangePasswordComponent implements OnInit {

  model: any = {};

  processing = false;

  constructor() {
  }

  ngOnInit() {
  }

  changePassword() {
    console.log('change password');
    // todo
  }

}
