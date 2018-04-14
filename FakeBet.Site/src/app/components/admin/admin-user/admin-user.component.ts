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
  private loading = false;

  constructor(private userService: UserService, private alertService: AlertService, private modalService: NgbModal) {
  }

  ngOnInit() {
  }

  private getAllUsers() {
    this.loading = true;
    this.userService.getAllUsers().subscribe(response => {
      this.loading = false;
      this.users = JSON.parse(response['_body']);
    });
  }

  private openUpdateUserModal(content, user: User) {
    Object.assign(this.selectedUser, user);
    this.modalService.open(content, {size: 'lg'});
  }

  private getUserByNickname(nickname: string) {
    if (nickname == null || nickname.length === 0) {
      return;
    }
    this.loading = true;
    this.userService.getByNickname(nickname).subscribe(result => {
      this.loading = false;
      const user = result;
      this.users = [user];
    });
  }

  private updateUser() {
    this.userService.updateUser(this.selectedUser).subscribe(result => {
      this.alertService.emitOk('Updated');
      this.getAllUsers();
    }, error => {
      this.alertService.emitError(error['_body']);
    });
  }

  private getUserStatus(status: UserStatus) {
    return UserStatus[status];
  }

  private getUserRole(role: UserRoles) {
    return UserRoles[role];
  }
}
