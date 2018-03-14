import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-changeemail',
  templateUrl: './changeemail.component.html',
  styleUrls: ['./changeemail.component.css']
})
export class ChangeEmailComponent implements OnInit {

  model: any = {};

  processing = false;

  constructor() {
  }

  ngOnInit() {
  }

  resetPassword() {
    console.log(this.model);
  }

}
