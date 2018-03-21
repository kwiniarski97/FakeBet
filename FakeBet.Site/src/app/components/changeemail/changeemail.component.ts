import {Component, OnInit} from '@angular/core';
import {UserService} from '../../services/user.service';

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

  // todo moze dodaj nowy dto do update emaila
  changeEmail() {
    console.log('change email');
  }

}
