import {Component, OnInit} from '@angular/core';
import {User} from '../../../models/user';
import {UserService} from '../../../services/user.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {AlertService} from '../../../services/alert.service';
import {UserRoles, UserStatus} from '../../../models/userenums';

@Component({
  selector: 'app-admin-user',
  templateUrl: './admin-user.component.html',
  styleUrls: ['./admin-user.component.css']
})
export class AdminUserComponent implements OnInit {

  users: User[];

  selectedUser: User = new User();

  constructor(private userService: UserService, private alertService: AlertService, private modalService: NgbModal) {
  }

  ngOnInit() {
  }

  private getAllUsers() {
    this.userService.getAllUsers().subscribe(response => {
      this.users = JSON.parse(response['_body']);
    });
  }

  openUpdateUserModal(content, user: User) {
    Object.assign(this.selectedUser, user);
    this.modalService.open(content, {size: 'lg'});
  }

  getUserByNickname(nickname: string) {
    this.userService.getByNickname(nickname).subscribe(result => {
      console.log(result.nickName);
      const user = result;
      this.users = [user];
      console.log(this.users);
    });
  }

  updateUser() {
    this.userService.updateUser(this.selectedUser).subscribe(result => {
      this.alertService.emitOk('Updated');
      this.getAllUsers();
    }, error => {
      this.alertService.emitError(error['_body']);
    });
  }

  getUserStatus(status: UserStatus) {
    return UserStatus[status];
  }

  getUserRole(role: UserRoles) {
    return UserRoles[role];
  }
}
