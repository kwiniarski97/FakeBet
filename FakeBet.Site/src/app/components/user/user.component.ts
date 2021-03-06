import {Component, OnInit} from '@angular/core';
import {LocalStorageService} from '../../services/localstorage.service';
import {User} from '../../models/user';
import {Router} from '@angular/router';
import {UserService} from '../../services/user.service';
import {printLine} from 'tslint/lib/verify/lines';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  user: any = new User();
  avatarUrl;

  constructor(private localStorageService: LocalStorageService, private userService: UserService, private router: Router) {

  }

  ngOnInit() {
    this.user = this.localStorageService.retrieve('currentUser') as User;
    this.userService.getByNickname(this.user.nickName).subscribe(data => {
      this.user = data;
    });
    this.avatarUrl = this.user.avatarUrl != null ? this.user.avatarUrl : 'assets/images/avatar_placeholder.png';
  }

  navigateToChangeEmail() {
    this.router.navigate(['user/change-email']);
  }

  navigateToChangePassword() {
    this.router.navigate(['user/change-password']);
  }


  navigateToDeleteAccount() {
    this.router.navigate(['user/delete']);
  }


}
