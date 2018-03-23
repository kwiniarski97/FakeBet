import {Component, OnInit} from '@angular/core';
import {AlertService} from '../../services/alert.service';
import {UserService} from '../../services/user.service';
import {UserAuth} from '../../models/userauth';

@Component({
  selector: 'app-deleteaccount',
  templateUrl: './deleteaccount.component.html',
  styleUrls: ['./deleteaccount.component.css']
})
export class DeleteAccountComponent implements OnInit {

  model: any = {};

  processing = false;

  constructor(private alertService: AlertService, private userService: UserService) {
  }

  ngOnInit() {
  }

  deleteAccount(user: UserAuth) {
    this.alertService.emitConfirm('Are you sure you want to delete your account? Operation can\'t be reversed.');
    this.userService.deleteAccount(user).subscribe();
  }

}
