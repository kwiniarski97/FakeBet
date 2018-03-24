import {Component, OnInit} from '@angular/core';
import {UserService} from '../../services/user.service';
import {User} from '../../models/user';
import {UserAuth} from '../../models/userauth';
import {AlertService} from '../../services/alert.service';

@Component({
  selector: 'app-changeemail',
  templateUrl: './changeemail.component.html',
  styleUrls: ['./changeemail.component.css']
})
export class ChangeEmailComponent implements OnInit {

  model: any = {};

  processing = false;

  constructor(private userService: UserService, private alertService: AlertService) {
  }

  ngOnInit() {
  }

  changeEmail() {
    if (this.model.newemail1 !== this.model.newemail2) {
      return;
    }
    const user = new UserAuth();
    user.email = this.model.newemail1;
    user.password = this.model.password;

    this.userService.changeEmail(user).subscribe(response => {
      this.alertService.emitOk('Email updated');
    }, error => {
      this.alertService.emitError(error._body);
    });
  }

}
