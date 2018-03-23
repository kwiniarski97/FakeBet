import {Component, OnInit} from '@angular/core';
import {ChangePasswordModel} from '../../models/change-password-model';
import {UserService} from '../../services/user.service';
import {AlertService} from '../../services/alert.service';

@Component({
  selector: 'app-changepassword',
  templateUrl: './changepassword.component.html',
  styleUrls: ['./changepassword.component.css']
})
export class ChangePasswordComponent implements OnInit {

  model: any = {};

  processing = false;

  constructor(private userService: UserService, private alertService: AlertService) {
  }

  ngOnInit() {
  }

  changePassword() {
    const changePassword = new ChangePasswordModel();
    changePassword.newPassword = this.model.newpassword1;
    changePassword.currentPassword = this.model.currentpassword;

    this.userService.changePassword(changePassword).subscribe(data => {

    }, error => {
      this.alertService.emitError(error._body);
    });
    // todo
  }

}
